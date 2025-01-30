using BUS_E_TICKET.Utilities;
using Business_Logic_Layer.Services;
using Core_Layer.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

[Route("api/[controller]")]
[ApiController]
public class TripController(TripService tripService) : ControllerBase
{
    private readonly TripService _tripService = tripService;

    [Authorize(Roles = "Provider")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost("AddTrip")]
    public async Task<IActionResult> AddTrip(TripRegistrationDTO tripDTO)
    {
        var createdTrip = await _tripService.AddTripAsync(tripDTO);

        return Ok(new ApiResponse
        {
            IsSuccess = true,
            Message = "Trip created successfully.",
            Data = createdTrip
        });
    }
    [HttpGet("GetAllTrips")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllTrips()
    {
        var trips = await _tripService.GetAllTripsAsync();
        return Ok(ResponeHelper.GetApiRespone(
            IsSuccess: true,
            Message: "Trips fetched successfully.",
            Data: trips
        ));
    }

}
