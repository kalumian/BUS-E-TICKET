using BUS_E_TICKET.Utilities;
using Business_Logic_Layer.Services.Actors;
using Core_Layer.DTOs;
using Core_Layer.Exceptions;
using Microsoft.AspNetCore.Authorization;
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
            var Customer = await _customerService.RegisterAsync(user);

            return Ok(ResponeHelper.GetApiRespone(Message: "Customer registered successfully", Data: new { Customer }, StatusCode: 200));
        }
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> index()
        {
            var customers= await _customerService.GetAllCustomersAsync();
            return Ok(ResponeHelper.GetApiRespone(200, "Customer was fitched successfully", customers));
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetManagerByID([FromRoute] string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new BadRequestException("ID is required");

            var customer = await _customerService.GetCustomerByID(id);

            return Ok(ResponeHelper.GetApiRespone(200, "Customer was fetched successfully", customer));
        }
        //[HttpPut("Update/{id}")]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> UpdateManager([FromRoute] string id, [FromBody] RegisterManagerAccountDTO managerDTO)
        //{
        //    var updatedManager = await _managerService.UpdateManagerAsync(id, managerDTO);
        //    return Ok(ResponeHelper.GetApiRespone(200, "Manager updated successfully", updatedManager));
        //}
    }
}
