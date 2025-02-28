using BUS_E_TICKET.Utilities;
using Business_Logic_Layer.Services;
using Core_Layer.DTOs;
using Core_Layer.Exceptions;
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
    [HttpPost]
    public async Task<IActionResult> AddTrip(TripRegistrationDTO tripDTO)
    {
        var createdTrip = await _tripService.AddTripAsync(tripDTO);
        return Ok(ResponeHelper.GetApiRespone(200, Message: "Trip created successfully.", createdTrip));
    }
    [HttpGet("all/{id?}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllTrips([FromRoute] string? id)
    {
        var trips = await _tripService.GetTripsByProviderAsync(id);
        return Ok(ResponeHelper.GetApiRespone(
            StatusCode: 200,
            Message: "Trips fetched successfully.",
            Data: trips
        ));
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTripById([FromRoute] int id)
    {
        var trip = await _tripService.GetTripByIdAsync(id);

        if (trip == null)
        {
            return NotFound(new { Message = "Trip not found." });
        }

        return Ok(ResponeHelper.GetApiRespone(
            StatusCode: 200,
            Message: "Trip fetched successfully.",
            Data: trip
        ));
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchTrips(
      [FromQuery] int from,
      [FromQuery] int to,
      [FromQuery] int year,
      [FromQuery] int month,
      [FromQuery] int day)
    {
        if (from == 0 || to == 0)
            throw new BadRequestException("City IDs must be provided.");

        var tripDate = new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);

        var trips = await _tripService.SearchTripsByCitiesAndDate(from, to, tripDate);

        if (trips == null || !trips.Any())
            throw new NotFoundException("No trips found matching the criteria");

        return Ok(ResponeHelper.GetApiRespone(
            StatusCode: 200,
            Message: "Trips fetched successfully.",
            Data: trips
        ));
    }


}
