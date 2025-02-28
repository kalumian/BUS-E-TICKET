using Microsoft.AspNetCore.Mvc;

namespace BUS_E_TICKET.Utilities
{
    public class HttpContextHelper
    {
        public static string getBaseUrl(ControllerBase controllerBase)
        {
            return $"{controllerBase.HttpContext.Request.Scheme}://{controllerBase.HttpContext.Request.Host}";
        }
    }
}
