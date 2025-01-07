using AutoMapper;
using Business_Logic_Layer.Interfaces;
using Core_Layer.DTOs;
using Core_Layer.Entities.Actors;
using Core_Layer.Enums;
using Core_Layer.Exceptions;
using Data_Access_Layer.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services
{
    public class ManagerService : BaseUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ManagerService(UserManager<AuthoUser> userManager, IUnitOfWork unitOfWork, IMapper Mapper) : base(userManager, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = Mapper;
        }
        

        public async Task<string> RegisterAsync(RegisterManagerAccountDTO registerManagerAccountDTO)
        {

            //Error If CreatedBy isn't Exists
            CheckEntityExist<ManagerEntity>(i => i.ManagerID == registerManagerAccountDTO.CreatedByID, "Manager Creater");

            // Try to Create Account 
            var accountDto = _mapper.Map<RegisterAccountDTO>(registerManagerAccountDTO);
            string resultUserID = await RegisterAsync(accountDto);
            if (string.IsNullOrEmpty(resultUserID)) throw new BadRequestException("User's ID Have not Created");

            // Created Manager
            var manager = new ManagerEntity { AccountID = resultUserID, CreatedByID = registerManagerAccountDTO.CreatedByID };

            await _unitOfWork.Managers.AddAsync(manager);
            await _unitOfWork.SaveChangesAsync();

            return resultUserID;
        }

        public async Task<string> Login(UserLoginDTO LoginInfo)
        {
            // Cheack Username
            AuthoUser? user = await _userManager.FindByNameAsync(LoginInfo.UserName) ??
                throw new BadRequestException("Login faild. Please make sure Username Or Passworrd are correct.");

            //Cheack Password
            bool PasswordIsMatch = await _userManager.CheckPasswordAsync(user, LoginInfo.Password);
            if (!PasswordIsMatch) throw new BadRequestException("Login faild. Please make sure Username Or Passworrd are correct.");

            return "Token";
        }
    }
}
