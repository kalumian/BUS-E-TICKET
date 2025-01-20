using Business_Logic_Layer.Utilities;
using Core_Layer.DTOs;
using Core_Layer.Entities.Actors;
using Core_Layer.Enums;
using Core_Layer.Exceptions;
using Data_Access_Layer.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;

namespace Business_Logic_Layer.Services.Actors
{
    public class BaseUserService : GeneralService
    {
        protected readonly UserManager<AuthoUser> _userManager;
        public BaseUserService(UserManager<AuthoUser> userManager, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _userManager = userManager;
        }
        protected async Task<bool> IsUserExisitByEmail(string Email)
        {
            return await _userManager.FindByEmailAsync(Email) is not null;
        }
        protected async Task<bool> IsUserExistByPhone(string phoneNumber)
        {
            return await Task.FromResult(_userManager.Users.Any(u => u.PhoneNumber == phoneNumber));
        }
        protected async Task<string> RegisterAsync(RegisterAccountDTO registerAccountDTO)
        {
            // Check email
            if (await IsUserExisitByEmail(registerAccountDTO.Email))
                throw new BadRequestException("A user with the same email already exists.");
            // Check phone
            if (await IsUserExistByPhone(registerAccountDTO.PhoneNumber))
                throw new BadRequestException("A user with the same Phone already exists.");
            var authoUser = new AuthoUser
            {
                UserName = registerAccountDTO.UserName,
                Email = registerAccountDTO.Email,
                PhoneNumber = registerAccountDTO.PhoneNumber,
                RegisterationDate = DateTime.Now,
                AccountStatus = EnAccountStatus.Active
            };
            // Entity Validation 
            ValidationHelper.ValidateEntity(authoUser);

            IdentityResult result = await _userManager.CreateAsync(authoUser, registerAccountDTO.Password);

            // Error If Couldn't Create A User
            if (!result.Succeeded) throw new BadRequestException(string.Join(", ", result.Errors.Select(e => e.Description)));

            return authoUser.Id;
        }
        public async Task<JwtSecurityToken> Login(UserLoginDTO LoginInfo, TokenConfiguration config)
        {
            // Cheack Username
            AuthoUser? user = await _userManager.FindByNameAsync(LoginInfo.UserName) ??
                throw new BadRequestException("Login faild. Please make sure Username Or Passworrd are correct.");

            //Get User Role
            EnUserRole UserRole = await _unitOfWork.Managers.GetUserRole(user.Id);

            //Check 
            LoginVerificationHelper.Check(UserRole);

            // Create Token :
            JwtSecurityToken token = LoginVerificationHelper.TokenHelper(config, user, UserRole);

            return token;
        }
        //protected
    }
}
