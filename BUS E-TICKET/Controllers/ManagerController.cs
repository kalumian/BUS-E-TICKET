using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Core_Layer.DTOs;
using Core_Layer.Entities.Actors;
using Core_Layer.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using BUS_E_TICKET.Utilities;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.IdentityModel.Tokens.Jwt;
using Business_Logic_Layer.Services.Actors;
using Microsoft.AspNetCore.Authorization;
using Business_Logic_Layer.Services;
using Core_Layer.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BUS_E_TICKET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController(ManagerService managerService) : ControllerBase
    {
        private readonly ManagerService _managerService = managerService;
        [Authorize(Roles = "Admin")]
        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterNewManegerUser([FromBody] RegisterManagerAccountDTO User)
        {
            //Registering
            var Manager = await _managerService.RegisterAsync(User);

            //Result
            return Ok(ResponeHelper.GetApiRespone(StatusCode: 200, Message: "registered successfully", Data: new { Manager }));
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> index()
        {
            var managers = await _managerService.GetAllManagersAsync();
            return Ok(ResponeHelper.GetApiRespone(200, "Managers was fitched successfully", managers));
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetManagerByID([FromRoute] string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new BadRequestException("ID is required");

            var manager = await _managerService.GetManagerByID(id) ?? throw new NotFoundException("Manager not found");

            return Ok(ResponeHelper.GetApiRespone(200, "Manager was fetched successfully", manager));
        }
        [HttpPut("Update/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateManager([FromRoute] string id, [FromBody] RegisterManagerAccountDTO managerDTO)
        {
            var updatedManager = await _managerService.UpdateManagerAsync(id, managerDTO);
            return Ok(ResponeHelper.GetApiRespone(200, "Manager updated successfully", updatedManager));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManager(string id)
        {
            await _managerService.DeleteManager(id);

            return Ok(new { message = "Manager deleted successfully." });
        }
    }
}
