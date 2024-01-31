using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CertifyHub.Core.Common;
using CertifyHub.Core.Data;
using CertifyHub.Core.Repository;
using Dapper;

namespace CertifyHub.Infra.Repository
{
    public class ReviewInstructorRepository : IReviewInstructorRepository
    {
        private readonly IDbContext _dbContext;

        public ReviewInstructorRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void CreateReview(Reviewinstructor reviewInstructor)
        {
            var p = new DynamicParameters();
            p.Add("Review_Content", reviewInstructor.Reviewcontent, DbType.String, ParameterDirection.Input);
            p.Add("Review_Rating", reviewInstructor.Rating, DbType.Int32, ParameterDirection.Input);
            p.Add("Review_Date", reviewInstructor.Reviewdate, DbType.DateTime, ParameterDirection.Input);
            p.Add("Section_ID", reviewInstructor.Sectionid, DbType.Int32, ParameterDirection.Input);

            _dbContext.Connection.Execute("ReviewInstructor_Package.CreateReview", p, commandType: CommandType.StoredProcedure);
        }

        public void DeleteReview(int id)
        {
            var p = new DynamicParameters();
            p.Add("Review_Id", id, DbType.Int32, ParameterDirection.Input);

            _dbContext.Connection.Execute("ReviewInstructor_Package.DeleteReview", p, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<Reviewinstructor>> GetAllReviews()
        {
            var result = await _dbContext.Connection.QueryAsync<Reviewinstructor>("ReviewInstructor_Package.GetAllReviews", commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task<List<Reviewinstructor>> GetLastThreeHighRatedReviews()
        {
            var result = await _dbContext.Connection.QueryAsync<Reviewinstructor>("ReviewInstructor_Package.GetLastThreeHighRatedReviews", commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task<Reviewinstructor> GetReviewById(int id)
        {
            var p = new DynamicParameters();
            p.Add("Review_Id", id, DbType.Int32, ParameterDirection.Input);

            var result = await _dbContext.Connection.QueryAsync<Reviewinstructor>("ReviewInstructor_Package.GetReviewById", p, commandType: CommandType.StoredProcedure);
            return result.FirstOrDefault();
        }

       

    }
}
