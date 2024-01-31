using CertifyHub.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Core.Service
{
	public interface IPollResponseService
	{
		
		Task<List<Pollresponse>> GetAllResponsesForPoll(int PollID);
		Task<List<Pollresponse>> GetUserPollResponses(int UserID);
		
		public void SubmitPollResponse(Pollresponse pollresponse);
		public void DeletePollResponse(int PollResponseID);
	}
}
