using CertifyHub.Core.Data;
using CertifyHub.Core.DTO;
using CertifyHub.Core.Repository;
using CertifyHub.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Infra.Service
{
    public class StdSectionService : IStdSectionService
    {
        private readonly IStdSectionRepository _sectionRepository;

        public StdSectionService(IStdSectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public void CreateStdSection(Stdsection stdsection)
        {
            _sectionRepository.CreateStdSection(stdsection);
        }


        public async Task<List<Stdsection>> GetAllStdSections()
        {
            return await _sectionRepository.GetAllStdSections();
        }
        public async Task<Stdsection> GetStdSectionById(int id)
        {
            return await _sectionRepository.GetStdSectionById(id);
        }

        public async Task<List<Stdsection>> GetStdSectionsBySectionId(int id)
        {
            return await _sectionRepository.GetStdSectionsBySectionId(id);
        }

        public void UpdateStdSection(Stdsection stdsection)
        {
            _sectionRepository.UpdateStdSection(stdsection);
        }
        public async Task<List<StdSectionInfo>> GetStdSectionsInfo()
        {
           return await _sectionRepository.GetStdSectionsInfo();
        }
        public async Task<List<StdSectionInfo>> GetStdSectionsInfoBySectionId(int sectionId)
        {
            return await _sectionRepository.GetStdSectionsInfoBySectionId(sectionId);
        }
        public async Task<List<UserSectionDTO>> GetStdUserSection(int userid)
        {
            return await _sectionRepository.GetStdUserSection(userid);
        }
        public void DeleteStdSection(int studentId, int sectionId)
        {
            _sectionRepository.DeleteStdSection(studentId, sectionId);
        }
        public void SetFlag(int sectionId, int studentId)
        {
            _sectionRepository.SetFlag(sectionId, studentId);
        }
        public Task<List<StudentsSolutionBySectionDTO>> GetStdSectionsBySection(int sectionId, int assessmentId)
        {
            return _sectionRepository.GetStdSectionsBySection(sectionId, assessmentId);
        }
        public async Task<List<StudentGradesDTO>> ListStudentsGrades(int assessmentId)
        {
            return await _sectionRepository.ListStudentsGrades(assessmentId);
        }
        public Task<Certificate> GetCertificateUrl(int userId, int sectionId)
        {
            return _sectionRepository.GetCertificateUrl(userId, sectionId);
        }
        public async Task<List<StudentInfo>> GetStudentsInfoBySectionId(int sectionId)
        {
            return await _sectionRepository.GetStudentsInfoBySectionId(sectionId);
        }
    }
}
