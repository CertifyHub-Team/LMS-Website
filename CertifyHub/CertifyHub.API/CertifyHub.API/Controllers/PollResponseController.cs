using CertifyHub.Core.Data;
using CertifyHub.Core.Service;
using CertifyHub.Infra.Service;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CertifyHub.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PollResponseController : ControllerBase
	{
		private readonly IPollResponseService pollResponseService;
		public PollResponseController(IPollResponseService pollResponseService)
		{
			this.pollResponseService = pollResponseService;
		}

		[HttpGet]
		[Route("GetUserPollResponses/{UserID}")]
		public Task<List<Pollresponse>> GetUserPollResponses(int UserID)
		{
			return pollResponseService.GetUserPollResponses(UserID);
		}

		[HttpGet]
		[Route("GetAllResponsesForPoll/{PollID}")]
		public Task<List<Pollresponse>> GetAllResponsesForPoll(int PollID)
		{
			return pollResponseService.GetAllResponsesForPoll(PollID);
		}
		
		[HttpDelete]
		[Route("DeletePollResponse/{PollResponseID}")]
		public void DeletePollResponse(int PollResponseID)
		{
			pollResponseService.DeletePollResponse(PollResponseID);
		}

		[HttpPost]
		[Route("SubmitPollResponse")]
		public void SubmitPollResponse(Pollresponse pollresponse)
		{
			pollResponseService.SubmitPollResponse(pollresponse);
		}
	}
}
