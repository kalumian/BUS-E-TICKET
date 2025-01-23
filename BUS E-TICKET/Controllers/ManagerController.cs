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
            return Ok(ResponeHelper.GetApiRespone("Manager registered successfully", true, new { Manager }));
        }

    }
}
