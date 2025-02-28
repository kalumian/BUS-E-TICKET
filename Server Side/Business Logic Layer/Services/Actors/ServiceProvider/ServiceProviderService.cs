using AutoMapper;
using Core_Layer.DTOs;
using Core_Layer.Entities.Actors;
using Core_Layer.Entities.Actors.ServiceProvider;
using Core_Layer.Entities.Actors.ServiceProvider.Registeration_Request;
using Core_Layer.Enums;
using Core_Layer.Exceptions;
using Data_Access_Layer.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.Actors.ServiceProvider
{
    public class ServiceProviderService(UserManager<AuthoUser> userManager, IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : BaseUserService(userManager, unitOfWork, httpContextAccessor)
    {
        private IMapper _mapper = mapper;

        public async Task<RegisterServiceProviderDTO> RegisterAsync(
            RegisterServiceProviderDTO registerServiceProviderDTO,
            EnAccountStatus status = EnAccountStatus.PendingVerification)
        {
            // Step 1: Create user account
            AuthoUser user = await CreateUserAccountAsync(registerServiceProviderDTO.Account, status);
            _unitOfWork.AttachEntity(user);

            // Step 2: Map DTO to Entity
            var serviceProviderEntity = _mapper.Map<ServiceProviderEntity>(registerServiceProviderDTO);
            serviceProviderEntity.AccountID = user.Id;
            serviceProviderEntity.Account = user;


            // Step 3: Save Service Provider entity
            await CreateEntityAsync(serviceProviderEntity, saveChanges: true);

            // Step 4: Return the mapped result
            return _mapper.Map<RegisterServiceProviderDTO>(serviceProviderEntity);
        }

        private async Task<AuthoUser> CreateUserAccountAsync(RegisterAccountDTO accountDTO, EnAccountStatus status)
        {
            // Attempt to register user account
            AuthoUser user = await RegisterAsync(accountDTO, status);

            // Throw exception if user creation failed
            if (string.IsNullOrEmpty(user.Id))
                throw new BadRequestException("Failed to create user account.");

            return user;
        }
        public async Task DeletePassifServiceProviderAccountAsync(ServiceProviderEntity serviceProvider)
        {
            var user = await _userManager.FindByIdAsync(serviceProvider.AccountID)
                ?? throw new NotFoundException("User of Pre-ServiceProvider isn't found.");

            if (user.AccountStatus != EnAccountStatus.Inactive)
                throw new InvalidOperationException("Service Provider Account cannot be deleted unless it is inactive.");

            // حذف الحساب
            DeleteEntity(serviceProvider);
            await _userManager.DeleteAsync(user);
        }

        public async Task<ServiceProviderEntity?>? GetServiceProviderByBussinesID(int bussinesID)
        {
            EnsureAdminRole();
            var serviceProvider = await _unitOfWork.ServiceProviders
              .GetAllQueryable()
              .Include(sp => sp.Business) 
              .FirstOrDefaultAsync(sp => sp.BusinessID == bussinesID);
            return serviceProvider;
        }
        public async Task<List<ServiceProviderDTO>> GetAllServiceProvidersAsync()
        {
            try
            {
                var serviceProviders = await _unitOfWork.ServiceProviders
                    .GetAllQueryable()
                     .Include(sp => sp.Account)
                    .Include(sp => sp.Business).ThenInclude(b => b.Address).ThenInclude(a => a.Street).ThenInclude(a => a.City).ThenInclude(c => c.Region).ThenInclude(r => r.Country)
                    .Include(sp => sp.Business).ThenInclude(b => b.Address).ThenInclude(a => a.City).ThenInclude(c=> c.Region).ThenInclude(r => r.Country)
                    .Include(sp => sp.Business).ThenInclude(b => b.ContactInformation)
                    .ToListAsync();

                return _mapper.Map<List<ServiceProviderDTO>>(serviceProviders);
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Failed to retrieve service providers, " + ex.Message);
            }
        }

        public async Task<ServiceProviderDTO> GetServiceProviderByID(string id)
        {
            try
            {
                var serviceProvider = await _unitOfWork.ServiceProviders
                    .GetAllQueryable()
                    .Include(sp => sp.Account)
                    .Include(sp => sp.Business).ThenInclude(b => b.Address).ThenInclude(a => a.Street).ThenInclude(a => a.City).ThenInclude(c => c.Region).ThenInclude(r => r.Country)
                    .Include(sp => sp.Business).ThenInclude(b => b.Address).ThenInclude(a => a.City).ThenInclude(c => c.Region).ThenInclude(r => r.Country)
                    .Include(sp => sp.Business).ThenInclude(b => b.ContactInformation)
                    .Where(sp => sp.AccountID == id)
                    .FirstOrDefaultAsync();

                return _mapper.Map<ServiceProviderDTO>(serviceProvider);
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Failed to retrieve service provider, " + ex.Message);
            }
        }
    }
}
