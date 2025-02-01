using AutoMapper;
using Business_Logic_Layer.Utilities;
using Core_Layer.DTOs;
using Core_Layer.Entities.Actors;
using Core_Layer.Enums;
using Core_Layer.Exceptions;
using Data_Access_Layer.UnitOfWork;
using Microsoft.AspNetCore.Http;
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

namespace Business_Logic_Layer.Services.Actors
{
    public class ManagerService : BaseUserService
    {
        private IMapper _mapper;

        public ManagerService(UserManager<AuthoUser> userManager, IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(userManager, unitOfWork, httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<RegisterManagerAccountDTO> RegisterAsync(RegisterManagerAccountDTO registerManagerAccountDTO)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Verify role
                //EnsureAdminRole();

                // Verify creator exists
                //CheckEntityExist<ManagerEntity>(i => i.ManagerID == registerManagerAccountDTO.CreatedByID);

                AuthoUser user = await CreateUserAccountAsync(registerManagerAccountDTO.Account);

                // Map and set the manager entity
                var manager = _mapper.Map<ManagerEntity>(registerManagerAccountDTO);
                manager.Account = user;
                // Add manager entity
                await CreateEntityAsync(manager, saveChanges: false);

                // Save all changes
                await _unitOfWork.SaveChangesAsync();
                await transaction.CommitAsync();

                // Return the DTO
                return _mapper.Map<RegisterManagerAccountDTO>(manager);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


    }
}
