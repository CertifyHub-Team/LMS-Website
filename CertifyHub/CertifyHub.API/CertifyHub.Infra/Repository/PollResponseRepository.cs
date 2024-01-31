using CertifyHub.Core.Common;
using CertifyHub.Core.Data;
using CertifyHub.Core.Repository;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Infra.Repository
{
	public class PollResponseRepository:IPollResponseRepository
	{
		private readonly IDbContext dbContext;
		public PollResponseRepository(IDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		public async Task<List<Pollresponse>> GetAllResponsesForPoll(int PollID)
		{
			var f = new DynamicParameters();
			f.Add("P_PollID", PollID, DbType.Int32, ParameterDirection.Input);
			

			IEnumerable<Pollresponse> result = dbContext.Connection.Query<Pollresponse>("PollResponse_Package.GetAllResponsesForPoll", f, commandType: CommandType.StoredProcedure);

			return result.ToList();
		}
		public async Task<List<Pollresponse>> GetUserPollResponses(int UserID)
		{
			var f = new DynamicParameters();
			f.Add("P_UserID", UserID, DbType.Int32, ParameterDirection.Input);


			IEnumerable<Pollresponse> result = dbContext.Connection.Query<Pollresponse>("PollResponse_Package.GetUserPollResponses", f, commandType: CommandType.StoredProcedure);

			return result.ToList();
		}
		
		public void DeletePollResponse(int PollResponseID)
		{
			var d = new DynamicParameters();
			d.Add("P_PollResponseID", PollResponseID, dbType: DbType.Int32, direction: ParameterDirection.Input);
			var result = dbContext.Connection.Execute("PollResponse_Package.DeletePollResponse", d, commandType: CommandType.StoredProcedure);
		}

		public void SubmitPollResponse(Pollresponse pollresponse)
		{
			var c = new DynamicParameters();
			c.Add("P_PollID", pollresponse.Pollid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			c.Add("P_UserID", pollresponse.Userid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			c.Add("P_OptionID", pollresponse.Optionid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			var result = dbContext.Connection.Execute("PollResponse_Package.SubmitPollResponse", c, commandType: CommandType.StoredProcedure);



		}














	}
}
