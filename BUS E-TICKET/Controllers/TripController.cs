using Business_Logic_Layer.Services;
using Core_Layer.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class TripController(TripService tripService) : ControllerBase
{
    private readonly TripService _tripService = tripService;

    [Authorize(Roles = "Provider")]
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
}
