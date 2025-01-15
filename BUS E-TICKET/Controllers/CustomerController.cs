using BUS_E_TICKET.Utilities;
using Business_Logic_Layer.Services;
using Core_Layer.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BUS_E_TICKET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterNewCustomerUser([FromBody] RegisterCustomerAccountDTO user)
        {
            // التحقق من صحة البيانات
            ValidationHelper.ModelsErrorCollector(ModelState);

            // تسجيل العميل
            string userId = await _customerService.RegisterAsync(user);

            // نتيجة العملية
            return Ok(ResponeHelper.GetApiRespone("Customer registered successfully", true, new { userId }));
        }
    }
}
