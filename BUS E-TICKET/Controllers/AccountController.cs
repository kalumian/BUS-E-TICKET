using BUS_E_TICKET.Utilities;
using Business_Logic_Layer.Services.Actors;
using Core_Layer.DTOs;
using Core_Layer.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace BUS_E_TICKET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private BaseUserService _baseUserService;
        private IConfiguration configuration;

        public AccountController(BaseUserService baseUserService, IConfiguration configuration)
        {
            _baseUserService = baseUserService;
            this.configuration = configuration;
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UserLogin([FromBody] UserLoginDTO LoginInfo)
        {
            //Validation
            ValidationHelper.ModelsErrorCollector(ModelState);

            // get configurations
            TokenConfiguration config = ResponeHelper.GetTokenConfiguration(configuration);

            //Try to sign In
            JwtSecurityToken Token = await _baseUserService.Login(LoginInfo, config);

            //Result
            return Ok(ResponeHelper.GetApiRespone("Log-in successfully", true, new { Token }));
        }
    }
}
