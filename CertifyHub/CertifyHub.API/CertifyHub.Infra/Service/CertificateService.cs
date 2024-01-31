using CertifyHub.Core.Data;
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
    public class CertificateService : ICertificateService
    {

        private readonly ICertificateRepository _certificateRepository;

        public CertificateService(ICertificateRepository certificateRepository)
        {
            _certificateRepository = certificateRepository;
        }

        public async Task<List<Certificate>>GetUserCertificates(int userId)
        {
            return await _certificateRepository.GetUserCertificates(userId);
        }

        public void SaveCertificate(Certificate certificate)
        {
            _certificateRepository.SaveCertificate(certificate);

        }
        public void UpdateUserCertificate(int userId, int courseId, string certificateUrl)
        {
            _certificateRepository.UpdateUserCertificate(userId, courseId, certificateUrl);
        }



    }
}
