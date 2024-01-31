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
    public class AboutusRepository : IAboutusRepository
    {
        private readonly IDbContext _dbContext; 

        public AboutusRepository (IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async void CreateAboutUs(Aboutu Aboutus)
        {
            var p = new DynamicParameters();
            p.Add("p_AboutUsText", Aboutus.Aboutustext, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("p_LastUpdated", Aboutus.Lastupdated, dbType: DbType.Date, direction: ParameterDirection.Input);
            await _dbContext.Connection.ExecuteAsync("AboutUs_Package.CreateAboutUs", p, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<Aboutu>> GetAllAboutus()
        {
            var result = await _dbContext.Connection.QueryAsync<Aboutu>("AboutUs_Package.GetAllAboutus", commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async void UpdateAboutUs(Aboutu Aboutus)
        {
            var p = new DynamicParameters();
            p.Add("p_AboutUsID", Aboutus.Aboutusid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("p_AboutUsText", Aboutus.Aboutustext, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("p_LastUpdated", Aboutus.Lastupdated, dbType: DbType.Date, direction: ParameterDirection.Input);
            await _dbContext.Connection.ExecuteAsync("AboutUs_Package.UpdateAboutUs", p, commandType: CommandType.StoredProcedure);
        }
    }
}
