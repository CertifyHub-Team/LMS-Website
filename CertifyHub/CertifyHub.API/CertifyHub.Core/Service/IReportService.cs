using CertifyHub.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Core.Service
{
	public interface IReportService
	{
		Task<List<LearnerReport>> GetLearnerReport(int UserID);
		//public List<LearnerReport> GetLearnerReport();
	}
}
