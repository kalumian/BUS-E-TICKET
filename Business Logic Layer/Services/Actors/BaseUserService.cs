using Business_Logic_Layer.Utilities;
using Core_Layer.DTOs;
using Core_Layer.Entities.Actors;
using Core_Layer.Enums;
using Core_Layer.Exceptions;
using Data_Access_Layer.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Business_Logic_Layer.Services.Actors
{
    public class BaseUserService : GeneralService
    {
        protected readonly UserManager<AuthoUser> _userManager;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public BaseUserService(UserManager<AuthoUser> userManager, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(unitOfWork)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor ?? throw new NotFoundException("_httpContextAccessor is null");
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

        public EnUserRole GetCurrentUserRole()
        {
            // تحقق من أن _httpContextAccessor غير null
            if (_httpContextAccessor == null || _httpContextAccessor.HttpContext == null)
            {
                throw new InvalidOperationException("HttpContext is not available.");
            }

            // احصل على دور المستخدم الحالي
            var roleClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role);
            if (roleClaim == null || string.IsNullOrEmpty(roleClaim.Value))
            {
                throw new UnauthorizedAccessException("User role claim not found.");
            }

            // تحويل الدور إلى النوع EnUserRole
            if (Enum.TryParse(roleClaim.Value, out EnUserRole userRole))
            {
                return userRole;
            }
            else
            {
                throw new InvalidOperationException("Invalid user role.");
            }
        }
        public void CheckRole(string role)
        {
            if (GetCurrentUserRole().ToString() != role)
            {
                new AuthorizationException("Unauthorized: Only managers can create other managers.");
            }
        }
    }
}
