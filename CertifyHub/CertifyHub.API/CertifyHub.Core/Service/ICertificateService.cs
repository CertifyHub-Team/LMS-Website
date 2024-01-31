using CertifyHub.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Core.Service
{
    public interface ICertificateService
    {
        void SaveCertificate(Certificate certificate);
        Task<List<Certificate>>GetUserCertificates(int userId);
        void UpdateUserCertificate(int userId, int courseId, string certificateUrl);
    }
}
