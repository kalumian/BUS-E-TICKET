using Core_Layer.DTOs;
using Microsoft.Extensions.Configuration;

namespace BUS_E_TICKET.Utilities
{
    internal class ResponeHelper
    {
        static public ApiResponse GetApiRespone(string Message,bool IsSuccess, object Data, List<string>? Errors = null)
        {
            return new ApiResponse() {
            IsSuccess = IsSuccess,
            Message = Message,
            Data = Data,
            Errors = Errors
            };
        }
        static public TokenConfiguration  GetTokenConfiguration(IConfiguration configuration)
        {
            string? Is = configuration["JWT:Issuer"];
            string? Au = configuration["JWT:Audience"];
            string? Se = configuration["JWT:SecretKey"];
            if (string.IsNullOrEmpty(Is) || string.IsNullOrEmpty(Au) || string.IsNullOrEmpty(Se)) 
            { throw new BadHttpRequestException("There an error in Token Configrutation"); }
            return new TokenConfiguration() {Issuer = Is, Audience = Au, SecretKey = Se};
        }
    }
}
