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
        public async Task<string> RegisterAsync(RegisterManagerAccountDTO registerManagerAccountDTO)
        {
            CheckRole(EnUserRole.Admin.ToString()); 
            //Error If CreatedBy isn't Exists
            CheckEntityExist<ManagerEntity>(i => i.ManagerID == registerManagerAccountDTO.CreatedByID);

            // Try to Create Account 
            string resultUserID = await RegisterAsync(registerManagerAccountDTO.Account);
            if (string.IsNullOrEmpty(resultUserID)) throw new BadRequestException("User's ID Have not Created");

            // Created Manager
            registerManagerAccountDTO.Account.AccountId = resultUserID;
            var manager = _mapper.Map<ManagerEntity>(registerManagerAccountDTO);
            
            await _unitOfWork.Managers.AddAsync(manager);
            await _unitOfWork.SaveChangesAsync();

            return resultUserID;
        }


    }
}
