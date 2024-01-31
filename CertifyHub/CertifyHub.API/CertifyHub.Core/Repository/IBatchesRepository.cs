using CertifyHub.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Core.Repository
{
    public interface IBatchesRepository
    {
        void CreateBatch(Batch batch);
        void UpdateNumberOfBatches(Batch batch);
        void DeleteBatch(int id);
        Task<Batch> GetBatchesByProgramID(int id);
        Task<List<Batch>> GetAllBatches();
    }
}
