using CertifyHub.Core.Data;
using CertifyHub.Core.Repository;
using CertifyHub.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Infra.Service
{
    public class BatchesService : IBatchesService
    {
        private readonly IBatchesRepository _batchesRepository;

        public BatchesService(IBatchesRepository batchesRepository)
        {
            _batchesRepository = batchesRepository;
        }


        public void CreateBatch(Batch batch)
        {
            _batchesRepository.CreateBatch(batch);
        }

        public void DeleteBatch(int id)
        {
             _batchesRepository.DeleteBatch(id);
        }

        public Task<List<Batch>> GetAllBatches()
        {
            return _batchesRepository.GetAllBatches();
        }

        public Task<Batch> GetBatchesByProgramID(int id)
        {
            return _batchesRepository.GetBatchesByProgramID(id);
        }

        public void UpdateNumberOfBatches(Batch batch)
        {
            _batchesRepository.UpdateNumberOfBatches(batch);
        }
    }
}
