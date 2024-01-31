using CertifyHub.Core.Common;
using CertifyHub.Core.Data;
using CertifyHub.Core.DTO;
using CertifyHub.Core.Repository;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace CertifyHub.Infra.Repository
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly IDbContext _dbContext;

        public AttendanceRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;  
        }


        public void InsertAttendance(Attendance attendance)
        {
            var p = new DynamicParameters();
            p.Add("section_id", attendance.Sectionid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("date_attendance", attendance.Dateofattendance, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            p.Add("learner_id", attendance.Userid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("attendance_status", attendance.Status, dbType: DbType.String, direction: ParameterDirection.Input);

            _dbContext.Connection.ExecuteAsync("Attendance_Package.InsertAttendance", p, commandType: CommandType.StoredProcedure);
        }



        public void UpdateAttendance(Attendance attendance)
        {
            var p = new DynamicParameters();
            p.Add("attendance_id", attendance.Attendanceid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("section_id", attendance.Sectionid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("date_attendance", attendance.Dateofattendance, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            p.Add("learner_id", attendance.Userid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("attendance_status", attendance.Status, dbType: DbType.String, direction: ParameterDirection.Input);

            _dbContext.Connection.ExecuteAsync("Attendance_Package.UpdateAttendance", p, commandType: CommandType.StoredProcedure);
        }


        public async void DeleteAttendance(int id)
        {
            var p = new DynamicParameters();
            p.Add("attendance_id", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            _dbContext.Connection.ExecuteAsync("Attendance_Package.DeleteAttendance", p, commandType: CommandType.StoredProcedure);
        }

       
        public async Task<List<Attendance>> GetAttendanceByDate(DateTime date)
        {
            var parameters = new DynamicParameters();
            parameters.Add("date_attendance", date, dbType: DbType.DateTime, ParameterDirection.Input);

            var result = await _dbContext.Connection.QueryAsync<Attendance>("Attendance_Package.GetAttendanceByDate", parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task<List<Attendance>> GetAttendanceBySection(int sectionId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("section_id", sectionId, dbType: DbType.Int32, ParameterDirection.Input);

            var result = await _dbContext.Connection.QueryAsync<Attendance>("Attendance_Package.GetAttendanceBySection", parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task<List<Attendance>> GetAttendanceByStatus(string attendanceStatus)
        {
            var parameters = new DynamicParameters();
            parameters.Add("attendance_status", attendanceStatus, dbType: DbType.String, ParameterDirection.Input);

            var result = await _dbContext.Connection.QueryAsync<Attendance>("Attendance_Package.GetAttendanceByStatus", parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task<List<AttendanceWithSection>> GetAttendanceByUser(int userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("user_id", userId, dbType: DbType.Int32, ParameterDirection.Input);

            var result = await _dbContext.Connection.QueryAsync<AttendanceWithSection>("Attendance_Package.GetAttendanceByUser", parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task<Attendance> GetAttendanceDetails(int id)
        {
            var p = new DynamicParameters();
            p.Add("attendance_id", id, dbType: DbType.Int32, ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryAsync<Attendance>("Attendance_Package.GetAttendanceDetails", p, commandType: CommandType.StoredProcedure);
            return result.FirstOrDefault();
        }

        public  async Task<int> GetPresentAttendanceCount()
        {
            var p = new DynamicParameters();
            p.Add("present_count", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await _dbContext.Connection.ExecuteAsync("Attendance_Package.GetPresentAttendanceCount", p, commandType: CommandType.StoredProcedure);

            var presentCount = p.Get<int>("present_count");
            return presentCount;

        }

        public async Task<int> GetAbsentAttendanceCount()
        {
            var p = new DynamicParameters();
            p.Add("absent_count", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await _dbContext.Connection.ExecuteAsync("Attendance_Package.GetAbsentAttendanceCount", p, commandType: CommandType.StoredProcedure);

            var absentCount = p.Get<int>("absent_count");
            return absentCount;
        }
        public async Task<int> GetAbsentAttendanceCountForUser(int userId, int sectionId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("user_id", userId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            parameters.Add("section_id", sectionId, dbType: DbType.Int32, direction: ParameterDirection.Input);

            var result = await _dbContext.Connection.QueryAsync<int>("Attendance_Package.GetAbsentAttendanceCountForUser", parameters, commandType: CommandType.StoredProcedure);

            return result.FirstOrDefault();
        }


        public async Task<int> GetPresentAttendanceCountForUser(int userId, int sectionId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("user_id", userId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            parameters.Add("section_id", sectionId, dbType: DbType.Int32, direction: ParameterDirection.Input);

            var result = await _dbContext.Connection.QueryAsync<int>("Attendance_Package.GetPresentAttendanceCountForUser", parameters, commandType: CommandType.StoredProcedure);

            return result.FirstOrDefault();
        }
        public async Task<List<Attendance>> GetAttendanceByUserAndSection(int userId, int sectionId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("user_id", userId, dbType: DbType.Int32, ParameterDirection.Input);
            parameters.Add("section_id", sectionId, dbType: DbType.Int32, ParameterDirection.Input);

            var result = await _dbContext.Connection.QueryAsync<Attendance>("Attendance_Package.GetAttendanceByUserAndSection", parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }


        public async Task<List<TopStudentAttendances>> GetTopStudentsByAttendanceForEachSection(int instructorId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("instructor_id", instructorId, DbType.Int32, ParameterDirection.Input);

            var result = await _dbContext.Connection.QueryAsync<TopStudentAttendances>("Attendance_Package.GetTopStudentsByAttendanceForEachSection", parameters, commandType: CommandType.StoredProcedure);

            return result.ToList();
        }

    }
}

