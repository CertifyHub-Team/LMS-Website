using CertifyHub.Core.Data;
using CertifyHub.Core.DTO;
using CertifyHub.Core.Repository;
using CertifyHub.Core.Service;
using CertifyHub.Infra.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CertifyHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {

            _attendanceService = attendanceService;
        }


        [HttpPost]
        public async void InsertAttendance(List<Attendance> attendance)
        {
            foreach (var item in attendance)
            {
                _attendanceService.InsertAttendance(item);
            }
        }

        [HttpPut]
        public async void UpdateAttendance(List<Attendance> attendance)
        {
            foreach (var item in attendance)
            {
                _attendanceService.UpdateAttendance(item);
            }
        }

        [HttpDelete("{id}")] 
        public async void DeleteAttendance(int id)
        {
            _attendanceService.DeleteAttendance(id);
        }



        [HttpGet]
        [Route("GetAttendanceByDate/{date}")]
        public async Task<List<Attendance>> GetAttendanceByDate(DateTime date)
        {
            return await _attendanceService.GetAttendanceByDate(date);
        }


        [HttpGet]
        [Route("GetAttendanceBySection/{sectionId}")]
        public async Task<List<Attendance>> GetAttendanceBySection(int sectionId)
        {
            return await _attendanceService.GetAttendanceBySection(sectionId);
        }

        [HttpGet]
        [Route("GetAttendanceByStatus/{attendanceStatus}")]

        public async Task<List<Attendance>> GetAttendanceByStatus(string attendanceStatus)
        {
            return await  _attendanceService.GetAttendanceByStatus(attendanceStatus);
        }


        [HttpGet]
        [Route("GetAttendanceByUser/{userId}")]
        public async Task<List<AttendanceWithSection>> GetAttendanceByUser(int userId)
        {
            return await _attendanceService.GetAttendanceByUser(userId);
        }

        [HttpGet]
        [Route("GetAttendanceDetails/{id}")]
        public async Task<Attendance> GetAttendanceDetails(int id)
        {
            return await _attendanceService.GetAttendanceDetails(id);
        }

        [HttpGet]
        [Route("GetPresentAttendanceCount")]
        public async Task<int> GetPresentAttendanceCount()
        {
            return await _attendanceService.GetPresentAttendanceCount();
        }

        [HttpGet]
        [Route("GetAbsentAttendanceCount")]
        public async Task<int> GetAbsentAttendanceCount()
        {
            return await  _attendanceService.GetAbsentAttendanceCount();
        }

        [HttpGet]
        [Route("GetAbsentAttendanceCountForUser/{userId}/{sectionId}")]
        public async Task<int> GetAbsentAttendanceCountForUser(int userId, int sectionId)
        {
            return await _attendanceService.GetAbsentAttendanceCountForUser(userId, sectionId);

        }


        [HttpGet]
        [Route("GetPresentAttendanceCountForUser/{userId}/{sectionId}")]

        public async Task<int> GetPresentAttendanceCountForUser(int userId, int sectionId)
        {
            return await _attendanceService.GetPresentAttendanceCountForUser(userId, sectionId);

        }
        [HttpGet]
        [Route("GetAttendanceByUserAndSection/{userId}/{sectionId}")]
        public async Task<List<Attendance>> GetAttendanceByUserAndSection(int userId, int sectionId)
        {
            return await _attendanceService.GetAttendanceByUserAndSection(userId, sectionId);

        }


        [HttpGet]
        [Route("GetTopStudentsByAttendanceForEachSection/{instructorId}")]

        public async Task<List<TopStudentAttendances>> GetTopStudentsByAttendanceForEachSection(int instructorId)
        {
            return await _attendanceService.GetTopStudentsByAttendanceForEachSection(instructorId);

        }

    }
}
