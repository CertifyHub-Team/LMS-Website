using CertifyHub.Core.Data;
using CertifyHub.Core.DTO;
using CertifyHub.Core.Service;
using CertifyHub.Infra.Service;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CertifyHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramController : ControllerBase
    {

        private readonly IProgramService _programService;
        public ProgramController(IProgramService programService)
        {
            _programService = programService;
        }


        [HttpGet]
        [Route("GetAllPrograms")]
        public async Task<List<CertifyHub.Core.Data.Program>> GetAllPrograms()
        {
            return await _programService.GetAllPrograms();
        }

        [HttpGet]
        [Route("GetAdminPrograms")]
        public async Task<List<CertifyHub.Core.Data.Program>> GetAdminPrograms()
        {
            return await _programService.GetAdminPrograms();
        }


        [HttpGet]
        [Route("GetProgramById")]
        public async Task<CertifyHub.Core.Data.Program> GetProgramByID(int id)
        {
            return await _programService.GetProgramByID(id);
  
        }
        [HttpPost]
        [Route("UploadImage")]
        public CertifyHub.Core.Data.Program UploadImage()
        {
            var file = Request.Form.Files[0];//the file is within the request with => form data => file,if uploded more than one file, in this case only one file so its index is 0
            var fileName = $"{Guid.NewGuid().ToString()}_{file.FileName}"; //change the name to avoid duplicate
            //var fileName = Guid.NewGuid().ToString() + "_" + file.FileName";
            var fullPath = Path.Combine("D:\\Downloads\\CertifyHub 2\\CertifyHub\\CertifyHub\\src\\assets\\Images", fileName); //define the path string
            using (var stream = new FileStream(fullPath, FileMode.Create)) //create the path
            {
                file.CopyTo(stream); //append the file to  the path
            }

            CertifyHub.Core.Data.Program program = new CertifyHub.Core.Data.Program(); //it is better to return object than a single value
            program.Imagepath = fileName;

            return program;
        }
        [HttpPost]
        [Route("CreateProgram")]
        public async void CreateProgram(CertifyHub.Core.Data.Program program)
        {

            _programService.CreateProgram(program);
        }

        [HttpPut]
        [Route("UpdateProgram")]
        public async void UpdateProgram(CertifyHub.Core.Data.Program program)
        {
            _programService.UpdateProgram(program);
        }

        [HttpDelete("{id}")]
        public async void DeleteProgram(int id)
        {
              _programService.DeleteProgram(id);

        }

        [HttpGet]
        [Route("GetStudentProgram/{id}")]
        public async Task<List<CertifyHub.Core.Data.Program>> GetStudentProgram(int id)
        {
           return await _programService.GetStudentProgram(id);
        }
        [HttpPut]
        [Route("UpdateProgramImage/{id}/{newImagePath}")]
        public void UpdateProgramImage(int id, string newImagePath)
        {
            _programService.UpdateProgramImage(id, newImagePath);
        }
        [HttpGet]
        [Route("GetProgramStudentCounts")]
        public async Task<List<ProgramStudentCount>> GetProgramStudentCounts()
        {
            return await _programService.GetProgramStudentCounts();

        }

    }
}
