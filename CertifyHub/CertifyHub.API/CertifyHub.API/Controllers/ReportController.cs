using CertifyHub.Core.Data;
using CertifyHub.Core.DTO;
using CertifyHub.Core.Service;
using CertifyHub.Infra.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CertifyHub.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReportController : ControllerBase
	{
		private readonly IReportService reportService;
		public ReportController(IReportService reportService)
		{
			this.reportService = reportService;
		}
		//[HttpGet]
		//[Route("GetLearnerReport")]
		//public List<LearnerReport> GetLearnerReport()
		//{
		//	return reportService.GetLearnerReport();
		//}

		[HttpGet]
		[Route("GetLearnerReport/{UserID}")]
		public Task<List<LearnerReport>> GetLearnerReport(int UserID)
		{
			return reportService.GetLearnerReport(UserID);
		}
	}
}
