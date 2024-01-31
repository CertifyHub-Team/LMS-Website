using CertifyHub.Core.Data;
using CertifyHub.Core.DTO;
using CertifyHub.Core.Repository;
using CertifyHub.Core.Service;
using CertifyHub.Infra.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Web;

namespace CertifyHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CvController : ControllerBase
    {
        private readonly ICvService _cvService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Cloudinary _cloudinary;
        private readonly HttpClient _httpClient;

        public CvController(ICvService cvService, IHttpClientFactory httpClientFactory, HttpClient httpClient)
        {
            _cvService = cvService;
            _httpClientFactory = httpClientFactory;
            _httpClient = httpClient;

            Account cloudinaryAccount = new Account(
                "dsmowyioy",
                "264385448398359",
                "66y0b2xzMkyfwImAMPKPENbUj5c"
            );

            _cloudinary = new Cloudinary(cloudinaryAccount);
            _httpClient = httpClient;
        }

        [HttpDelete]
        public void DeleteCV(int id)
        {
            _cvService.DeleteCV(id);
        }

        [HttpGet]
        [Route("GetAllCVs")]
        public Task<List<Cv>> GetAllCVs()
        {
            return _cvService.GetAllCVs();
        }

        [HttpGet]
        [Route("GetCVByQrCode/{qrUrl}")]
        public Task<List<Cv>> GetCVByQrCode(string qrUrl)
        {
            return _cvService.GetCVByQrCode(qrUrl);
        }

        [HttpGet]
        [Route("GetCVDetails/{id}")]
        public Task<Cv> GetCVDetails(int id)
        {
            return _cvService.GetCVDetails(id);
        }

        [HttpGet]
        [Route("GetCVsByUser/{userId}")]
        public Task<List<Cv>> GetCVsByUser(int userId)
        {
            return _cvService.GetCVsByUser(userId);
        }

        [HttpPut]
        public void UpdateCV(Cv cv)
        {
            _cvService.UpdateCV(cv);
        }

        [HttpPost]
        public void UploadCV(Cv cv)
        {
            _cvService.UploadCV(cv);
        }

        [HttpGet]
        [Route("GetUserCv/{userId}")]
        public async Task<UserCvInfo> GetUserCv(int userId)
        {

            return await _cvService.GetUserCvInfo(userId);

        }

        [HttpGet]
        [Route("GetUserCvInfo/{userId}")]
        public async Task<UserCvInfo> GetUserCvInfo(int userId)
        {
            
                var userCvInfo = await _cvService.GetUserCvInfo(userId);

                // Call GenerateCv API
                string generateCvResult = await CallGenerateCvApi(userCvInfo);

                    var result = new
                    {
                        UserCvInfo = userCvInfo,
                        GenerateCvResult = generateCvResult
                    };

            return result.UserCvInfo;

        }

        [HttpGet]
        [Route("GetUserInfo/{userId}")]
        public Task<UserInfo> GetUserInfo(int userId)
        {
            return _cvService.GetUserInfo(userId);
        }


        //-----------------------------------------------------------------------------

        [HttpPost("GenerateCv")]
        public async Task<IActionResult> GenerateCv([FromBody] UserCvInfo userCvInfo)
        {

            try
            {
                
                var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "cvTemplate.html");
                var templateContent = System.IO.File.ReadAllText(templatePath);

                templateContent = templateContent.Replace("{{FIRSTNAME}}", userCvInfo.FIRSTNAME)
                                                   .Replace("{{LASTNAME}}", userCvInfo.LASTNAME)
                                                   .Replace("{{MAJOR}}", userCvInfo.Major)
                                                   .Replace("{{gitHubLink}}", userCvInfo.GitHubLink)
                                                   .Replace("{{linkedintlink}}", userCvInfo.LINKEDINTLINK)
                                                   .Replace("{{username}}", userCvInfo.USERNAME)
                                                   .Replace("{{phonenumber}}", userCvInfo.PHONENUMBER)
                                                   .Replace("{{address}}", userCvInfo.ADDRESS)
                                                   .Replace("{{phonenumber}}", userCvInfo.PHONENUMBER)
                                                   .Replace("{{interests}}", userCvInfo.Interests)
                                                   .Replace("{{education}}", userCvInfo.Education)
                                                   .Replace("{{gpa}}", userCvInfo.GPA.ToString())
                                                   .Replace("{{experience}}", userCvInfo.Experience)
                                                   .Replace("{{projects}}", userCvInfo.Projects)
                                                   .Replace("{{skills}}", userCvInfo.Skills);

                Console.WriteLine("HTML Content:");
                    Console.WriteLine(templateContent);

                    var httpClient = _httpClientFactory.CreateClient();

                    var apiUrl = "https://api.pdf.co/v1/pdf/convert/from/html";

                var apiKey = "sadeenabed12@gmail.com_cWQ73P71LTeakgbC4AuYPe3UHhFX5DT9k2y6sQ1z370M7pfR2iEMjt96pf994M3Wq79U3Q8W5Qg9fZ193Fg2kPIw62v08UOpN5K50R1ZsbO0LJ94wbMK2ZesKSAzQxiWdXgRtex3q4fgCZOPO13GyQ0773";

                    var pdfCoData = new
                    {
                        name = "cv.pdf",
                        html = templateContent,
                        inline = true,
                        xApiKey = apiKey,
                        pageSize = "A3"
                    };


                    var jsonData = JsonSerializer.Serialize(new { html = templateContent });

                    httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);

                    var jsonContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync(apiUrl, jsonContent);

                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("API Response:");
                    Console.WriteLine(responseContent);

                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonSerializer.Deserialize<JsonDocument>(apiResponse);

                    var pdfUrl = responseObject.RootElement.GetProperty("url").GetString();

                    var pdfBytes = await httpClient.GetByteArrayAsync(pdfUrl);

                    var fileName = $"cv_{Guid.NewGuid()}.pdf";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Cvs", fileName);
                    System.IO.File.WriteAllBytes(filePath, pdfBytes);

                    var cloudinaryResult = await UploadPdfToCloudinary(filePath, fileName);

                    var cloudinaryUrl = cloudinaryResult.SecureUrl.AbsoluteUri;

                    string qrCodeImageUrl = await GenerateAndSaveQRCode(cloudinaryUrl);
                   
                    _cvService.UpdateQrCode((int)userCvInfo.USERID, qrCodeImageUrl);

                    var result = new
                    {
                        CloudinaryUrl = cloudinaryUrl,
                        QRCodeUrl = qrCodeImageUrl
                    };

                    return Ok(result);


                }
                else
                {
                    return BadRequest($"Error generating certificate. Status Code: {response.StatusCode}");
                }

            }

            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }

        }

        private async Task<RawUploadResult> UploadPdfToCloudinary(string pdfPath, string fileName)
        {
            byte[] pdfBytes = await System.IO.File.ReadAllBytesAsync(pdfPath);

            using (MemoryStream ms = new MemoryStream(pdfBytes))
            {
                var uploadParams = new RawUploadParams
                {
                    File = new FileDescription(fileName, ms),
                    PublicId = fileName, 
                    Folder = "pdf_folder" 
                };

                return await _cloudinary.UploadAsync(uploadParams);
            }
        }


        private async Task<string> CallGenerateCvApi(UserCvInfo userCvInfo)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var apiUrl = "https://localhost:7000/api/Cv/GenerateCv"; // Adjust with your actual URL

            try
            {
                // Assuming GenerateCv API accepts POST requests with JSON content
                var response = await httpClient.PostAsJsonAsync(apiUrl, userCvInfo);

                // Handle the response as needed
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return content;
                }
                else
                {
                    // Handle error cases
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error calling GenerateCv API. Status Code: {response.StatusCode}. Content: {errorContent}");
                    return null; // Adjust accordingly
                }
            }
            catch (Exception ex)
            {
                // Handle general exceptions (e.g., network issues)
                Console.WriteLine($"Exception calling GenerateCv API: {ex.Message}");
                return null; // Adjust accordingly
            }
        }

        private string SaveImageAndGetUrl(byte[] imageData)
        {
            var fileName = $"qr-{Guid.NewGuid()}.png";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "qrimages", fileName);

            System.IO.File.WriteAllBytes(filePath, imageData);

            return $"/qrimages/{fileName}";
        }



        //private async Task<string> GenerateAndSaveQRCode(string name)
        //{
        //    var apiUrl = $"https://chart.googleapis.com/chart?cht=qr&chs=300x300&chl={HttpUtility.UrlEncode($"{name}")}&choe=UTF-8&chco=B0A695&chf=bg,s,FFFFFF&chld=H";

        //    HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        if (response.Content.Headers.ContentType.MediaType.StartsWith("image"))
        //        {
        //            var imageData = await response.Content.ReadAsByteArrayAsync();
        //            return SaveImageAndGetUrl(imageData);
        //        }
        //        else
        //        {
        //            throw new InvalidOperationException("Unexpected content type received in response.");
        //        }
        //    }
        //    else
        //    {
        //        var errorContent = await response.Content.ReadAsStringAsync();
        //        throw new HttpRequestException($"Error generating QR code. Status Code: {response.StatusCode}, Content: {errorContent}");
        //    }
        //}

        private async Task<string> GenerateAndSaveQRCode(string name)
        {
            var apiUrl = $"https://chart.googleapis.com/chart?cht=qr&chs=300x300&chl={HttpUtility.UrlEncode($"{name}")}&choe=UTF-8&chco=053251&chf=bg,s,FFFFFF&chld=H";
           // var apiUrl = $"https://chart.googleapis.com/chart?chs=150x150&cht=qr&chl={name}";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var contentType = response.Content.Headers.ContentType?.MediaType;
                if (!string.IsNullOrEmpty(contentType) && contentType.Contains("image"))
                {
                    var imageData = await response.Content.ReadAsByteArrayAsync();
                    return SaveImageAndGetUrl(imageData);
                }
                else
                {
                    throw new InvalidOperationException("Unexpected content type received in response.");
                }
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error generating QR code. Status Code: {response.StatusCode}, Content: {errorContent}");
            }
        }
        //private async Task<string> GenerateAndSaveQRCode(string name)
        //{
        //    var apiUrl = $"https://chart.googleapis.com/chart?cht=qr&chs=300x300&chl={HttpUtility.UrlEncode(name)}&choe=UTF-8&chco=B0A695&chf=bg,s,FFFFFF&chld=H";

        //    using (HttpResponseMessage response = await _httpClient.GetAsync(apiUrl))
        //    {
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var contentType = response.Content.Headers.ContentType?.MediaType;
        //            if (!string.IsNullOrEmpty(contentType) && contentType.Contains("image"))
        //            {
        //                var imageData = await response.Content.ReadAsByteArrayAsync();
        //                return SaveImageAndGetUrl(imageData);
        //            }
        //            else
        //            {
        //                throw new InvalidOperationException("Unexpected content type received in response.");
        //            }
        //        }
        //        else
        //        {
        //            var errorContent = await response.Content.ReadAsStringAsync();
        //            throw new HttpRequestException($"Error generating QR code. Status Code: {response.StatusCode}, Content: {errorContent}");
        //        }
        //    }
        //}


    }
}
