using CertifyHub.Core.Data;
using CertifyHub.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CertifyHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UserSolutionController : ControllerBase
    {
        private readonly IUserSolutionService _userSolutionService;
        public UserSolutionController(IUserSolutionService userSolutionService)
        {
            _userSolutionService = userSolutionService;
        }

        //[HttpPost]
        //public void AddUserSolution(Usersolution usersolution)
        //{
        //    _userSolutionService.AddUserSolution(usersolution);
        //}
        [HttpGet]
        public Task<Usersolution> getUserSolutionByAssessment(int assessmentId, int userId)
        {
            return _userSolutionService.getUserSolutionByAssessment(assessmentId, userId);
        }



        [HttpPost]
        public async Task<IActionResult> AddUserSolution([FromForm] Usersolution usersolution)
        {
            try
            {
                var file = Request.Form.Files[0];

                if (file != null && file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine("D:\\Downloads\\CertifyHub 2\\CertifyHub\\CertifyHub\\src\\assets\\uploads", fileName);

                    // Save the file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Save the file name to the database
                    usersolution.Usersolutiontext = fileName;

                    await _userSolutionService.AddUserSolution(usersolution);
                }
                else
                {
                    await _userSolutionService.AddUserSolution(usersolution);
                }

                return Ok(new { Message = "User solution added successfully" });
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }


                [HttpPost]
        [Route("AddUserExamSolution")]
        public async void AddUserExamSolution(List<Usersolution> usersolution)
        {

            foreach (var item in usersolution)
            {
                _userSolutionService.AddUserSolution(item);

            }

        }
        [HttpGet]
        [Route("GetUserScore/{assessmentId}/{userId}")]
        public async Task<int> GetUserScore(int assessmentId, int userId)
        {
            return await _userSolutionService.GetUserScore(assessmentId, userId);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUserSolution([FromForm] Usersolution usersolution)
        {
            try
            {
                var file = Request.Form.Files.Count > 0 ? Request.Form.Files[0] : null;

                if (file != null && file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine("D:\\Downloads\\CertifyHub 2\\CertifyHub\\CertifyHub\\src\\assets\\uploads", fileName);

                    // Save the file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Save the file name to the database
                    usersolution.Usersolutiontext = fileName;

                }

                await _userSolutionService.UpdateUserSolution(usersolution);

                return Ok(new { Message = "User solution updated successfully" });
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");

                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }

        }


        [HttpDelete("{solutionId}")]
        public async Task DeleteUserSolution(int solutionId)
        {
            await _userSolutionService.DeleteUserSolution(solutionId);
        }
    }
}
