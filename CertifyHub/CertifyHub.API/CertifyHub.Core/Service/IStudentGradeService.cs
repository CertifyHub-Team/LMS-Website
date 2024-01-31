using CertifyHub.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Core.Service
{
    public interface IStudentGradeService
    {
        Task<List<CertificateDto>> GetStudentGradeInfo(int userId, int sectionId);

    }
}
