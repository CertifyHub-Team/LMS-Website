using CertifyHub.Core.DTO;
using CertifyHub.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace CertifyHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentGradeController : ControllerBase
    {
        private readonly IStudentGradeService _studentGradeService;

        public StudentGradeController(IStudentGradeService studentGradeService)
        {
            _studentGradeService = studentGradeService;
        }

        [HttpGet("GetStudentGrade")]
        public async Task<CertificateDto> GetStudentGrade(int userId, int sectionId)
        {
           
           var result = await _studentGradeService.GetStudentGradeInfo(userId, sectionId);
            return result.FirstOrDefault();

        }

        [HttpGet("GetStudentGradeInfo")]
        public async Task<ActionResult<List<CertificateDto>>> GetStudentGradeInfo(int userId, int sectionId)
        {
            try
            {
                var userCvInfo =  GetUserCvInfo(userId);

                var result = await _studentGradeService.GetStudentGradeInfo(userId, sectionId);



                // Pass the result to the GenerateCertificate API
                var pdfBytes = await CallGenerateCertificateApi(result[0]);

                // Return the PDF content
                return File(pdfBytes, "application/pdf", "certificate.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        private async Task<UserCvInfo> GetUserCvInfo(int userId)
        {
            var apiUrl = $"https://localhost:7000/api/Cv/GetUserCvInfo/{userId}";

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var userCvInfoJson = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<UserCvInfo>(userCvInfoJson);
                }
                else
                {
                    // Handle error cases
                    throw new HttpRequestException($"Error calling GetUserCvInfo API. Status Code: {response.StatusCode}");
                }
            }
        }


        private async Task<byte[]> CallGenerateCertificateApi(object inputData)
        {
            var apiUrl = "https://localhost:7000/api/Certificate/GenerateCertificate";

            using (var httpClient = new HttpClient())
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(inputData), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(apiUrl, jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsByteArrayAsync();
                }
                else
                {
                    throw new HttpRequestException("eeeeeeeeeeeeeeeeeeerrrrrrrrrrrrrrrrrroooooooorrrrrrrrrrrrrrr.");
                }
            }

        }
    }
}