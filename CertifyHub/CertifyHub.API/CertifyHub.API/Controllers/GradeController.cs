using CertifyHub.Core.Data;
using CertifyHub.Core.DTO;
using CertifyHub.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CertifyHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IGradeService _gradeService;
        public GradeController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        [HttpGet]
        [Route("CalculateUserGrade/{userid}")]
        public Task<double> CalculateUserGrade(int userid)
        {
            return _gradeService.CalculateUserGrade(userid);
        }

        [HttpGet]
        [Route("ConvertToLetterGrade/{finalGrade}")]
        public Task<string> ConvertToLetterGrade(double finalGrade)
        {
            return _gradeService.ConvertToLetterGrade(finalGrade);
        }

        [HttpPost]
        public void CreateGrade(List<Grade> grade)
        {
           foreach (Grade g in grade)
            {
                _gradeService.CreateGrade(g);
            }
        }

        [HttpDelete("{gradeid}")]
        public void DeleteGrade(int gradeid)
        {
            _gradeService.DeleteGrade(gradeid);
        }

        [HttpGet]
        [Route("GetGradeById/{gradeid}")]
        public Task<Grade> GetGradeById(int gradeid)
        {
            return _gradeService.GetGradeById(gradeid);
        }

        //[HttpGet]
        //[Route("GetUserGrade")]
        //public Task<Grade> GetUserGrade(int userid , int assessmentid)
        //{
        //    return _gradeService.GetUserGrade(userid, assessmentid);
        //}
        [HttpGet]
        [Route("GetUserGrade")]
        public async Task<decimal?> GetUserGrade(int userid, int assessmentid)
        {
            var grade = await _gradeService.GetUserGrade(userid, assessmentid);
            return grade?.Studentgrade;
        }
        [HttpPut]
        public void UpdateGrade(Grade grade)

        { 
            _gradeService.UpdateGrade(grade);
        }

        [HttpGet]
        [Route("GetGradeByUserId/{userid}")]
        public Task<List<StudentGradesDTO>> GetGradeByUserId(int userid)
        {
            return _gradeService.GetGradeByUserId(userid);
        }
    }
}
