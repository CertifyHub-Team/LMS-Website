using CertifyHub.Core.Data;
using CertifyHub.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CertifyHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {

        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }
        [HttpPost]
        public void CreateContact(Contact contact)
        {
            _contactService.CreateContact(contact);
        }
        [HttpDelete("{id}")]
        public void DeleteContact(int id)
        {
            _contactService.DeleteContact(id);
        }
        [HttpGet]
        public Task<List<Contact>> GetAllContact()
        {
            return _contactService.GetAllContact();
        }

    }
}
