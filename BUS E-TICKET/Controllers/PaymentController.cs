using Microsoft.AspNetCore.Mvc;
using Business_Logic_Layer.Services.Payment;
using Core_Layer.Exceptions;

namespace BUS_E_TICKET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(PayPalService _payPalService) : ControllerBase
    {
        private readonly PayPalService payPalService = _payPalService;

        [HttpGet("Paypal/ExecutePayment")]
        public async Task<IActionResult> ExecutePayment([FromQuery] int reservationId)
        {

            var paymentConfirmed = await payPalService.ExecutePaymentAsync(reservationId);

            return paymentConfirmed == null
                ? throw new BadRequestException("Payment confirmation failed.")
                :
                Ok(Utilities.ResponeHelper.GetApiRespone(
                    IsSuccess: true,
                    Message: "Payment was successful. Reservation is now confirmed.",
                    Data: paymentConfirmed
                )
            );
        }

        [HttpGet("Paypal/CancelPayment")]
        public async Task<IActionResult> CancelPayment([FromQuery] int reservationId)
        {
            await payPalService.WhenPaymentFaild(reservationId);
            throw new BadRequestException("Payment has been canceled. Please try again.");
        }
    }
}
