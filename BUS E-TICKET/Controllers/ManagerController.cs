using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Core_Layer.DTOs;
using Core_Layer.Entities.Actors;
using Core_Layer.Enums;
using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using BUS_E_TICKET.Utilities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BUS_E_TICKET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly ManagerService _managerService;
        public ManagerController(ManagerService managerService) { 
            _managerService = managerService;
        }

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> RegisterNewManegerUser([FromBody] RegisterManagerAccountDTO User)
        {
            //Validation
            ValidationHelper.ModelsErrorCollector(ModelState);

            //Registering
            string UserID = await _managerService.RegisterAsync(User);

            //Result
            return Ok(ResponeHelper.GetApiRespone("Manager registered successfully", true, new { UserID }));
        }
        [HttpPost("Login")]
        public async Task<IActionResult> UserLogin([FromBody] UserLoginDTO LoginInfo)
        {
            //Validation
            ValidationHelper.ModelsErrorCollector(ModelState);

            //Try to sign In
            string Token = await _managerService.Login(LoginInfo);

            //Result
            return Ok(ResponeHelper.GetApiRespone("Log-in successfully", true, new { Token }));
        }
    }
}
