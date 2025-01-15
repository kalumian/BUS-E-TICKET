using AutoMapper;
using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Utilities;
using Core_Layer.DTOs;
using Core_Layer.Entities;
using Core_Layer.Entities.Actors;
using Core_Layer.Entities.Locations;
using Core_Layer.Enums;
using Core_Layer.Exceptions;
using Data_Access_Layer.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services
{
    public class CustomerService : BaseUserService
    {
        private PersonService _personService;
        private AddressService _addressService;
        private ContactInformationService _contactService;
        private IMapper _mapper;

        public CustomerService(
            UserManager<AuthoUser> userManager,
            IUnitOfWork unitOfWork,
            PersonService personService,
            AddressService addressService,
            IMapper mapper,
            ContactInformationService contactService) : base(userManager, unitOfWork)
        {
            _personService = personService;
            _addressService = addressService;
            _contactService = contactService;
            _mapper = mapper;
        }


        // Main Registration Flow
        public async Task<string> RegisterAsync(RegisterCustomerAccountDTO registerCustomerAccountDTO)
        {
            // Init Entities  
            PersonEntity Person = _mapper.Map<PersonEntity>(registerCustomerAccountDTO.Person);
            AddressEntity Address = _mapper.Map<AddressEntity>(registerCustomerAccountDTO.Address);
            ContactInformationEntity ContactInformation = _mapper.Map<ContactInformationEntity>(registerCustomerAccountDTO.ContactInformation);

            // Entites Validation
            ValidationHelper.ValidateEntity(Person);
            ValidationHelper.ValidateEntity(Address);
            ValidationHelper.ValidateEntity(ContactInformation);

            // Create Entites
            string resultUserID = await RegisterAsync(registerCustomerAccountDTO.Account);
            await CreateEntity<PersonEntity>(Person);
            await CreateEntity<ContactInformationEntity>(ContactInformation);
            await CreateEntity<AddressEntity>(Address);


            CustomerEntity Customer = new CustomerEntity()
            {
                AccountID = resultUserID,
                ContactInformationID = ContactInformation.ContactInformationID,
                AddressID = Address.AddressID,
                PersonID = Person.PersonID
            };
            await CreateEntity<CustomerEntity>(Customer);
            await _unitOfWork.SaveChangesAsync();
            CheckCreatedState<CustomerEntity>(Customer.CustomerID);
            return resultUserID;
        }


    }
}
