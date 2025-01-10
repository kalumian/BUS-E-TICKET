using Business_Logic_Layer.Utilities;
using Core_Layer.DTOs;
using Core_Layer.Entities.Actors;
using Core_Layer.Enums;
using Core_Layer.Exceptions;
using Data_Access_Layer.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;

namespace Business_Logic_Layer.Services
{
    public class BaseUserService
    {
        protected readonly  UserManager<AuthoUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public BaseUserService(UserManager<AuthoUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        protected async Task<bool> IsUserExisitByEmail(string Email)
        {
            return await _userManager.FindByEmailAsync(Email) is not null;
        }

        protected void CheckEntityExist<T>(Expression<Func<T, bool>> prediction, string EntityName) where T : class {
            bool exists = _unitOfWork.GetDynamicRepository<T>().Exists(prediction);

            if (!exists)
            {
                EntityName ??= typeof(T).Name;
                throw new BadRequestException($"{EntityName} does not exist.");
            }
        }

        protected async Task<string> RegisterAsync(RegisterAccountDTO registerAccountDTO)
        {
                if (await IsUserExisitByEmail(registerAccountDTO.Email))
                throw new BadRequestException("A user with the same email already exists.");

                var authoUser = new AuthoUser
                    {
                        UserName = registerAccountDTO.UserName,
                        Email = registerAccountDTO.Email,
                        PhoneNumber = registerAccountDTO.PhoneNumber,
                        RegisterationDate = DateTime.Now,
                        AccountStatus = EnAccountStatus.Active
                    };

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
