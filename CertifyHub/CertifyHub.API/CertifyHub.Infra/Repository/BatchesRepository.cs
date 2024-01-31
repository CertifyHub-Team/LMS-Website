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
    public class BatchesRepository : IBatchesRepository
    {
        private readonly IDbContext _dbContext;

        public BatchesRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async void CreateBatch(Batch batch)
        {
            var p = new DynamicParameters();
            p.Add("p_ProgramID", batch.Programid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("p_NumberOfBatches", batch.Numberofbatches, dbType: DbType.Int32, direction: ParameterDirection.Input);
        
           await _dbContext.Connection.ExecuteAsync("Batch_Package.CreateBatch", p, commandType: CommandType.StoredProcedure);

        }

        public async void DeleteBatch(int id)
        {
            var p = new DynamicParameters();
            p.Add("p_BatchID", id, dbType: DbType.Int32, ParameterDirection.Input);
          await  _dbContext.Connection.ExecuteAsync("Batch_Package.DeleteBatch", p, commandType: CommandType.StoredProcedure);

        }

        public async Task<List<Batch>> GetAllBatches()
        {
            var result = await _dbContext.Connection.QueryAsync<Batch>("Batch_Package.GetAllBatches", commandType: CommandType.StoredProcedure);

            return result.ToList();
        }

        public async Task<Batch> GetBatchesByProgramID(int id)
        {
            var p = new DynamicParameters();
            p.Add("p_ProgramID", id, dbType: DbType.Int32, ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryAsync<Batch>("Batch_Package.GetBatchesByProgramID", p, commandType: CommandType.StoredProcedure);
            return result.FirstOrDefault();

        }

        public async void UpdateNumberOfBatches(Batch batch)
        {
            var p = new DynamicParameters();
            p.Add("p_BatchID", batch.Batchesid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("p_NewNumberOfBatches", batch.Numberofbatches, dbType: DbType.Int32, direction: ParameterDirection.Input);
         
            await _dbContext.Connection.ExecuteAsync("Batch_Package.UpdateNumberOfBatches", p, commandType: CommandType.StoredProcedure);
        }
    }
}
