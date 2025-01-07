using Core_Layer.DTOs;
using Core_Layer.Entities.Actors;
using Core_Layer.Enums;
using Core_Layer.Exceptions;
using Data_Access_Layer.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq.Expressions;

namespace Business_Logic_Layer.Services
{
    public abstract class BaseUserService
    {
        protected readonly  UserManager<AuthoUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        protected BaseUserService(UserManager<AuthoUser> userManager, IUnitOfWork unitOfWork)
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
                if (!result.Succeeded) throw new ArgumentException(string.Join(", ", result.Errors.Select(e => e.Description)));

                return authoUser.Id;
        }

        //protected 
    }
}
