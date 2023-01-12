using CommonProject.Result;
using CommonProject.ViewModels.Person;
using Contact.Framework.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contact.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IPersonService _personService;

        public ContactController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("personList")]
        public async Task<Response<List<PersonViewModel>>> GetPeople()
        {
            return await _personService.GetPeople();
        }

        [HttpGet("person/{id}")]
        public async Task<Response<PersonViewModel>> GetPerson([FromRoute] Guid id)
        {
            return await _personService.GetPerson(id);
        }

        [HttpPost("person")]
        public async Task<Response<PersonViewModel>> CreatePerson([FromBody] CreatePersonViewModel person)
        {
            return await _personService.CreatePerson(person);
        }

        [HttpDelete("person/{id}")]
        public async Task<Response<bool>> DeletePerson([FromRoute] Guid id)
        {
            return await _personService.DeletePerson(id);
        }

        [HttpPost("personContact")]
        public async Task<Response<PersonContactViewModel>> CreatePersonContact([FromBody] CreatePersonContactViewModel personContact)
        {
            return await _personService.CreatePersonContact(personContact);
        }

        [HttpDelete("personContact/{id}")]
        public async Task<Response<bool>> DeletePersonContact([FromRoute] Guid id)
        {
            return await _personService.DeletePersonContact(id);
        }
    }
}
