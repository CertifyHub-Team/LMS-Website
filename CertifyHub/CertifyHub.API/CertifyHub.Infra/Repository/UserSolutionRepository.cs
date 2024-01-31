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
    public class UserSolutionRepository : IUserSolutionRepository
    {
        private readonly IDbContext _dbContext;

        public UserSolutionRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddUserSolution(Usersolution usersolution)
        {
            var p = new DynamicParameters();
            p.Add("p_UserID", usersolution.Userid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("p_AssessmentID", usersolution.Assessmentid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("p_QuestionID", usersolution.Questionid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("p_AnswerID", usersolution.Answerid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("p_UserSolution", usersolution.Usersolutiontext, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("p_AttemptDate", usersolution.Attemptdate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
          await  _dbContext.Connection.ExecuteAsync("UserSolutions_Package.AddUserSolution", p, commandType: CommandType.StoredProcedure);
        }

        public async Task<Usersolution> getUserSolutionByAssessment(int assessmentId, int userId)
        {
            var p = new DynamicParameters();
            p.Add("p_AssessmentID", assessmentId, dbType: DbType.Int32, ParameterDirection.Input);
            p.Add("p_UserID", userId, dbType: DbType.Int32, ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryAsync<Usersolution>("UserSolutions_Package.getUserSolutionByAssessment", p, commandType: CommandType.StoredProcedure);
            return result.FirstOrDefault();
        }


        public async Task<int> GetUserScore(int assessmentId, int userId)
        {
            var p = new DynamicParameters();
            p.Add("p_AssessmentID", assessmentId, DbType.Int32, ParameterDirection.Input);
            p.Add("p_UserID", userId, DbType.Int32, ParameterDirection.Input);
            p.Add("totalMarks", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            await _dbContext.Connection.ExecuteAsync("UserSolutions_Package.CalculateUserScore", p, commandType: CommandType.StoredProcedure);

            // Retrieve the output parameter value
            int userScore = p.Get<int>("totalMarks");

            return userScore;
        }
        public async Task UpdateUserSolution(Usersolution usersolution)
        {
            var p = new DynamicParameters();
            p.Add("p_SOLUTIONID", usersolution.Solutionid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("p_UserID", usersolution.Userid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("p_AssessmentID", usersolution.Assessmentid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("p_QuestionID", usersolution.Questionid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("p_AnswerID", usersolution.Answerid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("p_UserSolutionText", usersolution.Usersolutiontext, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("p_AttemptDate", usersolution.Attemptdate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            await _dbContext.Connection.ExecuteAsync("UserSolutions_Package.UPDATE_UserSolution", p, commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteUserSolution(int solutionId)
        {
            var p = new DynamicParameters();
            p.Add("UserSolution_ID", solutionId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            await _dbContext.Connection.ExecuteAsync("UserSolutions_Package.DELETE_UserSolution", p, commandType: CommandType.StoredProcedure);

        }


        //public void getUserSolutionByAssessment(Usersolution usersolution)
        //{
        //    var p = new DynamicParameters();
        //    p.Add("p_UserID", usersolution.Userid, dbType: DbType.Int32, direction: ParameterDirection.Input);
        //    p.Add("p_AssessmentID", usersolution.Assessmentid, dbType: DbType.Int32, direction: ParameterDirection.Input);

        //     _dbContext.Connection.ExecuteAsync("UserSolutions_Package.AddUserSolution", p, commandType: CommandType.StoredProcedure);

        //}

        //public async Task<List<Usersolution>> getUserSolutionByAssessment()
        //{
        //    var result = await _dbContext.Connection.QueryAsync<Usersolution>("UserSolutions_Package.getUserSolutionByAssessment", commandType: CommandType.StoredProcedure);

        //    return result.ToList();
        //}
    }
}
