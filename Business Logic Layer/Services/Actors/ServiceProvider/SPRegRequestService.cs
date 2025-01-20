using AutoMapper;
using Azure.Core;
using Business_Logic_Layer.Utilities;
using Core_Layer.DTOs;
using Core_Layer.Entities;
using Core_Layer.Entities.Actors;
using Core_Layer.Entities.Actors.ServiceProvider;
using Core_Layer.Entities.Actors.ServiceProvider.Registeration_Request;
using Core_Layer.Entities.Locations;
using Core_Layer.Enums;
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
        private IMapper _mapper;
        private AddressService _addressService;
        private BaseUserService _baseUserService;

        public SPRegRequestService(IUnitOfWork unitOfWork, IMapper mapper, AddressService addressService, BaseUserService baseUserService) : base(unitOfWork)
        {
            _mapper = mapper;
            _addressService = addressService;
            _baseUserService = baseUserService;
        }
        public async Task<int> AddRequestAsync(SPRegRequestDTO RequestDTO ) {
            // Create Entites 
            ContactInformationEntity ContactInformation = _mapper.Map<ContactInformationEntity>(RequestDTO.Business.ContactInformation);
            AddressEntity Address = _mapper.Map<AddressEntity>(RequestDTO.Business.Address);
            BusinessEntity Business = _mapper.Map<BusinessEntity>(RequestDTO.Business);
            SPRegRequestEntity Request = _mapper.Map<SPRegRequestEntity>( RequestDTO );

            // Entites Validation
            ValidationHelper.ValidateEntities(new List<object> { Request, Business, ContactInformation, Address });


            //Insert ContactInfo, Address and Save
            await CreateEntity(ContactInformation);
            await _addressService.CreateEntity(Address);

            //Insert Business and Save
            Business.AddressID = Address.AddressID;
            Business.ContactInformationID = ContactInformation.ContactInformationID;
            await CreateEntity(Business);

            //Insert Request and Save
            Request.BusinessID = Business.BusinessID;
            await CreateEntity(Request);

            CheckCreatedState<SPRegRequestEntity>(Request.SPRegRequestID);
            return Request.SPRegRequestID;
        }
        public async Task<List<SPRegRequestDTO>> GetAllRegistrationRequestsAsync(EnRegisterationRequestStatus? status = null)
        {
            _baseUserService.CheckRole(EnUserRole.Admin.ToString());
            IQueryable<SPRegRequestEntity> query = _unitOfWork.SPRegRequests.GetAllQueryable()
                .Include(r => r.Business)
                    .ThenInclude(b => b.ContactInformation)
                .Include(r => r.Business)
                    .ThenInclude(b => b.Address);

            if (status.HasValue)
            {
                query = query.Where(r => r.Status == status.Value);
            }
            var requests = query.ToList();
            if (CheckList(requests)) return new List<SPRegRequestDTO>();
            return _mapper.Map<List<SPRegRequestDTO>>(requests);
        }
    }
}
