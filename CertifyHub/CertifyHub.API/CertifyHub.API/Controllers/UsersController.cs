using CertifyHub.Core.Data;
using CertifyHub.Core.Service;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Runtime.InteropServices;

namespace CertifyHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<List<User>> GetAllUsers()
        {
            return await _usersService.GetAllUsers();
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<int> CreateUser(User user)
        {
            return await _usersService.CreateUser(user);
        }

        [HttpPut]
        [Route("UpdateUser")]
        public void UpdateUser(User user)
        {
            _usersService.UpdateUser(user);
        }

        [HttpGet]
        [Route("GetUserById/{id}")]

        public async Task<User> GetUserById(int id)
        {
            return await _usersService.GetUserById(id);
        }


        [HttpPut]
        [Route("UpdateUserPhone/{id}/{newPhoneNumber}")]
        public void UpdateUserPhone(int id, string newPhoneNumber)
        {
            _usersService.UpdateUserPhone(id, newPhoneNumber);
        }


        [HttpPut]
        [Route("UpdateUserImage/{id}/{newImagePath}")]
        public void UpdateUserImage(int id, string newImagePath)
        {
            _usersService.UpdateUserImage(id, newImagePath);
        }


        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public void DeleteUser(int id)
        {
            _usersService.DeleteUser(id);
        }

        [HttpPut]
        [Route("UpdateUserIsActive/{id}/{isActive}")]
        public void UpdateUserIsActive(int id,  int isActive)
        {
            _usersService.UpdateUserIsActive(id, isActive);
        }

        [HttpPost]
        [Route("UploadImage")]
        public User UploadImage()
        {
            var file = Request.Form.Files[0];//the file is within the request with => form data => file,if uploded more than one file, in this case only one file so its index is 0
            var fileName = $"{Guid.NewGuid().ToString()}_{file.FileName}"; //change the name to avoid duplicate
            //var fileName = Guid.NewGuid().ToString() + "_" + file.FileName";
            var fullPath = Path.Combine("D:\\Downloads\\CertifyHub 2\\CertifyHub\\CertifyHub\\src\\assets\\Images", fileName); //define the path string
            using (var stream = new FileStream(fullPath, FileMode.Create)) //create the path
            {
                file.CopyTo(stream); //append the file to  the path
            }

            User user = new User(); //it is better to return object than a single value
            user.Imagepath = fileName;

            return user;
        }

        [HttpGet]
        [Route("GetAllStudents")]

        public Task<List<User>> GetAllStudents()
        {
            return _usersService.GetAllStudents();
        }


        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> Registration(Login login)
        {
            try
            {
                if (login != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        MimeMessage message = new MimeMessage();
                        MailboxAddress from = new MailboxAddress("CertifyHub", "eslamalshqeirat1@gmail.com");
                        message.From.Add(from);

                        MailboxAddress to = new MailboxAddress("Learner", login.Username);
                        message.To.Add(to);

                        string subject = "Welcome to CertifyHub - Registration Confirmation";
                        string body = "";

                        if (login.Roleid == 3) // Learner
                        {
                            
                            body = $"Dear Learner,\n\nThank you for registering with CertifyHub! Your learner account has been successfully created.\n\nYour login details:\nUsername: {login.Username}\nPassword: {login.Password}\n\nWelcome aboard!\n\nSincerely,\nThe CertifyHub Team";
                        }
                        else if (login.Roleid == 2) // Instructor
                        {
                            body = $"Dear Instructor,\n\nThank you for joining CertifyHub as an instructor! Your instructor account has been successfully created.\n\nYour login details:\nUsername: {login.Username}\nPassword: {login.Password}\n\nWelcome to our teaching community!\n\nSincerely,\nThe CertifyHub Team";
                        }

                        message.Subject = subject;

                        var bodyBuilder = new BodyBuilder();
                        bodyBuilder.TextBody = body;

                        message.Body = bodyBuilder.ToMessageBody();

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
                    return BadRequest("Login Object is Empty.");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("GetAllInstructors")]
        public async Task<List<User>> GetAllInstructors()
        {
            return await _usersService.GetAllInstructors();
        }

        [HttpGet]
        [Route("GetStudentsCvs")]
        public async Task<List<User>> GetStudentsCvs()
        {
            return await _usersService.GetStudentsCvs();
        }

    }
}
