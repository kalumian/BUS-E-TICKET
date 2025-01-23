using Core_Layer.DTOs;
using Core_Layer.Entities.Actors;
using Core_Layer.Enums;
using Core_Layer.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Business_Logic_Layer.Utilities
{
    internal class LoginVerificationHelper
    {
        public static JwtSecurityToken TokenHelper(TokenConfiguration config, AuthoUser user, EnUserRole userRole)
        {
            // Validate input
            if (user == null || string.IsNullOrEmpty(user.UserName))
                throw new NotFoundException("User or UserName cannot be null.");

                // Create claims
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, userRole.ToString())
        };

                // Create key and signing credentials
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.SecretKey));
                var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // Create the token
                return new JwtSecurityToken(
                    issuer: config.Issuer,
                    audience: config.Audience,
                    claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: signingCredentials
            );
        }
        public static void Check(EnUserRole role)
        {
            switch (role)
            {
                case EnUserRole.Admin:
                    EnsureManagerAccess();
                    break;
                case EnUserRole.Provider:
                    EnsureServiceProviderAccess();
                    break;
                case EnUserRole.Customer:
                    EnsureCustomerAccess();
                    break;
                default:
                    throw new UnauthorizedAccessException("Invalid user role.");
            }
        }
        // Helper methods for role-specific checks
        private static void EnsureManagerAccess()
        {
            // Add manager-specific validation logic
        }

        private static void EnsureServiceProviderAccess()
        {
            // Add service provider-specific validation logic
        }

        private static void EnsureCustomerAccess()
        {
            // Add customer-specific validation logic
        }
    }
}
