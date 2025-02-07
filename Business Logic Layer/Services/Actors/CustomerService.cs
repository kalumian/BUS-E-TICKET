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
    public class CustomerService(
        UserManager<AuthoUser> userManager,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        AddressService addressService,
        ContactInformationService contactService, IHttpContextAccessor httpContextAccessor) : BaseUserService(userManager, unitOfWork, httpContextAccessor)
    {
        private readonly IMapper _mapper = mapper;
        private readonly AddressService _addressService = addressService;


        // Main Registration Flow
        public async Task<RegisterCustomerAccountDTO> RegisterAsync(RegisterCustomerAccountDTO registerCustomerAccountDTO)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Map entities
                var person = _mapper.Map<PersonEntity>(registerCustomerAccountDTO.Person);
                var address = _mapper.Map<AddressEntity>(registerCustomerAccountDTO.Address);
                var contactInfo = _mapper.Map<ContactInformationEntity>(registerCustomerAccountDTO.ContactInformation);

                // Validate entities
                ValidationHelper.ValidateEntities([person, address, contactInfo]);

                // Register user
                AuthoUser user = await CreateUserAccountAsync(registerCustomerAccountDTO.Account);

                // Use CreateEntityAsync for dynamic entity creation
                await CreateEntityAsync(person);
                await CreateEntityAsync(address);
                await CreateEntityAsync(contactInfo);

                // Map and set customer entity
                var customer = _mapper.Map<CustomerEntity>(registerCustomerAccountDTO);
                customer.Account = user;
                customer.Person = person;
                customer.Address = address;
                customer.ContactInformation = contactInfo;

                // Save customer entity
                await CreateEntityAsync(customer);

                // Save all changes in one transaction
                await _unitOfWork.SaveChangesAsync();
                await transaction.CommitAsync();

                // Return DTO
                return _mapper.Map<RegisterCustomerAccountDTO>(customer);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


    }
}
