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
    public class ServiceProviderService : BaseUserService
    {
        private IMapper _mapper;

        public ServiceProviderService(UserManager<AuthoUser> userManager, IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(userManager, unitOfWork, httpContextAccessor)
        {
            _mapper = mapper;
        }
        public async Task<string> RegisterAsync(RegisterServiceProviderDTO RegisterServicePRoviderAccountDTO, EnAccountStatus Status = EnAccountStatus.Inactive)
        {
            // Try to Create Account 
            string resultUserID = await RegisterAsync(RegisterServicePRoviderAccountDTO.Account, Status);
            if (string.IsNullOrEmpty(resultUserID)) throw new BadRequestException("User's ID Have not Created");

            // Created Manager
            RegisterServicePRoviderAccountDTO.Account.AccountId = resultUserID;
            var ServiceProvider = _mapper.Map<ServiceProviderEntity>(RegisterServicePRoviderAccountDTO);
            await CreateEntity(ServiceProvider);

            return ServiceProvider.AccountID;
        }
        public async void DeletePassifServiceProviderAccount(ServiceProviderEntity serviceProvider)
        {
            CheckRole("Admin");
            AuthoUser? user = await _userManager.FindByIdAsync(serviceProvider.AccountID) ?? throw new NotFoundException("User of Pre-ServiceProvider Isn't Founded");
            if (user.AccountStatus == EnAccountStatus.Inactive) throw new SystemException("Service Provider Account Cannot be deleted");
            DeleteEntity(serviceProvider);
            await _userManager.DeleteAsync(user);
        }
        public async Task<ServiceProviderEntity?>? GetServiceProviderByBussinesID(int bussinesID)
        {
            CheckRole("Admin");
            var ServiceProvider =  await _unitOfWork.ServiceProviders
                .GetAllQueryable()
                .Include(nameof(SPRegRequestEntity.Business))
                .FirstOrDefaultAsync(i=> i.BusinessID == bussinesID) ?? null;
            return ServiceProvider;
        }
    }
}
