using CertifyHub.Core.Common;
using CertifyHub.Core.Data;
using CertifyHub.Core.DTO;
using CertifyHub.Core.Repository;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace CertifyHub.Infra.Repository
{
    public class GradeRepository : IGradeRepository
    {
        private readonly IDbContext _dbContext;

        public GradeRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<double> CalculateUserGrade(int userid)
        {
            var p = new DynamicParameters();
            p.Add("p_userid", userid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("p_final_grade", dbType: DbType.Double, direction: ParameterDirection.Output);
            await _dbContext.Connection.ExecuteAsync("Grades_Package.CalculateUserGrade", p, commandType: CommandType.StoredProcedure);
            double finalGrade = p.Get<double>("p_final_grade");
            return finalGrade;
        }

        public async Task<string> ConvertToLetterGrade(double finalGrade)
        {
            var p = new DynamicParameters();
            p.Add("p_final_grade", finalGrade, dbType: DbType.Double, direction: ParameterDirection.Input);
            p.Add("p_letter_grade", dbType: DbType.String, size: 10, direction: ParameterDirection.Output);
            await _dbContext.Connection.ExecuteAsync("Grades_Package.ConvertToLetterGrade", p, commandType: CommandType.StoredProcedure);
            string GradeLetter = p.Get<string>("p_letter_grade");
            return GradeLetter;
        }

        public async void CreateGrade(Grade grade)
        {
            var p = new DynamicParameters();
            p.Add("p_grade", grade.Studentgrade, dbType: DbType.Double, direction: ParameterDirection.Input);
            p.Add("p_userid", grade.Userid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("p_assessmentid", grade.Assessmantid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("p_notes", grade.Notes, dbType: DbType.String, direction: ParameterDirection.Input);
            await _dbContext.Connection.ExecuteAsync("Grades_Package.CreateGrade", p, commandType: CommandType.StoredProcedure);
        }

        public async void DeleteGrade(int gradeid)
        {
            var p = new DynamicParameters();
            p.Add("p_gradeid", gradeid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            await _dbContext.Connection.ExecuteAsync("Grades_Package.DeleteGrade", p, commandType: CommandType.StoredProcedure);

        }

        public async Task<Grade> GetGradeById(int gradeid)
        {
            var p = new DynamicParameters();
            p.Add("p_gradeid", gradeid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryAsync<Grade>("Grades_Package.GetGradeById", p, commandType: CommandType.StoredProcedure);
            return result.FirstOrDefault();
        }

        public  async Task<Grade> GetUserGrade(int userid , int assessmentid)
        {
            var p = new DynamicParameters();
            p.Add("p_userid", userid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("p_assessmentid", assessmentid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryAsync<Grade, Assessment, Grade>("Grades_Package.GetUserGrade",
            (grade, assessment) =>
            {
                grade.Assessmant = assessment;
                return grade;
            },
            
                p,
                splitOn: "assessmentid",
                commandType: CommandType.StoredProcedure) ;
            return result.FirstOrDefault();
        }

        public async void UpdateGrade(Grade grade)
        {
            var p = new DynamicParameters();
            p.Add("p_grade", grade.Studentgrade, dbType: DbType.Double, direction: ParameterDirection.Input);
            p.Add("p_userid", grade.Userid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("p_assessmentid", grade.Assessmantid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            await _dbContext.Connection.ExecuteAsync("Grades_Package.UpdateGrade", p, commandType: CommandType.StoredProcedure);
        }
        public async Task<List<StudentGradesDTO>> GetGradeByUserId(int userid)
        {
            var p = new DynamicParameters();
            p.Add("User_Id", userid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryAsync<StudentGradesDTO>("Grades_Package.GetGradeByUserId", p, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
    }
}
