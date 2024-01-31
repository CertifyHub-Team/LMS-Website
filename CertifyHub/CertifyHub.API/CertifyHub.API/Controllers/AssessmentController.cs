using CertifyHub.Core.Data;
using CertifyHub.Core.Service;
using CertifyHub.Infra.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CertifyHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentController : ControllerBase
    {
        private readonly IAssessmentService _assessmentService;

        public AssessmentController(IAssessmentService assessmentService)
        {
            _assessmentService = assessmentService;
        }

        [HttpGet]
        [Route("AssessmentCountInSection/{sectionId}")]
        public Task<int> CountAssessmentsBySection(int sectionId)
        {
            return _assessmentService.CountAssessmentsBySection(sectionId);
        }

        [HttpGet]
        [Route("AssessmentCountByType/{AssessmentType}")]
        public Task<int> CountAssessmentsByType(string AssessmentType)
        {
            return _assessmentService.CountAssessmentsByType(AssessmentType);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAssessment([FromForm] Assessment assessment)
        {
            try
            {
                var files = Request.Form.Files;

                if (files != null && files.Count > 0)
                {
                    var file = files[0];

                    // Use the assessment title as part of the file name
                    var fileName = $"Assignment.pdf";

                    var filePath = Path.Combine("D:\\Downloads\\CertifyHub 2\\CertifyHub\\CertifyHub\\src\\assets\\uploads", fileName);

                    // Save the file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Save the file name to the database
                    assessment.Attachfile = fileName;

                    _assessmentService.CreateAssessment(assessment);
                }
                else
                {
                    _assessmentService.CreateAssessment(assessment);
                }

                return Ok(); // Assuming success, you can adjust the response accordingly
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal Server Error"); // Adjust the response for error cases
            }
        }



        [HttpDelete("{Assessmentid}")]
        public void DeleteAssessment(int Assessmentid)
        {
            _assessmentService.DeleteAssessment(Assessmentid);
        }

        [HttpGet]
        [Route("FilterAssessmentsByDate")]
        public Task<List<Assessment>> FilterAssessmentsByDate(DateTime startDate, DateTime endDate)
        {
            return _assessmentService.FilterAssessmentsByDate(startDate, endDate);
        }

        [HttpGet]
        [Route("GetUpcomingAssessments")]
        public Task<List<Assessment>> GetUpcomingAssessments()
        {
            return _assessmentService.GetUpcomingAssessments();
        }

        [HttpGet]
        [Route("ListAssessmentsByType/{AssessmentType}")]
        public Task<List<Assessment>> ListAssessmentsByType(string AssessmentType)
        {
            return _assessmentService.ListAssessmentsByType(AssessmentType);
        }

        [HttpPut]
        public void UpdateAssessment(Assessment assessment)
        {
            _assessmentService.UpdateAssessment(assessment);
        }

        [HttpGet]
        [Route("GetAssessmentBysectionId/{section_id}")]
        public Task<List<Assessment>> GetAssessmentBysectionId(int section_id)
        {
            return _assessmentService.GetAssessmentBysectionId(section_id);
        }
    }
}
