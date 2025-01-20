using BUS_E_TICKET.Utilities;
using Business_Logic_Layer.Services.Actors.ServiceProvider;
using Core_Layer.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BUS_E_TICKET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceProviderController : ControllerBase
    {
        private SPRegRequestService _SPRegRequestService;

        public ServiceProviderController(SPRegRequestService sPRegRequestService) {
            _SPRegRequestService = sPRegRequestService; 
        }
        [HttpPost("registeration/request")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterationRequest(SPRegRequestDTO RequestDTO)
        {
            ValidationHelper.ModelsErrorCollector(ModelState);

            int Request_ID =  await _SPRegRequestService.AddRequestAsync(RequestDTO);

            return Ok(ResponeHelper.GetApiRespone("Service Provider Registeration Requerst Was Created successfully", true, new { Request_ID }));

        }
    }
}
