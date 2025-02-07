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
    public class ServiceProviderController(SPRegRequestService sPRegRequestService, SPRegResponseService sPRegResponseService) : ControllerBase
    {
        private readonly SPRegRequestService _SPRegRequestService = sPRegRequestService;
        private readonly SPRegResponseService _SPRegResponseService = sPRegResponseService;

        [HttpPost("registration/request")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegistrationRequest(SPRegRequestDTO requestDTO)
        {
            var request = await _SPRegRequestService.CreateRequestAsync(requestDTO);
            return Ok(ResponeHelper.GetApiRespone(StatusCode: 200, Message: "Service Provider Registration Request created successfully",Data: new { request }));
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("RegistrationRequests")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllServiceProviderRegistrationRequests([FromQuery] EnRegisterationRequestStatus? status)
        {
            var requests = _SPRegRequestService.GetAllRegistrationRequests(status);

            return Ok(new ApiResponse
            {
                Message = "Data fetched successfully.",
                Data = new { Requests = requests }
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AcceptRegistrationRequest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AcceptRegistrationRequest(SPRegResponseDTO response)
        {
            var responseId = await _SPRegResponseService.AcceptRegistrationRequestAsync(response);

            return Ok(new ApiResponse
            {
                Message = "Registration request accepted and created a Service Provider Account successfully.",
                Data = new { ResponseID = responseId }
            });
        }

    }
}
