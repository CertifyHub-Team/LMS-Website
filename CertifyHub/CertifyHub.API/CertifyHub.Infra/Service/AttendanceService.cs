using CertifyHub.Core.Data;
using CertifyHub.Core.DTO;
using CertifyHub.Core.Repository;
using CertifyHub.Core.Service;
using CertifyHub.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Infra.Service
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;

        public AttendanceService(IAttendanceRepository attendanceRepository)
        {

            _attendanceRepository= attendanceRepository;
        }

        public void DeleteAttendance(int id)
        {
            _attendanceRepository.DeleteAttendance(id);
        }
       

        public Task<List<Attendance>> GetAttendanceByDate(DateTime date)
        {
            return _attendanceRepository.GetAttendanceByDate(date);
        }

        public Task<List<Attendance>> GetAttendanceBySection(int sectionId)
        {
            return _attendanceRepository.GetAttendanceBySection(sectionId);
        }

        public Task<List<Attendance>> GetAttendanceByStatus(string attendanceStatus)
        {
            return _attendanceRepository.GetAttendanceByStatus(attendanceStatus);
        }
        
        public Task<List<AttendanceWithSection>> GetAttendanceByUser(int userId)
        {
            return _attendanceRepository.GetAttendanceByUser(userId);
        }

        public Task<Attendance> GetAttendanceDetails(int id)
        {
            return _attendanceRepository.GetAttendanceDetails(id);
        }

        public Task<int> GetPresentAttendanceCount()
        {
            return _attendanceRepository.GetPresentAttendanceCount();
        }

        public Task<int> GetAbsentAttendanceCount()
        {
            return _attendanceRepository.GetAbsentAttendanceCount();
        }

        public void InsertAttendance(Attendance attendance)
        {
            _attendanceRepository.InsertAttendance(attendance);
        }

        public void UpdateAttendance(Attendance attendance)
        {
            _attendanceRepository.UpdateAttendance  (attendance);
        }

        public async Task<int> GetAbsentAttendanceCountForUser(int userId, int sectionId)
        {
            return await _attendanceRepository.GetAbsentAttendanceCountForUser(userId, sectionId);
        }

        public async Task<int> GetPresentAttendanceCountForUser(int userId, int sectionId)
        {
           return await _attendanceRepository.GetPresentAttendanceCountForUser(userId, sectionId);
        }
        public async Task<List<Attendance>> GetAttendanceByUserAndSection(int userId, int sectionId)
        {
            return await _attendanceRepository.GetAttendanceByUserAndSection(userId, sectionId);

        }
        public async Task<List<TopStudentAttendances>> GetTopStudentsByAttendanceForEachSection(int instructorId)
        {
            return await _attendanceRepository.GetTopStudentsByAttendanceForEachSection(instructorId);

        }
    }
}
