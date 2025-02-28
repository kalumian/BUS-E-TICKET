// Controllers/AddressController.cs

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Business_Logic_Layer.Services;
using BUS_E_TICKET.Utilities;
using Core_Layer.Entities.Locations;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController(AddressService addressService) : ControllerBase
    {
        private readonly AddressService _addressService = addressService;

        [HttpGet("countries")]
        public IActionResult GetCountries()
        {
            var countries = _addressService.GetCountries();
            return Ok(ResponeHelper.GetApiRespone(200, "countries fetched successfully", countries));
        }

        [HttpGet("regions")]
        public IActionResult GetRegions([FromQuery] int countryID)
        {
            var regions = _addressService.GetRegionsByCountry(countryID);
            return Ok(ResponeHelper.GetApiRespone(200, "Regions fetched successfully", regions));
        }
        [HttpGet("cities")]
        public IActionResult GetCities([FromQuery] int? regionID)
        {
            IEnumerable<CityEntity> cities;

            if (regionID.HasValue) cities = _addressService.GetCitiesByRegion(regionID.Value);
            
            else cities = _addressService.GetAllCities();
            
            return Ok(ResponeHelper.GetApiRespone(200, "cities fetched successfully", cities));
        }
    }
}
