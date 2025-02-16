using Business_Logic_Layer.Utilities;
using Core_Layer.DTOs;
using Core_Layer.Entities.Actors;
using Core_Layer.Enums;
using Core_Layer.Exceptions;
using Core_Layer.Interfaces;
using Data_Access_Layer.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;

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
        public async Task<bool> IsUserExisitByEmail(string Email)
        {
            return await _userManager.FindByEmailAsync(Email) is not null;
        }
        public async Task<bool> IsUserExistByPhone(string phoneNumber)
        {
            return await Task.FromResult(_userManager.Users.Any(u => u.PhoneNumber == phoneNumber));
        }
        public async Task<bool> IsUserExistByUsername(string Username)
        {
            return await _userManager.FindByNameAsync(Username) is not null;
        }
        public async Task<bool> IsUserInformationDuplicate(string email, string phonenumber, string username)
        {
            return await IsUserExisitByEmail(email) ||
                   await IsUserExistByPhone(phonenumber) ||
                   await IsUserExistByUsername(username);
        }
        protected async Task<AuthoUser> RegisterAsync(RegisterAccountDTO registerAccountDTO, EnAccountStatus status = EnAccountStatus.Active)
        {
            // Validate input
            await ValidateAccountAsync(registerAccountDTO);

            if (string.IsNullOrWhiteSpace(registerAccountDTO.Email) || !new EmailAddressAttribute().IsValid(registerAccountDTO.Email))
            {
                throw new BadRequestException("Invalid email format.");
            }

            if (string.IsNullOrWhiteSpace(registerAccountDTO.PhoneNumber) || !Regex.IsMatch(registerAccountDTO.PhoneNumber, @"^\+?\d{9,15}$"))
            {
                throw new BadRequestException("Invalid phone number format.");
            }
            // Map DTO to AuthoUser entity
            var authoUser = new AuthoUser
            {
                UserName = registerAccountDTO.UserName,
                Email = registerAccountDTO.Email,
                PhoneNumber = registerAccountDTO.PhoneNumber,
                RegisterationDate = DateTime.UtcNow,
                AccountStatus = status,
            };

            // Attempt to create the user
            var result = await _userManager.CreateAsync(authoUser, registerAccountDTO.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new BadRequestException($"Failed to register user: {errors}");
            }

            // Attach the user to the current context
            _unitOfWork.AttachEntity(authoUser);

            return authoUser;
        }

        protected async Task<AuthoUser> CreateUserAccountAsync(RegisterAccountDTO accountDTO, EnAccountStatus status = EnAccountStatus.Active)
        {
            // Attempt to register user account
            AuthoUser user = await RegisterAsync(accountDTO, status);

            // Throw exception if user creation failed
            if (string.IsNullOrEmpty(user.Id))
                throw new BadRequestException("Failed to create user account.");

            return user;
        }
        private async Task ValidateAccountAsync(RegisterAccountDTO registerAccountDTO)
        {
            if (await IsUserExisitByEmail(registerAccountDTO.Email))
                throw new BadRequestException("the email is already used.");
            if (await IsUserExistByPhone(registerAccountDTO.PhoneNumber))
                throw new BadRequestException("the phone is already used.");
            if (await IsUserExistByUsername(registerAccountDTO.UserName))
                throw new BadRequestException("the username is already used.");
        }
        public async Task<JwtSecurityToken> Login(UserLoginDTO loginInfo, TokenConfiguration config)
        {
            // Validate login information
            if (string.IsNullOrWhiteSpace(loginInfo.UserName))
                throw new BadRequestException("Username cannot be null or empty.");

            // Retrieve user by username
            var user = await _userManager.FindByNameAsync(loginInfo.UserName)
                ?? throw new BadRequestException("Login failed. Please check your username or password.");

            // Check if account is active
            if (user.AccountStatus != EnAccountStatus.Active)
                throw new BadRequestException("Login failed. Your account is not active.");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginInfo.Password);
            if (!isPasswordValid)
                throw new BadRequestException("Login failed. Please check your username or password.");

            await CheakAccountStatus(user);
            // Get user role
            var userRole = await GetUserRoleAsync(user.Id);

            // Verify user role
            LoginVerificationHelper.Check(userRole);

            // Generate token
            var token = LoginVerificationHelper.TokenHelper(config, user, userRole);

            return token;
        }
        public async Task CheakAccountStatus(AuthoUser user)
        {
            if (user.AccountStatus == EnAccountStatus.Deleted)
                throw new NotFoundException("Account Was Deleted");
            if(user.AccountStatus == EnAccountStatus.Inactive)
                throw new NotFoundException("Account Is Inactive");

        }
        public EnUserRole GetCurrentUserRole()
        {
            // Ensure HttpContext is available
            if (_httpContextAccessor?.HttpContext == null)
                throw new InvalidOperationException("HttpContext is not available.");

            // Retrieve the role claim
            var roleClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role);
            if (roleClaim == null || string.IsNullOrEmpty(roleClaim.Value))
                throw new UnauthorizedAccessException("User role claim not found.");

            // Parse the role to EnUserRole
            return Enum.TryParse(roleClaim.Value, out EnUserRole userRole)
                ? userRole
                : throw new InvalidOperationException("Invalid user role.");
        }

        public async Task<EnUserRole> GetUserRoleAsync(string userId)
        {
            if (await _unitOfWork.Customers.AnyAsync(c => c.AccountID == userId)) return EnUserRole.Customer; 
            if (await _unitOfWork.Managers.AnyAsync(m => m.AccountID == userId)) return EnUserRole.Admin; 
            if (await _unitOfWork.ServiceProviders.AnyAsync(s => s.AccountID == userId)) return EnUserRole.Provider;
            return EnUserRole.Unkown;
        }

        public void CheckRole(string role)
        {
            if (GetCurrentUserRole().ToString() != role)
            {
                new AuthorizationException("Unauthorized: Only managers can create other managers.");
            }
        }
        public async Task ChangeUserStatus(EnAccountStatus Status, string userID)
        {
            var user = await _userManager.FindByIdAsync(userID) ?? throw new NotFoundException($"User with ID '{userID}' not found.");
            user.AccountStatus = Status;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Failed to update user status.");
            }
            _unitOfWork.SaveChanges();
        }

        public void EnsureAdminRole() {
            CheckRole("Admin");
        }
        public void EnsureCustomerRole()
        {
            CheckRole("Customer");
        }
        public void EnsureServiceProvider()
        {
            CheckRole("ServiceProvider");
        }
        public async Task ChangeUserStatusAsync(EnAccountStatus newStatus, string accountId)
        {
            var user = await _userManager.FindByIdAsync(accountId) ?? throw new NotFoundException($"User with ID {accountId} not found.");

            if (user.AccountStatus == newStatus)
                throw new InvalidOperationException($"User is already in the '{newStatus}' status.");

            user.AccountStatus = newStatus;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                throw new InvalidOperationException("Failed to update user status. " +
                    string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        public string GetLoggedInUserId()
        {
            // تحقق من أن HttpContext موجودة
            if (_httpContextAccessor.HttpContext == null)
                throw new InvalidOperationException("HttpContext is not available.");

            // استخراج Claim الخاص بالـ User ID
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User is not authenticated.");

            return userId;
        }
        public void EnsureOwnership<T>(int entityId) where T : class, Core_Layer.Interfaces.IAccount
        {
            // الحصول على معرف المستخدم المسجل الدخول
            string loggedInUserId = GetLoggedInUserId();

            // جلب الكيان بناءً على ID
            var entity = _unitOfWork.GetDynamicRepository<T>().GetById(entityId) ?? throw new NotFoundException($"{typeof(T).Name} with ID {entityId} not found.");

            // التحقق من أن الكيان مملوك للمستخدم المسجل الدخول
            if (entity.AccountID != loggedInUserId)
                throw new UnauthorizedAccessException($"You do not have permission to access this {typeof(T).Name}.");
        }

    }
}
