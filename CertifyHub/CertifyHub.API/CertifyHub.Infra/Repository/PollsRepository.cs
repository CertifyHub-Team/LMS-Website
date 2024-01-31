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
    public class PollsRepository : IPollsRepository
    {
        private readonly IDbContext _dbContext;
        public PollsRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async void CreatePoll(Poll poll)
        {
            var p = new DynamicParameters();
            p.Add("p_Title", poll.Title, DbType.String, ParameterDirection.Input);
            p.Add("p_Description", poll.Description, DbType.String, ParameterDirection.Input);
            p.Add("p_StartDate", poll.Startdate, DbType.DateTime, ParameterDirection.Input);
            p.Add("p_EndDate", poll.Enddate, DbType.DateTime, ParameterDirection.Input);
            p.Add("p_Section", poll.Sectionid, DbType.Int32, ParameterDirection.Input);

            await _dbContext.Connection.ExecuteAsync("Poll_Package.CreatePoll", p, commandType: CommandType.StoredProcedure);
        }

        public async void UpdatePoll(Poll poll)
        {
            var p = new DynamicParameters();
            p.Add("p_PollID", poll.Pollid, DbType.Int32, ParameterDirection.Input);
            p.Add("p_Title", poll.Title, DbType.String, ParameterDirection.Input);
            p.Add("p_Description", poll.Description, DbType.String, ParameterDirection.Input);
            p.Add("p_StartDate", poll.Startdate, DbType.DateTime, ParameterDirection.Input);
            p.Add("p_EndDate", poll.Enddate, DbType.DateTime, ParameterDirection.Input);
            p.Add("p_Section", poll.Sectionid, DbType.Int32, ParameterDirection.Input);

            await _dbContext.Connection.ExecuteAsync("Poll_Package.UpdatePoll", p, commandType: CommandType.StoredProcedure);
        }

        public async void DeletePoll(int pollId)
        {
            var p = new DynamicParameters();
            p.Add("p_PollID", pollId, DbType.Int32, ParameterDirection.Input);

            await _dbContext.Connection.ExecuteAsync("Poll_Package.DeletePoll", p, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<Poll>> GetAllPolls()
        {
            var result = await _dbContext.Connection.QueryAsync<Poll>("Poll_Package.GetAllPolls", commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task<Poll> GetPollById(int pollId)
        {
            var p = new DynamicParameters();
            p.Add("p_PollID", pollId, DbType.Int32, ParameterDirection.Input);

            var result = await _dbContext.Connection.QueryAsync<Poll>("Poll_Package.GetPollByID", p, commandType: CommandType.StoredProcedure);
            return result.FirstOrDefault();
        }

       

        

    }
}
