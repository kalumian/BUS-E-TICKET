using AutoMapper;
using Azure.Core;
using Business_Logic_Layer.Utilities;
using Core_Layer.DTOs;
using Core_Layer.Entities;
using Core_Layer.Entities.Actors;
using Core_Layer.Entities.Actors.ServiceProvider;
using Core_Layer.Entities.Actors.ServiceProvider.Registeration_Request;
using Core_Layer.Entities.Locations;
using Data_Access_Layer.UnitOfWork;
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

        public SPRegRequestService(IUnitOfWork unitOfWork, IMapper mapper, AddressService addressService) : base(unitOfWork)
        {
            _mapper = mapper;
            _addressService = addressService;
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
    }
}
