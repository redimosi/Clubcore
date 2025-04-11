using Clubcore.Domain.Models;
using Clubcore.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Clubcore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController(IPersonService personService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllPersons()
        {
            var persons = await personService.GetAllPersonsAsync();
            return Ok(persons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonById(Guid id)
        {
            var person = await personService.GetPersonByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> AddPerson([FromBody] PersonDto personDto)
        {
            await personService.AddPersonAsync(personDto);
            return CreatedAtAction(nameof(GetPersonById), new { id = personDto.PersonId }, personDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(Guid id, [FromBody] PersonDto personDto)
        {
            if (id != personDto.PersonId)
            {
                return BadRequest();
            }
            await personService.UpdatePersonAsync(personDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            await personService.DeletePersonAsync(id);
            return NoContent();
        }
    }
}
