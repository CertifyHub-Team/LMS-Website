using CertifyHub.Core.Data;
using CertifyHub.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Core.Repository
{
    public interface IStdSectionRepository
    {
        Task<List<Stdsection>> GetAllStdSections();
        Task<Stdsection> GetStdSectionById(int id);
        Task<List<Stdsection>> GetStdSectionsBySectionId(int id);
        void CreateStdSection(Stdsection stdsection);
        void UpdateStdSection(Stdsection stdsection);
        Task<List<StdSectionInfo>> GetStdSectionsInfo();
        Task<List<StdSectionInfo>> GetStdSectionsInfoBySectionId(int sectionId);
        Task<List<UserSectionDTO>> GetStdUserSection(int userid);
        void DeleteStdSection(int studentId, int sectionId);
        void SetFlag(int sectionId, int studentId);
        Task<List<StudentsSolutionBySectionDTO>> GetStdSectionsBySection(int sectionId, int assessmentId);
        Task<List<StudentGradesDTO>> ListStudentsGrades(int assessmentId);
         Task<Certificate> GetCertificateUrl(int userId, int sectionId);
        Task<List<StudentInfo>> GetStudentsInfoBySectionId(int sectionId);

    }
}
