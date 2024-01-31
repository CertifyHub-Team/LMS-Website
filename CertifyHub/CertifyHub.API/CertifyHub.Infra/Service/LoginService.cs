using CertifyHub.Core.Data;
using CertifyHub.Core.Repository;
using CertifyHub.Core.Service;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Infra.Service
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;

        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public void CreateLogin(Login login)
        {
            _loginRepository.CreateLogin(login);
        }

        public void DeleteLogin(int id)
        {
           _loginRepository.DeleteLogin(id);
        }

        public async Task<List<Login>> GetAllLogins()
        {
          return await _loginRepository.GetAllLogins();
        }

        public async Task<Login> GetLoginById(int id)
        {
            return await _loginRepository.GetLoginById(id);
        }

        public void UpdateLogin(Login login)
        {
            _loginRepository.UpdateLogin(login);
        }

        public void UpdatePassword(Login login)
        {
            _loginRepository.UpdatePassword(login);
        }

        public string UserLogin(Login login)
        {
            var checkLogin = _loginRepository.UserLogin(login);
            if (checkLogin == null)
            {
                return null;
            }
            else
            {
                var symSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345xzxzxzxzxzxzxzxzxzxzxzxzxzxzxzxzxzxsuperSecretKey@345xzxzxzxzxzxzxzxzxzxzxzxzxzxzxzxzxzxA"));
                var sCredentials = new SigningCredentials(symSecurityKey, SecurityAlgorithms.HmacSha256);

                var claimss = new List<Claim>
                {
                    new Claim("UserId", checkLogin.Userid.ToString()),
                    new Claim("RoleId", checkLogin.Roleid.ToString())
                };

                var token = new JwtSecurityToken
                    (
                    claims: claimss,
                    signingCredentials: sCredentials,
                    expires: DateTime.Now.AddHours(1)
                    );

                var genToken = new JwtSecurityTokenHandler().WriteToken(token);
                return genToken;
            }
        }

        public async Task<int> GetInstructorCount()
        {
            return await _loginRepository.GetInstructorCount();
        }

        public async Task<int> GetStudentCount()
        {
            return await _loginRepository.GetStudentCount();
        }
    }
}
