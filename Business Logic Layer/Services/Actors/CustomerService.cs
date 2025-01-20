using AutoMapper;
using Business_Logic_Layer.Utilities;
using Core_Layer.DTOs;
using Core_Layer.Entities;
using Core_Layer.Entities.Actors;
using Core_Layer.Entities.Locations;
using Data_Access_Layer.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;


namespace Business_Logic_Layer.Services.Actors
{
    public class CustomerService : BaseUserService
    {
        private IMapper _mapper;

        public CustomerService(
            UserManager<AuthoUser> userManager,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ContactInformationService contactService, IHttpContextAccessor httpContextAccessor) : base(userManager, unitOfWork, httpContextAccessor)
        {
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
            await CreateEntity(Person);
            await CreateEntity(ContactInformation);
            await CreateEntity(Address);


            CustomerEntity Customer = new CustomerEntity()
            {
                AccountID = resultUserID,
                ContactInformationID = ContactInformation.ContactInformationID,
                AddressID = Address.AddressID,
                PersonID = Person.PersonID
            };
            await CreateEntity(Customer);
            await _unitOfWork.SaveChangesAsync();
            CheckCreatedState<CustomerEntity>(Customer.CustomerID);
            return resultUserID;
        }


    }
}
