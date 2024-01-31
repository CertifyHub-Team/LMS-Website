using CertifyHub.Core.Repository;
using CertifyHub.Core.Service;
using CertifyHub.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CertifyHub.Core.Data;
using CertifyHub.Core.DTO;



namespace CertifyHub.Infra.Service
{
	public class PollResponseService:IPollResponseService
	{
		private readonly IPollResponseRepository pollResponseRepository;

		public PollResponseService(IPollResponseRepository pollResponseRepository)
		{
			this.pollResponseRepository = pollResponseRepository;
		}

		public void DeletePollResponse(int PollResponseID)
		{
			pollResponseRepository.DeletePollResponse(PollResponseID);
		}

		public void SubmitPollResponse(Pollresponse pollresponse)
		{
		
		  pollResponseRepository.SubmitPollResponse(pollresponse);
			
		}


		public Task<List<Pollresponse>> GetAllResponsesForPoll(int PollID)
		{
			return pollResponseRepository.GetAllResponsesForPoll(PollID);
		}

		Task<List<Pollresponse>> IPollResponseService.GetUserPollResponses(int UserID)
		{
			return pollResponseRepository.GetUserPollResponses(UserID);
		}
	}
}
