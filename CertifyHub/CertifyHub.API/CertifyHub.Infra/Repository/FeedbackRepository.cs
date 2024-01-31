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
    public class FeedbackRepository : IFeedbackRepository
    {

        private readonly IDbContext _dbContext;

        public FeedbackRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateFeedback(Feedback feedback)
        {
            var p = new DynamicParameters();
            p.Add("Feedback_Content", feedback.Feedbackcontent, DbType.String, ParameterDirection.Input);
            p.Add("Feedback_Rating", feedback.Rating, DbType.Int32, ParameterDirection.Input);
            p.Add("Feedback_Date", feedback.Feedbackdate, DbType.DateTime, ParameterDirection.Input);
            p.Add("Section_ID", feedback.Sectionid, DbType.Int32, ParameterDirection.Input);
            _dbContext.Connection.Execute("FeedbackPackage.CreateFeedback", p, commandType: CommandType.StoredProcedure);
        }

        public void DeleteFeedback(int id)
        {
            var p = new DynamicParameters();
            p.Add("Feedback_Id", id, DbType.Int32, ParameterDirection.Input);

            _dbContext.Connection.Execute("FeedbackPackage.DeleteFeedback", p, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<Feedback>> GetAllFeedback()
        {
            var result = _dbContext.Connection.Query<Feedback>("FeedbackPackage.GetAllFeedbacks", commandType: CommandType.StoredProcedure);
            return  result.ToList();
        }

        

        
    }
}
