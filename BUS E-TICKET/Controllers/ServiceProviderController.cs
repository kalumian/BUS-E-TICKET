using BUS_E_TICKET.Utilities;
using Business_Logic_Layer.Services.Actors.ServiceProvider;
using Core_Layer.DTOs;
using Core_Layer.Enums;
using Core_Layer.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BUS_E_TICKET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceProviderController(SPRegRequestService sPRegRequestService, ServiceProviderService serviceProviderService   ,SPRegResponseService sPRegResponseService) : ControllerBase
    {
        private readonly SPRegRequestService _SPRegRequestService = sPRegRequestService;
        private readonly SPRegResponseService _SPRegResponseService = sPRegResponseService;
        private readonly ServiceProviderService _serviceProviderService = serviceProviderService;

        [HttpPost("registration/applicate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegistrationApplication(SPRegRequestDTO requestDTO)
        {
            var request = await _SPRegRequestService.CreateApplicationAsync(requestDTO);
            return Ok(ResponeHelper.GetApiRespone(StatusCode: 200, Message: "Service Provider Registration application created successfully",Data: new { request }));
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("applications")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllRegistrationApplication([FromQuery] EnRegisterationRequestStatus? status)
        {
            var requests = _SPRegRequestService.GetAllRegistrationApplication(status);

            return Ok(new ApiResponse
            {
                Message = "Data fetched successfully.",
                Data = requests
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("applications/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRegistrationApplicationByID([FromRoute] int id)
        {
            var requests = await _SPRegRequestService.GetRegistertionApplicationByIDAsync(id);

            return Ok(new ApiResponse
            {
                Message = "Data fetched successfully.",
                Data = requests
            });
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("application/review")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReviewRegistrationRequest(SPRegResponseDTO response)
        {
            var responseId = await _SPRegResponseService.ReviewRegistrationApplicationAsync(response);

            return Ok(new ApiResponse
            {
                Message = "Registration request accepted and created a Service Provider Account successfully.",
                Data = new { ResponseID = responseId }
            });
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> index()
        {
            var serviceProviders = await _serviceProviderService.GetAllServiceProvidersAsync();
            return Ok(ResponeHelper.GetApiRespone(200, "Service Providers were fetched successfully", serviceProviders));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetServiceProviderByID([FromRoute] string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new BadRequestException("ID is required");

            var serviceProvider = await _serviceProviderService.GetServiceProviderByID(id) ?? throw new NotFoundException("Service Provider not found");

            return Ok(ResponeHelper.GetApiRespone(200, "Service Provider was fetched successfully", serviceProvider));
        }
    }
}
