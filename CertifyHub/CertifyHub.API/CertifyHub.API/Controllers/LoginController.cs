using CertifyHub.Core.Data;
using CertifyHub.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CertifyHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpGet]
        [Route("GetAllLogins")]
        public async Task<List<Login>> GetAllLogins()
        {
            return await _loginService.GetAllLogins();
        }

        [HttpGet]
        [Route("GetLoginById/{id}")]
        public async Task<Login> GetLoginById(int id)
        {
            return await _loginService.GetLoginById(id);
        }

        [HttpPost]
        [Route("CreateLogin")]
        public void CreateLogin(Login login)
        {
            _loginService.CreateLogin(login);
        }

        [HttpPut]
        [Route("UpdateLogin")]
        public void UpdateLogin(Login login)
        {
            _loginService.UpdateLogin(login);
        }

        [HttpDelete("{id}")]
        public void DeleteLogin(int id)
        {
            _loginService.DeleteLogin(id);
        }

        [HttpPut]
        [Route("UpdatePassword")]
        public void UpdatePassword(Login login)
        {
            _loginService.UpdatePassword(login);
        }

        [HttpPost]
        [Route("UserLogin")]
        public IActionResult UserLogin(Login login)
        {
            var token = _loginService.UserLogin(login);
            if (token == null)
            {
                return Unauthorized();
            }
            else
            {
                return Ok(token);
            }
        }
        [HttpGet]
        [Route("GetInstructorCount")]
        public async Task<int> GetInstructorCount()
        {
            return await _loginService.GetInstructorCount();
        }

        [HttpGet]
        [Route("GetStudentCount")]
        public async Task<int> GetStudentCount()
        {
            return await _loginService.GetStudentCount();
        }


    }
}
