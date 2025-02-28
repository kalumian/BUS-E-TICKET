using Business_Logic_Layer.Services;
using Core_Layer.Exceptions;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class InvoiceController(InvoiceService ticketService) : Controller
{
    private readonly InvoiceService _ticketService = ticketService;

    [HttpGet("GetByReservation/{reservationId}")]
    public async Task<IActionResult> GetInvoiceByReservation(int reservationId)
    {
        var invoice = await _ticketService.GetInvoiceByReservationId(reservationId);

        if (invoice == null) throw new NotFoundException("Invoice not found for this reservation ID.");

        return Ok(BUS_E_TICKET.Utilities.ResponeHelper.GetApiRespone(200, "Invoice fetched successfully", invoice));
    }
}
