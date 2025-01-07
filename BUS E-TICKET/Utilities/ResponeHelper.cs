using Core_Layer.DTOs;

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
    }
}
