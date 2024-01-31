using CertifyHub.Core.Data;
using CertifyHub.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CertifyHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : ControllerBase
    {

        private readonly IBatchesService _batchesService;
        public BatchController(IBatchesService batchesService)
        {
            _batchesService = batchesService;
        }

        [HttpPost]
        public void CreateBatch(Batch batch)
        {
            _batchesService.CreateBatch(batch);
        }
        [HttpDelete("{id}")]
        public void DeleteBatch(int id)
        {
            _batchesService.DeleteBatch(id);
        }
        [HttpGet]
        public Task<List<Batch>> GetAllBatches()
        {
            return _batchesService.GetAllBatches();
        }
        [HttpGet]
        [Route("GetBatches/{id}")]
        public Task<Batch> GetBatchesByProgramID(int id)
        {
            return _batchesService.GetBatchesByProgramID(id);
        }
        [HttpPut]
        public void UpdateNumberOfBatches(Batch batch)
        {
            _batchesService.UpdateNumberOfBatches(batch);
        }


    }
}
