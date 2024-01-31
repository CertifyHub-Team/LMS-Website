using CertifyHub.Core.Data;
using CertifyHub.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Core.Repository
{
    public interface ICvRepository
    {
        
        void UploadCV(Cv cv);
        void UpdateCV(Cv cv);
        void DeleteCV(int id);
        Task<List<Cv>> GetCVsByUser(int userId);
        Task<UserCvInfo> GetUserCvInfo(int userId);
        Task<UserInfo> GetUserInfo(int userId);
        Task<Cv> GetCVDetails(int id);
        Task<List<Cv>> GetCVByQrCode(string qrUrl);
        Task<List<Cv>> GetAllCVs();
        Task<string> GetQRCodeUrlByCVId(int cvId);
        void UpdateQrCode(int userId, string qrCode);
    }
}
