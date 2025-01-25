using Business_Logic_Layer.Services.Payment;
using Core_Layer.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BUS_E_TICKET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentAccountController(PaymentAccountService paymentAccountService) : ControllerBase
    {
        private readonly PaymentAccountService _paymentAccountService = paymentAccountService;

        [Authorize(Roles = "Provider")]
        [HttpPost("AddPayPalAccount")]
        public async Task<IActionResult> AddPayPalAccount(PayPalAccountDTO dto)
        {
            var payPalAccountDto = await _paymentAccountService.AddPayPalAccountAsync(dto);

            return Ok(new
            {
                IsSuccess = true,
                Message = "PayPal account added successfully.",
                Data = payPalAccountDto
            });
        }
        [Authorize(Roles = "Provider")]
        [HttpGet("GetAllAccounts/{serviceProviderId}")]
        public async Task<IActionResult> GetAllAccounts(int serviceProviderId)
        {
            var accounts = await _paymentAccountService.GetAllAccountsByServiceProviderAsync(serviceProviderId);

            return Ok(new
            {
                IsSuccess = true,
                Message = "Accounts retrieved successfully.",
                Data = accounts
            });
        }
    }
}
