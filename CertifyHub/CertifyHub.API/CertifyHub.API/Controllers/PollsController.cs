using CertifyHub.Core.Data;
using CertifyHub.Core.Service;
using CertifyHub.Infra.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CertifyHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollsController : ControllerBase
    {
        private readonly IPollsService _pollsService;
        public PollsController(IPollsService pollsService)
        {
            _pollsService = pollsService;
        }




        [HttpGet]
        public Task<List<Poll>> GetAllPolls()
        {
            return _pollsService.GetAllPolls();
        }
        [HttpPost]
        public async void CreatePolls(Poll poll)
        {
            _pollsService.CreatePoll(poll);
        }

        [HttpPut]
        public void UpdateUser(Poll poll)
        {
            _pollsService.UpdatePoll(poll );
        }



        [HttpGet]
        [Route("GetPoll/{id}")]

        public async Task<Poll> GetPollById(int id)
        {
            return await _pollsService.GetPollById(id);
        }

        [HttpDelete("{id}")]
        public async void DeleteUsers(int id)
        {
            _pollsService.DeletePoll(id);
        }


    }
}
