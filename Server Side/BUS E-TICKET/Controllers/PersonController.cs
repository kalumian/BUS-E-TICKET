using BUS_E_TICKET.Utilities;
using Business_Logic_Layer.Services.Actors;
using Core_Layer.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BUS_E_TICKET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController(PersonService personService) : ControllerBase
    {
        private PersonService _personService = personService;

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{NationalID}")]
        public async Task<IActionResult> GetPersonByNationalID([FromRoute]string NationalID)
        {
            var person = await _personService.GetPersonAsync(NationalID) ??
                throw new NotFoundException($"Person with {NationalID} is't Exsist");
            return Ok(ResponeHelper.GetApiRespone(200, Message: "Person was fetched successfully.", person));
        }
    }
}
