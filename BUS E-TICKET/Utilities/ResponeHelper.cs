using Core_Layer.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BUS_E_TICKET.Utilities
{
    internal class ResponeHelper
    {
        static public ApiResponse GetApiRespone(int StatusCode, string? Message = null, object? Data = null, string? Error = null)
        {
            return new ApiResponse() {
            Message = Message,
            Data = Data,
            Error = Error,
            StatusCode = StatusCode
            };
        }
        static public string? GetTokenIssuer(IConfiguration configuration)
        {
            return configuration["JWT:Issuer"];
        }
        static public string? GetTokenAudience(IConfiguration configuration)
        {
            return configuration["JWT:Audience"];
        }
        static public string? GetTokenSecretKey(IConfiguration configuration)
        {
            return configuration["JWT:SecretKey"];
        }
        static public TokenConfiguration  GetTokenConfiguration(IConfiguration configuration)
        {
            string? Is = GetTokenIssuer(configuration);
            string? Au = GetTokenAudience(configuration);
            string? Se = GetTokenSecretKey(configuration);
            if (string.IsNullOrEmpty(Is) || string.IsNullOrEmpty(Au) || string.IsNullOrEmpty(Se)) 
            { throw new BadHttpRequestException("There an error in Token Configrutation"); }
            return new TokenConfiguration() {Issuer = Is, Audience = Au, SecretKey = Se};
        }
    }
}
