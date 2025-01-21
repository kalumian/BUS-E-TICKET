using BUS_E_TICKET.Utilities;
using Business_Logic_Layer.Services.Actors.ServiceProvider;
using Core_Layer.DTOs;
using Core_Layer.Enums;
using Microsoft.AspNetCore.Authorization;
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
        private SPRegResponseService _SPRegResponseService;

        public ServiceProviderController(SPRegRequestService sPRegRequestService, SPRegResponseService sPRegResponseService) {
            _SPRegRequestService = sPRegRequestService;
            _SPRegResponseService = sPRegResponseService;
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
        [Authorize(Roles = "Admin")]
        [HttpGet("RegistrationRequests")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllServiceProviderRegistrationRequests([FromQuery] EnRegisterationRequestStatus? status)
        {
                var requests = _SPRegRequestService.GetAllRegistrationRequestsAsync(status);

                return Ok(new ApiResponse { IsSuccess= true, Message = "Data was fetched successfully", Data = new {Requests = requests}});
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AcceptRegistrationRequest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AcceptRegistrationRequest(SPRegResponseDTO Respone)
        {
            var ResponseID = await _SPRegResponseService.AcceptRegistrationRequestAsync(Respone);
            return Ok(new ApiResponse
                {
                    IsSuccess = true,
                    Message = "Registration request accepted and created a Service Proivder Account Directly successfully.",
                    Data = new { ResponseID }
                }
            );
        }
    }
}
