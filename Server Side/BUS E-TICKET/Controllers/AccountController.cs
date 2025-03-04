using BUS_E_TICKET.Utilities;
using Business_Logic_Layer.Services.Actors;
using Core_Layer.DTOs;
using Core_Layer.Enums;
using Core_Layer.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
            TokenConfiguration config = ResponeHelper.GetTokenConfiguration(configuration);

            JwtSecurityToken Token = await _baseUserService.Login(LoginInfo, config);

            return Ok(ResponeHelper.GetApiRespone(StatusCode: 200, Message: "User was logined successfuly", Data: new { Token = new JwtSecurityTokenHandler().WriteToken(Token) }));
        }

    }
}
