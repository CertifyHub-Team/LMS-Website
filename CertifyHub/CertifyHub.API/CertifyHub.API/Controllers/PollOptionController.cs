using CertifyHub.Core.Data;
using CertifyHub.Core.Repository;
using CertifyHub.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CertifyHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollOptionController : ControllerBase
    {
        private readonly IPollOptionService _pollOptionService;

        public PollOptionController(IPollOptionService pollOptionService)
        {
            _pollOptionService = pollOptionService;
        }


        [HttpPost]
        public async void AddPollOption(Polloption pollOption)
        {
            _pollOptionService.AddPollOption(pollOption);
        }


        [HttpDelete("{optionID}")]
        public async void DeletePollOption(int optionID)
        {
            _pollOptionService.DeletePollOption(optionID);
        }


        [HttpGet]
        [Route("GetPollOptions/{id}")]
        public async Task<List<Polloption>> GetPollOptions(int id)
        {
            return await _pollOptionService.GetPollOptions(id);
        }


        [HttpPut]
        public async void UpdatePollOption(Polloption pollOption)
        {
            _pollOptionService.UpdatePollOption(pollOption);
        }

    }
}
