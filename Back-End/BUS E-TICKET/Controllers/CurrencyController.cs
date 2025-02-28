using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business_Logic_Layer.Services;
using Core_Layer.Entities;
using BUS_E_TICKET.Utilities;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController(CurrencyService currencyService) : ControllerBase
    {
        private readonly CurrencyService _currencyService = currencyService;
        [HttpGet]
        public IActionResult GetCurrencies()
        {
            var currencies = _currencyService.GetCurrencies();
            return Ok(ResponeHelper.GetApiRespone(200, "Currencies fetched successfully", currencies));
        }
    }
}
