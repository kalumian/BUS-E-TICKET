﻿using AutoMapper;
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
            EnAccountStatus status = EnAccountStatus.Inactive)
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
        
    }
}
