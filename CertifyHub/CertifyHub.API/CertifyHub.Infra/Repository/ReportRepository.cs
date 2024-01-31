using CertifyHub.Core.Common;
using CertifyHub.Core.Data;
using CertifyHub.Core.DTO;
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
	public class ReportRepository : IReportRepository
	{
		private readonly IDbContext dbContext;
		public ReportRepository(IDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		//public List<LearnerReport> GetLearnerReport()
		//{
		//	var result =
		//   dbContext.Connection.Query<LearnerReport>("Report_Package.GetLearnerReport", commandType: CommandType.StoredProcedure);
		//	return result.ToList();
		//}
		
		public async Task<List<LearnerReport>> GetLearnerReport(int UserID)
		{
			var f = new DynamicParameters();
			f.Add("R_UserID", UserID, DbType.Int32, ParameterDirection.Input);
			IEnumerable<LearnerReport> result = dbContext.Connection.Query<LearnerReport>("Report_Package.GetLearnerReport", f, commandType: CommandType.StoredProcedure);

			return result.ToList();
		}
	}
}
