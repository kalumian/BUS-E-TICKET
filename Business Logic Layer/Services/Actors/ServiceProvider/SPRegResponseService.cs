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

namespace Business_Logic_Layer.Services.Actors.ServiceProvider { 
    public class SPRegResponseService(IUnitOfWork unitOfWork, IMapper mapper, ServiceProviderService serviceProviderService) : GeneralService(unitOfWork)
    {
        private readonly IMapper _mapper = mapper;
        private  readonly ServiceProviderService _ServiceProviderService = serviceProviderService;

        public async Task<SPRegResponseDTO> AcceptRegistrationRequestAsync(SPRegResponseDTO Respone)
        {
            _ServiceProviderService.CheckRole("Admin");

            // Validate if the request and manager exist
            ValidateEntitiesExistence(Respone);


            // Fetch the related request
            var RequestEntity = await _unitOfWork.SPRegRequests.FirstOrDefaultAsync(e => e.SPRegRequestID == Respone.RequestID)
                ?? throw new NotFoundException("Request not found.");

            if (RequestEntity.Status == EnRegisterationRequestStatus.Regected)
                throw new BadRequestException("Rejected requests cannot be approved.");

            // Check if a positive response already exists for the Request
            var existingPositiveResponse = await IsPositiveResponseExists(Respone.RequestID);

            if (existingPositiveResponse)
                throw new BadRequestException("Cannot add a negative response when a positive response already exists with status OK.");

            // Map the response DTO, ServiceProivder to entity and save it
            SPRegResponseEntity? ResponeEntity = _mapper.Map<SPRegResponseEntity>(Respone);
            await CreateEntity(ResponeEntity);

            // Update the status of the related Request
            await UpdateRequestStatus(Respone);
            
            await UpdateSPStatus(ResponeEntity, RequestEntity);
            return _mapper.Map<SPRegResponseDTO>(ResponeEntity);
        }
        private async Task UpdateSPStatus(SPRegResponseEntity ResponeEntity, SPRegRequestEntity RequestEntity)
        {
            _ServiceProviderService.CheckRole("Admin");

            var ServiceProvider = await _ServiceProviderService.GetServiceProviderByBussinesID(RequestEntity.BusinessID);

            if (ServiceProvider == null)
            {
                throw new NotFoundException($"ServiceProvider not found for BusinessID {RequestEntity.BusinessID}");
            }

            if (ResponeEntity.Result)
            {
                if (string.IsNullOrEmpty(ServiceProvider.AccountID))
                {
                    throw new InvalidOperationException("ServiceProvider's AccountID is null or empty.");
                }

                _ServiceProviderService.ChangeUserStatus(EnAccountStatus.Active, ServiceProvider.AccountID);
            }
            else
            {
                _ServiceProviderService.DeletePassifServiceProviderAccount(ServiceProvider);
            }
        }
        private void ValidateEntitiesExistence(SPRegResponseDTO Respone)
        {
            CheckEntityExist<SPRegRequestEntity>(i => i.SPRegRequestID == Respone.RequestID);
            CheckEntityExist<ManagerEntity>(i => i.ManagerID == Respone.RespondedByID);
        }

        private async Task<bool> IsPositiveResponseExists(int requestId)
        {
            return await _unitOfWork.SPRegResponses.FirstOrDefaultAsync(r =>
                r.SPRegRequestID == requestId && r.Status == EnRegisterationRequestStatus.Approved) != null;
        }

        private async Task UpdateRequestStatus(SPRegResponseDTO Respone)
        {
            var request = await _unitOfWork.SPRegRequests.FirstOrDefaultAsync(e => e.SPRegRequestID == Respone.RequestID) ?? throw new NotFoundException("Request not found.");
            request.Status = Respone.Result ? EnRegisterationRequestStatus.Approved : EnRegisterationRequestStatus.Regected;
            _unitOfWork.SPRegRequests.Update(request);

        }

    }
}
