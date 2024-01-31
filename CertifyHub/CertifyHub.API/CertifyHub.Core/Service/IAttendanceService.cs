using CertifyHub.Core.Data;
using CertifyHub.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Core.Service
{
    public interface IAttendanceService
    {

        void InsertAttendance(Attendance attendance);
        void UpdateAttendance(Attendance attendance);
        void DeleteAttendance(int id);
        Task<Attendance> GetAttendanceDetails(int id);
        Task<List<Attendance>> GetAttendanceBySection(int sectionId);
        Task<List<AttendanceWithSection>> GetAttendanceByUser(int userId);
        Task<List<Attendance>> GetAttendanceByDate(DateTime date);
        Task<List<Attendance>> GetAttendanceByStatus(string attendanceStatus);
        Task<int> GetPresentAttendanceCount();
        Task<int> GetAbsentAttendanceCount();
        Task<int> GetAbsentAttendanceCountForUser(int userId, int sectionId);
        Task<int> GetPresentAttendanceCountForUser(int userId, int sectionId);
        Task<List<Attendance>> GetAttendanceByUserAndSection(int userId, int sectionId);
        Task<List<TopStudentAttendances>> GetTopStudentsByAttendanceForEachSection(int instructorId);
    }
}
