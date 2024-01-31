using CertifyHub.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Core.Service
{
    public interface ILoginService
    {
        Task<List<Login>> GetAllLogins();
        Task<Login> GetLoginById(int id);
        void CreateLogin(Login login);
        void UpdateLogin(Login login);
        void DeleteLogin(int id);
        void UpdatePassword(Login login);
        string UserLogin(Login login);
        Task<int> GetInstructorCount();
        Task<int> GetStudentCount();

    }
}
