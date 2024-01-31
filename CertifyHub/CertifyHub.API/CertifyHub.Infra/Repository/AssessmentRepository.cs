using CertifyHub.Core.Common;
using CertifyHub.Core.Data;
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
    public class AssessmentRepository : IAssessmentRepository
    {
        private readonly IDbContext _dbContext;

        public AssessmentRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> CountAssessmentsBySection(int sectionId)
        {
            var p = new DynamicParameters();
            p.Add("p_SectionID", sectionId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("p_Count", dbType: DbType.Int32, direction: ParameterDirection.Output);
            await _dbContext.Connection.ExecuteAsync("Assessment_Package.CountAssessmentsBySection", p, commandType: CommandType.StoredProcedure);
            int AssCount = p.Get<int>("p_Count");
            return AssCount;
        }

        public async Task<int> CountAssessmentsByType(string AssessmentType)
        {
            var p = new DynamicParameters();
            p.Add("p_AssessmentType", AssessmentType, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("p_Count", dbType: DbType.Int32, direction: ParameterDirection.Output);
            await _dbContext.Connection.ExecuteAsync("Assessment_Package.CountAssessmentsByType", p, commandType: CommandType.StoredProcedure);
            int AssCount = p.Get<int>("p_Count");
            return AssCount;
        }

        public async void CreateAssessment(Assessment assessment)
        {
            var p = new DynamicParameters();
            p.Add("p_Title", assessment.Title, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("p_Description", assessment.Description, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("p_AssessmentType", assessment.Assessmenttype, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("p_StartDate", assessment.Startdate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            p.Add("p_EndDate", assessment.Enddate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            p.Add("p_StartTime", assessment.Starttime, dbType: DbType.Time, direction: ParameterDirection.Input);
            p.Add("p_EndTime", assessment.Endtime, dbType: DbType.Time, direction: ParameterDirection.Input);
            p.Add("p_AssessmentScore", assessment.Assessmentscore, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("p_AttachFile", assessment.Attachfile, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("p_SectionID", assessment.Sectionid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            await _dbContext.Connection.ExecuteAsync("Assessment_Package.CreateAssessment", p, commandType: CommandType.StoredProcedure);

        }

        public async void DeleteAssessment(int Assessmentid)
        {
            var p = new DynamicParameters();
            p.Add("p_AssessmentID", Assessmentid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            await _dbContext.Connection.ExecuteAsync("Assessment_Package.DeleteAssessment", p, commandType: CommandType.StoredProcedure);

        }

        public async Task<List<Assessment>> FilterAssessmentsByDate(DateTime startDate, DateTime endDate)
        {
            var p = new DynamicParameters();
            p.Add("p_StartDate", startDate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            p.Add("p_EndDate", endDate, dbType: DbType.DateTime, direction: ParameterDirection.Input);

            var result = await _dbContext.Connection.QueryAsync<Assessment>("Assessment_Package.FilterAssessmentsByDate", p, commandType: CommandType.StoredProcedure);

            return result.ToList();
        }



        public async Task<List<Assessment>> GetUpcomingAssessments()
        {
            var result = await _dbContext.Connection.QueryAsync<Assessment>("Assessment_Package.GetUpcomingAssessments", commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task<List<Assessment>> ListAssessmentsByType(string AssessmentType)
        {
            var p = new DynamicParameters();
            p.Add("p_AssessmentType", AssessmentType, dbType: DbType.String, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryAsync<Assessment>("Assessment_Package.ListAssessmentsByType", commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async void UpdateAssessment(Assessment assessment)
        {
            var p = new DynamicParameters();
            p.Add("p_AssessmentID", assessment.Assessmentid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("p_Title", assessment.Title, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("p_Description", assessment.Description, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("p_AssessmentType", assessment.Assessmenttype, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("p_StartDate", assessment.Startdate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            p.Add("p_EndDate", assessment.Enddate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            p.Add("p_StartTime", assessment.Starttime, dbType: DbType.Time, direction: ParameterDirection.Input);
            p.Add("p_EndTime", assessment.Endtime, dbType: DbType.Time, direction: ParameterDirection.Input);
            p.Add("p_AssessmentScore", assessment.Assessmentscore, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("p_AttachFile", assessment.Attachfile, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("p_SectionID", assessment.Sectionid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            await _dbContext.Connection.ExecuteAsync("Assessment_Package.UpdateAssessment", p, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<Assessment>> GetAssessmentBysectionId(int section_id)
        {
            var p = new DynamicParameters();
            p.Add("section_id", section_id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryAsync<Assessment>("Assessment_Package.GetAssessmentBysectionId", p, commandType: CommandType.StoredProcedure);

            return result.ToList();

        }
    }
}
