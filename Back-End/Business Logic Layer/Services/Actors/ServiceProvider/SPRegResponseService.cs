using Core_Layer.DTOs;
using Core_Layer.Entities.Actors.ServiceProvider.Registeration_Request;
using Core_Layer.Entities.Actors;
using Core_Layer.Enums;
using Core_Layer.Exceptions;
using Data_Access_Layer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core_Layer.Entities.Actors.ServiceProvider;
using Azure;
using Azure.Core;

namespace Business_Logic_Layer.Services.Actors.ServiceProvider { 
    public class SPRegResponseService(IUnitOfWork unitOfWork, IMapper mapper, ServiceProviderService serviceProviderService) : GeneralService(unitOfWork)
    {
        private readonly IMapper _mapper = mapper;
        private  readonly ServiceProviderService _ServiceProviderService = serviceProviderService;

        public async Task<SPRegResponseDTO> ReviewRegistrationApplicationAsync(SPRegResponseDTO response)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Check if the user has Admin role
                _ServiceProviderService.EnsureAdminRole();

                // Fetch the related request
                var requestEntity = await GetRequestEntityAsync(response.RequestID);

                // Validate the existence of related entities
                var managerExists = await _unitOfWork.Managers.FirstOrDefaultAsync(m => m.AccountID == response.RespondedByID) ??
                    throw new NotFoundException($"Manager with ID {response.RespondedByID} not found.");
                response.RespondedByID = managerExists.ManagerID.ToString();

                // Validate the request status
                ValidateRequestStatus(requestEntity);

                // Check for existing positive response
                await ValidatePositiveResponseAsync(response.RequestID);

                // Map response DTO to entity and save it
                var responseEntity = await CreateResponseEntityAsync(response);

                // Update the status of the related request
                await UpdateRequestStatusAsync(response, requestEntity);

                // Update the status of the related Service Provider
                await UpdateServiceProviderStatusAsync(responseEntity, requestEntity);

                await _unitOfWork.SaveChangesAsync();
                await transaction.CommitAsync();
                return _mapper.Map<SPRegResponseDTO>(responseEntity);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        private async Task<SPRegRequestEntity> GetRequestEntityAsync(int requestId)
        {
            return await _unitOfWork.SPRegRequests.FirstOrDefaultAsync(r => r.SPRegRequestID == requestId)
                ?? throw new NotFoundException($"Request with ID {requestId} not found.");
        }
        private void ValidateRequestStatus(SPRegRequestEntity request)
        {
            if (request.Status == EnRegisterationRequestStatus.Regected)
                throw new BadRequestException("Rejected requests cannot be approved or responded to again.");
        }
        private async Task ValidatePositiveResponseAsync(int requestId)
        {
            var positiveResponseExists = await _unitOfWork.SPRegResponses.AnyAsync(r =>
                r.RequestID == requestId && r.Result);
            if (positiveResponseExists)
                throw new BadRequestException("Cannot add a new response when a positive response already exists.");
        }
        private async Task<SPRegResponseEntity> CreateResponseEntityAsync(SPRegResponseDTO response)
        {
            var responseEntity = _mapper.Map<SPRegResponseEntity>(response);
            await CreateEntityAsync(responseEntity, saveChanges: true);
            return responseEntity;
        }
        private async Task UpdateRequestStatusAsync(SPRegResponseDTO response, SPRegRequestEntity request)
        {
            request.Status = response.Result ? EnRegisterationRequestStatus.Approved : EnRegisterationRequestStatus.Regected;
            _unitOfWork.SPRegRequests.Update(request);
            await _unitOfWork.SaveChangesAsync();
        }
        private async Task UpdateServiceProviderStatusAsync(SPRegResponseEntity responseEntity, SPRegRequestEntity requestEntity)
        {
            _ServiceProviderService.EnsureAdminRole();

            var serviceProvider = await _ServiceProviderService.GetServiceProviderByBussinesID(requestEntity.BusinessID) ?? throw new NotFoundException($"ServiceProvider not found for BusinessID {requestEntity.BusinessID}");
            if (responseEntity.Result)
            {
                await _ServiceProviderService.ChangeUserStatusAsync(EnAccountStatus.Active, serviceProvider.AccountID);
            }
            else
            {
                await _ServiceProviderService.DeletePassifServiceProviderAccountAsync(serviceProvider);
            }
        }

    }
}
