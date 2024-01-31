using CertifyHub.Core.Common;
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
    public class StudentGradeRepository : IStudentGradeRepository
    {

        private readonly IDbContext _dbContext;

        public StudentGradeRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<List<CertificateDto>> GetStudentGradeInfo(int userId, int sectionId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("USER_ID", userId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("SECTION_ID", sectionId, DbType.Int32, ParameterDirection.Input);

            var result = await _dbContext.Connection.QueryAsync<CertificateDto>("STUDENT_GRADE_PACKAGE.GET_STUDENT_GRADE_INFO", parameters, commandType: CommandType.StoredProcedure);

            
            return result.ToList();
        }
    }
}
