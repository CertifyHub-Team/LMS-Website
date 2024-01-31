using CertifyHub.Core.DTO;
using CertifyHub.Core.Repository;
using CertifyHub.Core.Service;
using CertifyHub.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Infra.Service
{
    public class StudentGradeService : IStudentGradeService
    {
        private readonly IStudentGradeRepository _studentGradeRepository;
        public StudentGradeService(IStudentGradeRepository studentGradeRepository)
        {
            _studentGradeRepository = studentGradeRepository;
        }

        public async Task<List<CertificateDto>> GetStudentGradeInfo(int userId, int sectionId)
        {
            return await _studentGradeRepository.GetStudentGradeInfo(userId, sectionId);
        }
    }
}