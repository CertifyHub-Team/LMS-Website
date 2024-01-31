using CertifyHub.Core.Common;
using CertifyHub.Core.Data;
using CertifyHub.Core.Repository;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CertifyHub.Infra.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly IDbContext _dbContext; 
        public QuestionRepository(IDbContext dbContext) { 
            _dbContext = dbContext;
        } 
        public async void CreateQuestion(Question question)
        {
            var p = new DynamicParameters();
            p.Add("p_QuestionText", question.Questiontext, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("p_Marks", question.Marks, dbType: DbType.Double, direction: ParameterDirection.Input);
            p.Add("p_AssessmentID", question.Assessmentid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            await _dbContext.Connection.ExecuteAsync("Questions_Package.CreateQuestion", p, commandType: CommandType.StoredProcedure);
        }

        public async void DeleteQuestion(int Questionid)
        {
            var p = new DynamicParameters();
            p.Add("p_QuestionID", Questionid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            await _dbContext.Connection.ExecuteAsync("Questions_Package.DeleteQuestion", p, commandType: CommandType.StoredProcedure);

        }

        public async Task<Question> GetQuestionByID(int Questionid)
        {
            var p = new DynamicParameters();
            p.Add("p_QuestionID", Questionid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryAsync<Question>("Questions_Package.GetQuestionByID", p, commandType: CommandType.StoredProcedure);
            return result.FirstOrDefault();
        }

        public async Task<List<Question>> ListQuestionsByAssessment(int AssessmentId)
        {
            var p = new DynamicParameters();
            p.Add("p_AssessmentID", AssessmentId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryAsync<Question>("Questions_Package.ListQuestionsByAssessment", p, commandType: CommandType.StoredProcedure);
            return result.ToList(); 
        }

        public async void UpdateQuestion(Question question)
        {
            var p = new DynamicParameters();
            p.Add("p_QuestionID", question.Questionid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("p_QuestionText", question.Questiontext, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("p_Marks", question.Marks, dbType: DbType.Double, direction: ParameterDirection.Input);
            p.Add("p_AssessmentID", question.Assessmentid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            await _dbContext.Connection.ExecuteAsync("Questions_Package.UpdateQuestion", p, commandType: CommandType.StoredProcedure);
        }

        public async void UpdateQuestionMarks(int Questionid, int newMark)
        {
            var p = new DynamicParameters();
            p.Add("p_QuestionID", Questionid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("p_NewMarks", newMark, dbType: DbType.Double, direction: ParameterDirection.Input);
            await _dbContext.Connection.ExecuteAsync("Questions_Package.UpdateQuestionMarks", p, commandType: CommandType.StoredProcedure);
        }
    }
}
