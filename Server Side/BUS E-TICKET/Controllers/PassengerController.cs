using BUS_E_TICKET.Utilities;
using Business_Logic_Layer.Services.Actors;
using Core_Layer.DTOs;
using Core_Layer.Entities.Actors;
using Core_Layer.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BUS_E_TICKET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly PasengerService _passengerService;

        public PassengerController(PasengerService passengerService)
        {
            _passengerService = passengerService;
        }

        [Authorize(Roles = "Admin,Provider")]
        [HttpGet]
        public async Task<IActionResult> GetAllPassengers()
        {
            var passengers = _passengerService.GetAll();
            return Ok(ResponeHelper.GetApiRespone(200, "passengers was fitched successfully", passengers));

        }

        [Authorize(Roles = "Admin,Provider")]
        [HttpGet("{nationalID}")]
        public async Task<IActionResult> GetPassengerById(string nationalID)
        {
            var passenger = _passengerService.GetById(nationalID) ?? 
                    throw new NotFoundException("Passenger not found");

            return Ok(ResponeHelper.GetApiRespone(200, "Passenger was fetched successfully", passenger));
        }

    }
}
