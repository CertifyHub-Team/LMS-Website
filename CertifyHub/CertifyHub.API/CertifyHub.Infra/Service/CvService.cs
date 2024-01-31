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
    public class CvService : ICvService
    {
        private readonly ICvRepository _cvRepository; 

        public CvService(ICvRepository cvRepository)
        { 
             _cvRepository = cvRepository;
        }
        public void DeleteCV(int id)
        {
            _cvRepository.DeleteCV(id);
        }

        public Task<List<Cv>> GetAllCVs()
        {
            return _cvRepository.GetAllCVs();
        }

        public Task<List<Cv>> GetCVByQrCode(string qrUrl)
        {
            return _cvRepository.GetCVByQrCode(qrUrl);
        }

        public Task<Cv> GetCVDetails(int id)
        {
            return _cvRepository.GetCVDetails(id);
        }

        public Task<List<Cv>> GetCVsByUser(int userId)
        {
            return _cvRepository.GetCVsByUser(userId);
        }

        public Task<string> GetQRCodeUrlByCVId(int cvId)
        {
            return _cvRepository.GetQRCodeUrlByCVId(cvId);
        }

        public void UpdateCV(Cv cv)
        {
            _cvRepository.UpdateCV(cv);
        }

        public void UploadCV(Cv cv)
        {
            _cvRepository.UploadCV(cv);
        }
        public void UpdateQrCode(int userId, string qrCode)
        {
            _cvRepository.UpdateQrCode(userId, qrCode);
        }

        public Task<UserCvInfo> GetUserCvInfo(int userId)
        {
            return _cvRepository.GetUserCvInfo(userId);
        }

        public Task<UserInfo> GetUserInfo(int userId)
        {
            return _cvRepository.GetUserInfo(userId);
        }
    }
}
