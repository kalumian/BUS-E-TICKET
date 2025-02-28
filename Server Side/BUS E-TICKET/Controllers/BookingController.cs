using BUS_E_TICKET.Utilities;
using Core_Layer.DTOs;
using Core_Layer.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BUS_E_TICKET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController(ReservationService reservationService) : ControllerBase
    {
        private readonly ReservationService _reservationService = reservationService;

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationDTO reservationDTO)
        {
            var baseApiUrl = "http://localhost:3000/#/";
            var reservation = await _reservationService.CreateReservationAsync(reservationDTO, baseApiUrl);

            return Ok(ResponeHelper.GetApiRespone(
                StatusCode:200,
                Message: "Reservation created successfully. Complete payment using the provided link.",
                Data: reservation
            ));
        }

        [HttpGet("{pnr}")]
        public async Task<IActionResult> GetReservationByPNR(string pnr)
        {
            var reservation = await _reservationService.GetReservationByPNRAsync(pnr);

            if (reservation == null) throw new NotFoundException("Booking not found for the provided PNR.");


            return Ok(ResponeHelper.GetApiRespone(
                StatusCode: 200,
                Message: "Reservation fetched successfully.",
                Data: reservation
            ));
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _reservationService.GetAllReservationsAsync();

            return Ok(ResponeHelper.GetApiRespone(
                StatusCode: 200,
                Message: "All reservations fetched successfully.",
                Data: reservations
            ));
        }

        [HttpGet("trip/{tripId}")]
        public async Task<IActionResult> GetReservationsByTripId(int tripId)
        {
            var reservations = await _reservationService.GetReservationsByTripIdAsync(tripId);

            return Ok(ResponeHelper.GetApiRespone(
                StatusCode: 200,
                Message: "Reservations for the trip fetched successfully.",
                Data: reservations
            ));
        }

    }
}
