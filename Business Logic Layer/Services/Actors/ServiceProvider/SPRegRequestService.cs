using AutoMapper;
using Azure;
using Azure.Core;
using Business_Logic_Layer.Utilities;
using Core_Layer.DTOs;
using Core_Layer.Entities;
using Core_Layer.Entities.Actors;
using Core_Layer.Entities.Actors.ServiceProvider;
using Core_Layer.Entities.Actors.ServiceProvider.Registeration_Request;
using Core_Layer.Entities.Locations;
using Core_Layer.Enums;
using Core_Layer.Exceptions;
using Data_Access_Layer.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.Actors.ServiceProvider
{
    public class SPRegRequestService(IUnitOfWork unitOfWork, IMapper mapper, AddressService addressService, BaseUserService baseUserService, ServiceProviderService serviceProviderService) : GeneralService(unitOfWork)
    {
        private readonly IMapper _mapper = mapper;
        private readonly AddressService _addressService = addressService;
        private readonly BaseUserService _baseUserService = baseUserService;
        private readonly ServiceProviderService _serviceProviderService = serviceProviderService;

        public async Task<SPRegRequestDTO> CreateRequestAsync(SPRegRequestDTO requestDTO)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Check for existing approved or pending requests
                ValidateExistingRequests(requestDTO);

                // Validate uniqueness of user information
                await ValidateUserInformationAsync(requestDTO);

                // Map DTOs to entities
                var contactInformation = _mapper.Map<ContactInformationEntity>(requestDTO.Business.ContactInformation);
                var address = _mapper.Map<AddressEntity>(requestDTO.Business.Address);
                var business = _mapper.Map<BusinessEntity>(requestDTO.Business);
                var request = _mapper.Map<SPRegRequestEntity>(requestDTO);
                request.Status = EnRegisterationRequestStatus.Pending;

                // Validate and save entities
                await ValidateAndSaveEntitiesAsync(contactInformation, address, business, request);

                // Link and save service provider
                requestDTO.ServiceProvider.BusinessID = business.BusinessID;
                var serviceProviderDTO = await _serviceProviderService.RegisterAsync(requestDTO.ServiceProvider);

                // Map back results to DTO
                requestDTO = _mapper.Map<SPRegRequestDTO>(request);
                requestDTO.ServiceProvider = serviceProviderDTO;

                await transaction.CommitAsync();
                return requestDTO;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }

        }
        private async Task ValidateUserInformationAsync(SPRegRequestDTO requestDTO)
        {
            if (await _serviceProviderService.IsUserInformationDuplicate(
                    requestDTO.ServiceProvider.Account.Email,
                    requestDTO.ServiceProvider.Account.PhoneNumber,
                    requestDTO.ServiceProvider.Account.UserName))
            {
                throw new BadRequestException("Email, phone number, or username is already in use.");
            }
        }

        private void ValidateExistingRequests(SPRegRequestDTO requestDTO)
        {
            var existingRequest = _unitOfWork.SPRegRequests
                .GetAllQueryable()
                .Include(r => r.Business)
                .FirstOrDefault(r =>
                    r.Business.BusinessLicenseNumber == requestDTO.Business.BusinessLicenseNumber &&
                    (r.Status == EnRegisterationRequestStatus.Approved || r.Status == EnRegisterationRequestStatus.Pending));

            if (existingRequest != null)
            {
                throw new BadRequestException("An active, approved, or pending request with the same BusinessLicenseNumber already exists.");
            }
        }

        private async Task ValidateAndSaveEntitiesAsync(
            ContactInformationEntity contactInformation,
            AddressEntity address,
            BusinessEntity business,
            SPRegRequestEntity request)
        {
            ValidationHelper.ValidateEntities([contactInformation, address, business, request]);

            await _addressService.CreateEntityAsync(address);
            await CreateEntityAsync(contactInformation);

            business.Address = address;
            business.ContactInformation = contactInformation;
            await CreateEntityAsync(business);

            request.Business = business;
            await CreateEntityAsync(request);

            await _unitOfWork.SaveChangesAsync();
            CheckCreatedState<SPRegRequestEntity>(request.SPRegRequestID);
        }

        public List<SPRegRequestDTO> GetAllRegistrationRequests(EnRegisterationRequestStatus? status = null)
        {
            // Step 1: Ensure the user has the correct role
            _baseUserService.CheckRole(EnUserRole.Admin.ToString());

            // Step 2: Build the query with includes
            var query = BuildRegistrationRequestQuery();

            // Step 3: Filter by status if provided
            if (status.HasValue) query = query.Where(r => r.Status == status.Value);

            // Step 4: Execute query and map results
            var requests = query.ToList();
            if (requests.Count == 0) return [];

            return _mapper.Map<List<SPRegRequestDTO>>(requests);
        }

        private IQueryable<SPRegRequestEntity> BuildRegistrationRequestQuery()
        {
            return _unitOfWork.SPRegRequests.GetAllQueryable()
              .Include(r => r.Business)
                  .ThenInclude(b => b.ServiceProvider)
                  .ThenInclude(sp => sp.Account)
              .Include(r => r.Business)
                  .ThenInclude(b => b.ContactInformation)
              .Include(r => r.Business)
                  .ThenInclude(b => b.Address)
              .Include(r => r.Response);
        }
    }
}

