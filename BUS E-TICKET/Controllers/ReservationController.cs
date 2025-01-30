using BUS_E_TICKET.Utilities;
using Core_Layer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BUS_E_TICKET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController(ReservationService reservationService) : ControllerBase
    {
        private readonly ReservationService _reservationService = reservationService;

        [HttpPost("CreateReservation")]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationDTO reservationDTO)
        {
            var baseApiUrl = HttpContextHelper.getBaseUrl(this);
            var reservation = await _reservationService.CreateReservationAsync(reservationDTO, baseApiUrl);

            return Ok(ResponeHelper.GetApiRespone(
                IsSuccess: true,
                Message: "Reservation created successfully. Complete payment using the provided link.",
                Data: reservation
            ));
        }
    }
}
