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
    public class PollOptionRepository : IPollOptionRepository
    {
        private readonly IDbContext _dbContext;

        public PollOptionRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async void AddPollOption(Polloption pollOption)
        {
            var p = new DynamicParameters();
            p.Add("p_PollID", pollOption.Pollid, dbType: DbType.Int32, ParameterDirection.Input);
            p.Add("p_OptionText", pollOption.Optiontext, DbType.String, ParameterDirection.Input);

            await _dbContext.Connection.ExecuteAsync("PollOption_Package.AddPollOption", p, commandType: CommandType.StoredProcedure);
        }
    

        public async void DeletePollOption(int optionID)
        {
            var p = new DynamicParameters();
            p.Add("p_OptionID", optionID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            await _dbContext.Connection.ExecuteAsync("PollOption_Package.DeletePollOption", p, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<Polloption>> GetPollOptions(int id)
        {
            var p = new DynamicParameters();
            p.Add("p_PollID", id, dbType: DbType.Int32, ParameterDirection.Input);

            var result = await _dbContext.Connection.QueryAsync<Polloption>("PollOption_Package.GetPollOptions", p, commandType: CommandType.StoredProcedure);
            return result.ToList();

        }

        public async void UpdatePollOption(Polloption pollOption)
        {
            var p = new DynamicParameters();
            p.Add("p_OptionID", pollOption.Optionid, DbType.Int32, ParameterDirection.Input);
            p.Add("p_OptionText", pollOption.Optiontext, DbType.String, ParameterDirection.Input);

            await _dbContext.Connection.ExecuteAsync("PollOption_Package.UpdatePollOption", p, commandType: CommandType.StoredProcedure);
        }

    }
    
}
