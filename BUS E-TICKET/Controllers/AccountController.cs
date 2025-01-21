using BUS_E_TICKET.Utilities;
using Business_Logic_Layer.Services.Actors;
using Core_Layer.DTOs;
using Core_Layer.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace BUS_E_TICKET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(BaseUserService baseUserService, IConfiguration configuration) : ControllerBase
    {
        private readonly BaseUserService _baseUserService = baseUserService;
        private readonly IConfiguration configuration = configuration;

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UserLogin([FromBody] UserLoginDTO LoginInfo)
        {
            // get configurations
            TokenConfiguration config = ResponeHelper.GetTokenConfiguration(configuration);

            //Try to sign In
            JwtSecurityToken Token = await _baseUserService.Login(LoginInfo, config);

            //Result
            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(Token) });
        }
    }
}
