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
    public class SPRegRequestService : GeneralService
    {
        private readonly IMapper _mapper;
        private readonly AddressService _addressService;
        private readonly BaseUserService _baseUserService;
        private readonly ServiceProviderService _serviceProviderService;

        public SPRegRequestService(IUnitOfWork unitOfWork, IMapper mapper, AddressService addressService, BaseUserService baseUserService, ServiceProviderService serviceProviderService) : base(unitOfWork)
        {
            _mapper = mapper;
            _addressService = addressService;
            _baseUserService = baseUserService;
            _serviceProviderService = serviceProviderService;
        }
        public async Task<SPRegRequestDTO> AddRequestAsync(SPRegRequestDTO RequestDTO)
        {
            // 1. Validate uniqueness of user information
            if (await _serviceProviderService.IsUserInformationDuplicate(
                email: RequestDTO.ServiceProvider.Account.Email,
                phonenumber: RequestDTO.ServiceProvider.Account.PhoneNumber,
                username: RequestDTO.ServiceProvider.Account.UserName))
            {
                throw new BadRequestException("Email, phone number, or username is already in use");
            }

            // 2. Check if there are approved or pending requests
            IsThereApprovedOrPendingRequest(RequestDTO);

            // 3. Map DTOs to entities
            var contactInformation = _mapper.Map<ContactInformationEntity>(RequestDTO.Business.ContactInformation);
            var address = _mapper.Map<AddressEntity>(RequestDTO.Business.Address);
            var business = _mapper.Map<BusinessEntity>(RequestDTO.Business);
            var request = _mapper.Map<SPRegRequestEntity>(RequestDTO);
            request.Status = EnRegisterationRequestStatus.Pending;
            // 4. Validate entities
            ValidationHelper.ValidateEntities(
            (
                        [
                contactInformation,
                address,
                business,
                request,
                new AuthoUser(){
                UserName = RequestDTO.ServiceProvider.Account.UserName,
                Email = RequestDTO.ServiceProvider.Account.Email,
                PhoneNumber = RequestDTO.ServiceProvider.Account.PhoneNumber,
                 }
            ]
            ));

            // 5. Insert Address and Contact Information
            await _addressService.CreateEntity(address); // Create Address
            await CreateEntity(contactInformation); // Create Contact Information

            // 6. Link Business with Address and Contact Information
            business.Address = address;
            business.ContactInformation = contactInformation;
            await CreateEntity(business); // Save Business

            // 7. Link Request with Business
            request.Business = business;
            await CreateEntity(request); // Save Request

            // 8. Save changes and verify creation
            await _unitOfWork.SaveChangesAsync();
            CheckCreatedState<SPRegRequestEntity>(request.SPRegRequestID);

            // 9. Create Service Provider
            RequestDTO.ServiceProvider.BusinessID = business.BusinessID;
            var serviceProviderDTO = await _serviceProviderService.RegisterAsync(RequestDTO.ServiceProvider);

            // 10. Map results back to DTO
            RequestDTO = _mapper.Map<SPRegRequestDTO>(request);
            RequestDTO.ServiceProvider = serviceProviderDTO;

            return RequestDTO;
        }
        public List<SPRegRequestDTO> GetAllRegistrationRequestsAsync(EnRegisterationRequestStatus? status = null)
        {
            _baseUserService.CheckRole(EnUserRole.Admin.ToString());
            IQueryable<SPRegRequestEntity> query = _unitOfWork.SPRegRequests.GetAllQueryable()
            .Include(r => r.Business)
            .ThenInclude(b => b.ServiceProvider)
            .ThenInclude(sp => sp.Account) 
            .Include(r => r.Business)
                .ThenInclude(b => b.ContactInformation)
            .Include(r => r.Business)
                .ThenInclude(b => b.Address);

            if (status.HasValue)
            {
                query = query.Where(r => r.Status == status.Value);
            }
            var requests = query.ToList();
            if (CheckList(requests)) return [];

            return _mapper.Map<List<SPRegRequestDTO>>(requests);
        }
        private void IsThereApprovedOrPendingRequest(SPRegRequestDTO RequestDTO)
        {
            SPRegRequestEntity? request = _unitOfWork.SPRegRequests
                .GetAllQueryable()
                .Include("Business")
                .FirstOrDefault(i =>
                    i.Business.BusinessLicenseNumber == RequestDTO.Business.BusinessLicenseNumber &&
                    (i.Status == EnRegisterationRequestStatus.Approved ||
                     i.Status == EnRegisterationRequestStatus.Pending));

            if (request != null)
                throw new BadRequestException("There is an active, approved, or pending request with the entered BusinessLicenseNumber.");
        }



    }
}
