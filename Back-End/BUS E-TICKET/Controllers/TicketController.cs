using Business_Logic_Layer.Services;
using Core_Layer.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BUS_E_TICKET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController(TicketService ticketService) : Controller
    {
        private readonly TicketService _ticketService = ticketService;
        [HttpGet("GetByReservation/{reservationId}")]
        public async Task<IActionResult> GetByReservation(int reservationId)
        {

            var ticket = await _ticketService.GetTicketByReservationId(reservationId) ??
                throw new NotFoundException("Ticket not found for this reservation ID.");
         
            return Ok(Utilities.ResponeHelper.GetApiRespone(200, "Message Ticket Fetched Successfully", ticket));

        }
    }
}
