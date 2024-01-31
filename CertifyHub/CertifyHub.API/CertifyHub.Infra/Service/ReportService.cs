using CertifyHub.Core.DTO;
using CertifyHub.Core.Repository;
using CertifyHub.Core.Service;
using CertifyHub.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Infra.Service
{
	public class ReportService:IReportService
	{
		private readonly IReportRepository reportRepository;

		public ReportService(IReportRepository reportRepository)
		{
			this.reportRepository = reportRepository;
		}

		public Task<List<LearnerReport>> GetLearnerReport(int UserID)
		{
			return reportRepository.GetLearnerReport(UserID);
		}
	}
	
}
