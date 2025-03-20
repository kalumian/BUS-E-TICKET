using AutoMapper;
using Business_Logic_Layer.Utilities;
using Core_Layer.DTOs;
using Core_Layer.Entities;
using Core_Layer.Entities.Actors;
using Core_Layer.Entities.Locations;
using Core_Layer.Exceptions;
using Data_Access_Layer.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Business_Logic_Layer.Services.Actors
{
    public class CustomerService(
        UserManager<AuthoUser> userManager,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        AddressService addressService,
        PersonService personService,
        ContactInformationService contactService, IHttpContextAccessor httpContextAccessor) : UserService(userManager, unitOfWork, httpContextAccessor)
    {
        private readonly IMapper _mapper = mapper;
        private readonly PersonService _personService = personService;

        // Main Registration Flow
        public async Task<RegisterCustomerAccountDTO> RegisterAsync(RegisterCustomerAccountDTO registerCustomerAccountDTO)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var person = await _personService.GetAndUpdateOrCreatePersonWithContactAsync(registerCustomerAccountDTO.Person);

                var address = _mapper.Map<AddressEntity>(registerCustomerAccountDTO.Address);

                await CreateEntityAsync(address, saveChanges: true);

                AuthoUser user = await CreateUserAccountAsync(registerCustomerAccountDTO.Account);


                var customer = _mapper.Map<CustomerEntity>(registerCustomerAccountDTO);
                customer.Account = user;
                customer.Person = person;
                customer.Address = address;

                // Save customer entity
                await CreateEntityAsync(customer, saveChanges: true, validate: false);
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
        public async Task<List<CustomerDTO>> GetAllCustomersAsync()
        {
            var customers = await _unitOfWork.Customers
                    .GetAllQueryable()
                    .Include(m => m.Account)
                    .Include(c => c.Address)
                        .ThenInclude(a => a.City)
                    .Include(c => c.Person)
                        .ThenInclude(p => p.ContactInformation)
                     .ToListAsync();
            return _mapper.Map<List<CustomerDTO>>(customers);
        }     public async Task<CustomerDTO> GetCustomerByID(string id)
        {
            var customer = await _unitOfWork.Customers
                    .GetAllQueryable()
                    .Include(m => m.Account)
                    .Include(c => c.Address)
                        .ThenInclude(a => a.City)
                    .Include(c => c.Person)
                        .ThenInclude(p => p.ContactInformation)
                     .FirstOrDefaultAsync(e => e.AccountID == id) ?? throw new NotFoundException("Customer Is'nt Founded");
            return _mapper.Map<CustomerDTO>(customer);
        }
    }
}