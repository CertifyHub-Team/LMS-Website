using CertifyHub.Core.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CertifyHub.Core.Service;
using CertifyHub.Infra.Repository;
using System.Runtime.InteropServices;
using CertifyHub.Core.Data;
using MimeKit;
using System.Reflection.Metadata;
using MailKit.Net.Smtp;

namespace CertifyHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Cloudinary _cloudinary;
        private readonly UsersRepository _usersRepository;
        private readonly CertificateRepository _certificateRepository;
        private readonly CourseRepository _courseRepository;
        private readonly ICertificateService _certificateService;


        public CertificateController(IHttpClientFactory httpClientFactory, UsersRepository usersRepository, CertificateRepository certificateRepository, CourseRepository courseRepository, ICertificateService certificateService)
        {
            _httpClientFactory = httpClientFactory;
            _usersRepository = usersRepository;
            _certificateRepository = certificateRepository;
            _certificateService = certificateService;
            _courseRepository = courseRepository;

            Account cloudinaryAccount = new Account(
                "dsmowyioy",
                "264385448398359",
                "66y0b2xzMkyfwImAMPKPENbUj5c"
            );

            _cloudinary = new Cloudinary(cloudinaryAccount);
        }

        [HttpPost("GenerateCertificate")]
        public async Task<IActionResult> GenerateCertificate([FromBody] CertificateDto certificateDto)
        {

            try
            {
                  var relativeImagePath = certificateDto.QRCODEURL.TrimStart('/');
                    var imageFolderPath = "D:\\Training\\VisualStudioProjects\\CertifyHub.API\\CertifyHub.API"; //*************************
                    var imagePath = Path.Combine(imageFolderPath, relativeImagePath);

                    var uploadResult = await UploadToCloudinary(imagePath);

                    if (uploadResult.Error != null)
                    {
                        return BadRequest($"Error uploading photo to Cloudinary: {uploadResult.Error.Message}");
                    }

                    var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "CertificateTemplate.html");
                    var templateContent = System.IO.File.ReadAllText(templatePath);

                    templateContent = templateContent.Replace("{{FIRSTNAME}}", certificateDto.FIRSTNAME)
                                                     .Replace("{{LASTNAME}}", certificateDto.LASTNAME)
                                                     .Replace("{{CourseName}}", certificateDto.CourseName)
                                                     .Replace("{{TOTAL_GRADE_SUM}}", certificateDto.Totalgrade.ToString())
                                                     .Replace("{{DATE}}", DateTime.Now.ToString("dd MMM, yyyy"))
                                                     .Replace("{{QRCODEURL}}", uploadResult.SecureUrl.AbsoluteUri);

                    Console.WriteLine("HTML Content:");
                    Console.WriteLine(templateContent);

                    var httpClient = _httpClientFactory.CreateClient();

                    var apiUrl = "https://api.pdf.co/v1/pdf/convert/from/html";

                    var apiKey = "sadeenabed12@gmail.com_cWQ73P71LTeakgbC4AuYPe3UHhFX5DT9k2y6sQ1z370M7pfR2iEMjt96pf994M3Wq79U3Q8W5Qg9fZ193Fg2kPIw62v08UOpN5K50R1ZsbO0LJ94wbMK2ZesKSAzQxiWdXgRtex3q4fgCZOPO13GyQ0773"; // Replace with your PDF.co API key

                    var pdfCoData = new
                    {
                        name = "certificate.pdf",
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

                        // Save the PDF file locally
                        var certificateFolderPath = "D:\\Training\\VisualStudioProjects\\CertifyHub.API\\CertifyHub.API\\PdfCertificates";
                        var pdfFileName = $"certificate_{certificateDto.USERID}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
                        var pdfFilePath = Path.Combine(certificateFolderPath, pdfFileName);
                        await System.IO.File.WriteAllBytesAsync(pdfFilePath, pdfBytes);

                        var cloudinaryUploadResult = await UploadPdfToCloudinary(pdfBytes, pdfFileName);

                        var relativePath = $"PdfCertificates/{pdfFileName}";

                        var user = _usersRepository.GetUserById(certificateDto.USERID);
                        var courseId = await _courseRepository.GetCourseIdByName(certificateDto.CourseName);

                        var certificate = new Certificate
                        {
                            Userid = certificateDto.USERID,
                            Courseid = courseId,
                            Releasedate = DateTime.Now,
                            Expiredate = DateTime.Now.AddYears(1),
                            Certificatecloudinaryurl = cloudinaryUploadResult.SecureUrl.AbsoluteUri,


                        };
                        List<Certificate> certificates = await _certificateService.GetUserCertificates(certificateDto.USERID);
                        if (certificates != null)
                        {
                            bool certificateExistsForCourse = false;

                            foreach (var cert in certificates)
                            {
                                if (cert.Courseid == certificate.Courseid)
                                {
                                    _certificateService.UpdateUserCertificate((int)certificate.Userid, (int)certificate.Courseid, certificate.Certificatecloudinaryurl);
                                    certificateExistsForCourse = true;
                                    break;
                                }
                            }

                            if (!certificateExistsForCourse)
                            {
                                _certificateRepository.SaveCertificate(certificate);
                            }
                        }
                        else
                        {
                            _certificateRepository.SaveCertificate(certificate);
                        }





                        var cloudinaryPdfUrl = _cloudinary.Api.UrlImgUp.Transform(new Transformation().FetchFormat("pdf")).ResourceType("raw").BuildUrl($"PdfCertificates/{pdfFileName}");


                        Response.Headers.Add("Content-Type", "application/pdf");

                        return File(pdfBytes, "application/pdf", "certificate.pdf");
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

        private async Task<ImageUploadResult> UploadToCloudinary(string imagePath)
        {
            byte[] imageBytes = await System.IO.File.ReadAllBytesAsync(imagePath);

            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription("photo", ms)
                };

                return await _cloudinary.UploadAsync(uploadParams);
            }
        }


        //UploadToCloudinary for pdf file

        private async Task<ImageUploadResult> UploadPdfToCloudinary(byte[] pdfBytes, string pdfFileName)
        {
            using (MemoryStream ms = new MemoryStream(pdfBytes))
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(pdfFileName, ms),
                    PublicId = $"{pdfFileName.Substring(0, pdfFileName.LastIndexOf("."))}",
                    Overwrite = true,
                };

                return await _cloudinary.UploadAsync(uploadParams);
            }
        }


        [HttpGet]
        [Route("GetUserCertificates/{userId}")]
        public async Task<List<Certificate>> GetUserCertificates(int userId)
        {

            return await _certificateService.GetUserCertificates(userId);


        }

        [HttpPost]
        [Route("SendCertificate")]
        public async Task<IActionResult> SendCertificate([FromForm] IFormFile certificateFile, [FromForm] string recipientEmail)
        {
            try
            {
                // Check if the certificateFile is not null and has some content
                if (certificateFile != null && certificateFile.Length > 0)
                {
                    // Read the file content into a byte array
                    using (var memoryStream = new MemoryStream())
                    {
                        await certificateFile.CopyToAsync(memoryStream);
                        byte[] certificateBytes = memoryStream.ToArray();

                        // Build MIME message
                        MimeMessage message = new MimeMessage();
                        MailboxAddress from = new MailboxAddress("CertifyHub", "eslamalshqeirat1@gmail.com");
                        message.From.Add(from);

                        // Use the dynamically provided recipient email address
                        MailboxAddress to = new MailboxAddress("Learner", recipientEmail);
                        message.To.Add(to);

                        message.Subject = "Certificate";

                        var bodyBuilder = new BodyBuilder();
                        bodyBuilder.TextBody = $"Dear Learner,\n\nCongratulations! You have successfully completed the course and earned a certificate. Please find your certificate attached to this email.\n\nBest Regards,\nCertifyHub";
                        bodyBuilder.Attachments.Add("certificate.pdf", certificateBytes, ContentType.Parse("application/pdf"));

                        message.Body = bodyBuilder.ToMessageBody();

                        // Send the email using MailKit
                        using (var client = new SmtpClient())
                        {
                            await client.ConnectAsync("smtp.gmail.com", 465, true);
                            Console.WriteLine("Connected to SMTP server.");

                            await client.AuthenticateAsync("eslamalshqeirat1@gmail.com", "kuwabpvlwahjuhxt");
                            Console.WriteLine("Authenticated to SMTP server.");

                            await client.SendAsync(message);
                            Console.WriteLine("Message sent.");

                            await client.DisconnectAsync(true);
                            Console.WriteLine("Disconnected from SMTP server.");
                        }
                    }
                }
                else
                {
                    return BadRequest("Certificate file is missing or empty.");
                }
                return Ok("Created");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}



















