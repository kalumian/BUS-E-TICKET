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
        static public JwtSecurityToken TokenHelper(TokenConfiguration config, AuthoUser user, EnUserRole UserRole)
        {
            var claimes = new List<Claim>();
            if (user != null && !string.IsNullOrEmpty(user.UserName))
            {
                claimes.Add(new Claim(ClaimTypes.Name, user.UserName));
            }
            else
            {
                throw new NotFoundException(nameof(user)  + " User or UserName cannot be null.");
            }
            claimes.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claimes.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claimes.Add(new Claim(ClaimTypes.Role, UserRole.ToString()));
            // - Create Key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.SecretKey));
            var sc = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
               issuer: config.Issuer,
               audience: config.Audience,
               claims: claimes,
               expires: DateTime.UtcNow.AddHours(3),
               signingCredentials: sc
            );
            return token;
        }
        static public void Check(EnUserRole role)
        {
            switch (role) {
                case EnUserRole.Admin:
                    _CheckManager();
                    break;
                case EnUserRole.Provider:
                    _CheckServiceProvider();
                    break;
                case EnUserRole.Customer:
                    _CheckCustomer();
                    break;
                default:
                    break;
            }
        }
        static private void _CheckManager()
        {

        }
        static private void _CheckServiceProvider()
        {

        }
        static private void _CheckCustomer()
        {

        }
    }
}
