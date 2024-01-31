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

namespace CertifyHub.Infra.Repository
{
    public class StdSectionRepository : IStdSectionRepository
    {
        private readonly IDbContext _dbContext;

        public StdSectionRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Stdsection>> GetAllStdSections()
        {
            return (await _dbContext.Connection.QueryAsync<Stdsection>("STD_SECTION_PACKAGE.GET_ALL_STD_SECTIONS", commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<Stdsection> GetStdSectionById(int id)
        {
            var p = new DynamicParameters();
            p.Add("STD_SECTION_ID", id, DbType.Int32, ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryAsync<Stdsection>("STD_SECTION_PACKAGE.GET_STD_SECTION_BY_ID", p, commandType: CommandType.StoredProcedure);
            return result.FirstOrDefault();
        }

        public async Task<List<Stdsection>> GetStdSectionsBySectionId(int id)
        {
            var p = new DynamicParameters();
            p.Add("SECTION_ID", id, DbType.Int32, ParameterDirection.Input);
            return (await _dbContext.Connection.QueryAsync<Stdsection>("STD_SECTION_PACKAGE.GET_STD_SECTIONS_BY_SECTION_ID", p, commandType: CommandType.StoredProcedure)).ToList();
        }

        public void CreateStdSection(Stdsection stdsection)
        {
            var p = new DynamicParameters();
            p.Add("SECTION_ID", stdsection.Sectionid, DbType.Int32, ParameterDirection.Input);
            p.Add("STUDENT_ID", stdsection.Studentid, DbType.Int32, ParameterDirection.Input);
            _dbContext.Connection.Execute("STD_SECTION_PACKAGE.CREATE_STD_SECTION", p, commandType: CommandType.StoredProcedure);
        }

        public void UpdateStdSection(Stdsection stdsection)
        {
            var p = new DynamicParameters();
            p.Add("STD_SECTION_ID", stdsection.Stdsectionid, DbType.Int32, ParameterDirection.Input);
            p.Add("SECTION_ID", stdsection.Sectionid, DbType.Int32, ParameterDirection.Input);
            p.Add("STUDENT_ID", stdsection.Studentid, DbType.Int32, ParameterDirection.Input);
            _dbContext.Connection.Execute("STD_SECTION_PACKAGE.UPDATE_STD_SECTION", p, commandType: CommandType.StoredProcedure);
        }

        public void DeleteStdSection(int studentId, int sectionId)
        {
            var p = new DynamicParameters();
            p.Add("SECTION_ID", sectionId, DbType.Int32, ParameterDirection.Input);
            p.Add("STUDENT_ID", studentId, DbType.Int32, ParameterDirection.Input);
            _dbContext.Connection.Execute("STD_SECTION_PACKAGE.DELETE_STD_SECTION", p, commandType: CommandType.StoredProcedure);
        }
        public async Task<List<StdSectionInfo>> GetStdSectionsInfo()
        {
            return (await _dbContext.Connection.QueryAsync<StdSectionInfo>("STD_SECTION_PACKAGE.GET_STD_SECTION_INFO", commandType: CommandType.StoredProcedure)).ToList();
        }
        public async Task<List<StudentsSolutionBySectionDTO>> GetStdSectionsBySection(int sectionId, int assessmentId)
        {
            var p = new DynamicParameters();
            p.Add("SECTION_ID", sectionId, DbType.Int32, ParameterDirection.Input);
            p.Add("ASSESSMENT_ID", assessmentId, DbType.Int32, ParameterDirection.Input);
            return (await _dbContext.Connection.QueryAsync<StudentsSolutionBySectionDTO>("STD_SECTION_Package.GET_STD_SECTION_BY_SECTION_ID", p, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<List<StdSectionInfo>> GetStdSectionsInfoBySectionId(int sectionId)
        {
            var p = new DynamicParameters();
            p.Add("SECTION_ID", sectionId, DbType.Int32, ParameterDirection.Input);
            return (await _dbContext.Connection.QueryAsync<StdSectionInfo>("STD_SECTION_PACKAGE.GET_STD_SECTION_INFO_BY_SECTION_ID",p, commandType: CommandType.StoredProcedure)).ToList();
        }
        public async Task<List<UserSectionDTO>> GetStdUserSection(int userid)
        {
            var p = new DynamicParameters();
            p.Add("student_id", userid, DbType.Int32, ParameterDirection.Input);
            return (await _dbContext.Connection.QueryAsync<UserSectionDTO>("STD_SECTION_PACKAGE.GET_USER_SECTION", p, commandType: CommandType.StoredProcedure)).ToList();

        }
        public void SetFlag(int sectionId, int studentId)
        {
            var p = new DynamicParameters();
            p.Add("SECTION_ID", sectionId, DbType.Int32, ParameterDirection.Input);
            p.Add("STUDENT_ID", studentId, DbType.Int32, ParameterDirection.Input);
            _dbContext.Connection.Execute("STD_SECTION_PACKAGE.SET_FLAG", p, commandType: CommandType.StoredProcedure);
        }
        public async Task<List<StudentGradesDTO>> ListStudentsGrades(int assessmentId)
        {
            var p = new DynamicParameters();
            p.Add("ASSESSMENT_ID", assessmentId, DbType.Int32, ParameterDirection.Input);
            return (await _dbContext.Connection.QueryAsync<StudentGradesDTO>("STD_SECTION_PACKAGE.LIST_STUDENT_GRADES", p, commandType: CommandType.StoredProcedure)).ToList();

        }
        public async Task<Certificate> GetCertificateUrl(int userId, int sectionId)
        {
            var p = new DynamicParameters();
            p.Add("pUserId", userId, DbType.Int32, ParameterDirection.Input);
            p.Add("pSectionId", sectionId, DbType.Int32, ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryAsync<Certificate>("STD_SECTION_PACKAGE.GetCertificateURL", p, commandType: CommandType.StoredProcedure);
            return result.FirstOrDefault();
        }
        public async Task<List<StudentInfo>> GetStudentsInfoBySectionId(int sectionId)
        {
            var p = new DynamicParameters();
            p.Add("SECTION_ID", sectionId, DbType.Int32, ParameterDirection.Input);
            return (await _dbContext.Connection.QueryAsync<StudentInfo>("STD_SECTION_PACKAGE.GET_STUDENTS_INFO_BY_SECTION", p, commandType: CommandType.StoredProcedure)).ToList();

        }
    }
}
