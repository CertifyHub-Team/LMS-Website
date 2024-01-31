using CertifyHub.Core.Data;
using CertifyHub.Core.DTO;
using CertifyHub.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CertifyHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StdSectionController : ControllerBase
    {
        private readonly IStdSectionService _stdSectionService;

        public StdSectionController(IStdSectionService stdSectionService)
        {
            _stdSectionService = stdSectionService;
        }

        [HttpGet]
        [Route("GetAllStdSections")]
        public async Task<List<Stdsection>> GetAllStdSections()
        {
            return await _stdSectionService.GetAllStdSections();
        }

        [HttpGet]
        [Route("GetStdSectionById/{id}")]
        public async Task<Stdsection> GetStdSectionById(int id)
        {
            return await _stdSectionService.GetStdSectionById(id);
        }

        [HttpGet]
        [Route("GetStdSectionsBySectionId/{id}")]
        public async Task<List<Stdsection>> GetStdSectionsBySectionId(int id)
        {
            return await _stdSectionService.GetStdSectionsBySectionId(id);
        }

        [HttpPost]
        [Route("CreateStdSection")]
        public void CreateStdSection(Stdsection stdsection)
        {
            _stdSectionService.CreateStdSection(stdsection);
        }

        [HttpPut]
        [Route("UpdateStdSection")]
        public void UpdateStdSection(Stdsection stdsection)
        {
            _stdSectionService.UpdateStdSection(stdsection);
        }

        [HttpDelete]
        [Route("DeleteStdSection/{studentId}/{sectionId}")]
        public void DeleteStdSection(int studentId, int sectionId)
        {
            _stdSectionService.DeleteStdSection(studentId, sectionId);
        }

        [HttpGet]
        [Route("GetStdSectionsInfo")]
        public async Task<List<StdSectionInfo>> GetStdSectionsInfo()
        {
            return await _stdSectionService.GetStdSectionsInfo();
        }
        [HttpGet]
        [Route("GetStdSectionsInfoBySectionId/{sectionId}")]
        public async Task<List<StdSectionInfo>> GetStdSectionsInfoBySectionId(int sectionId)
        {
            return await _stdSectionService.GetStdSectionsInfoBySectionId(sectionId);
        }

        [HttpGet]
        [Route("GetUserSection/{userid}")]
        public async Task<List<UserSectionDTO>> GetStdUserSection(int userid)
        {
            return await _stdSectionService.GetStdUserSection(userid);
        }

        [HttpPut]
        [Route("SetFlag/{sectionId}/{studentId}")]
        public void SetFlag(int sectionId, int studentId)
        {
            _stdSectionService.SetFlag(sectionId, studentId);
        }

        [HttpGet]
        [Route("GetStdSectionsBySection/{sectionId}/{assessmentId}")]
        public Task<List<StudentsSolutionBySectionDTO>> GetStdSectionsBySection(int sectionId, int assessmentId)
        {
            return _stdSectionService.GetStdSectionsBySection(sectionId, assessmentId);
        }
        [HttpGet]
        [Route("ListStudentsGrades/{assessmentId}")]
        public Task<List<StudentGradesDTO>> ListStudentsGrades(int assessmentId)
        {
            return _stdSectionService.ListStudentsGrades(assessmentId);
        }
        [HttpGet]
        [Route("GetCertificateUrl/{userId}/{sectionId}")]
        public Task<Certificate> GetCertificateUrl(int userId, int sectionId)
        {
            return _stdSectionService.GetCertificateUrl(userId, sectionId);
        }

        [HttpGet]
        [Route("StudentsInfoForSection/{sectionId}")]
        public async Task<List<StudentInfo>> GetStudentsInfoBySectionId(int sectionId)
        {
            return await _stdSectionService.GetStudentsInfoBySectionId(sectionId);
        }
    }
}
