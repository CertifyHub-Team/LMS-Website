using CertifyHub.Core.Common;
using CertifyHub.Core.Data;
using CertifyHub.Core.Repository;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Infra.Repository
{
    public class CertificateRepository : ICertificateRepository
    {
        private readonly IDbContext _dbContext;

        public CertificateRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SaveCertificate(Certificate certificate)
        {
           
            var p = new DynamicParameters();
                p.Add("P_USER_ID", certificate.Userid, DbType.String, ParameterDirection.Input);
                p.Add("P_COURSE_ID", certificate.Courseid, DbType.Int32, ParameterDirection.Input);
                p.Add("P_RELEASE_DATE", certificate.Releasedate, DbType.DateTime, ParameterDirection.Input);
                p.Add("P_EXPIRE_DATE", certificate.Expiredate, DbType.DateTime, ParameterDirection.Input);
                p.Add("P_CERTIFICATEURL", certificate.Certificatecloudinaryurl, DbType.String, ParameterDirection.Input);


            _dbContext.Connection.Execute("Certificate_Package.SaveCertificate", p, commandType: CommandType.StoredProcedure);
      

        }


        public async  Task<List<Certificate>> GetUserCertificates(int userId)
        {

            var p = new DynamicParameters();
            p.Add("P_USER_ID", userId, DbType.String, ParameterDirection.Input);
          // var result = await _dbContext.Connection.QueryAsync<Certificate>("Certificate_Package.GetUserCertificates", p, commandType: CommandType.StoredProcedure);



            var result = await _dbContext.Connection.QueryAsync<Certificate, Course, Certificate>("Certificate_Package.GetUserCertificates",
              (certificate, course) => {
                  certificate.Course = course;
                  return certificate;
              },
              p,
             splitOn: "CourseID",
             
             commandType: CommandType.StoredProcedure);
            return result.ToList();
           
        }

        public async void UpdateUserCertificate(int userId, int courseId, string certificateUrl)
        {
            var p = new DynamicParameters();
            p.Add("P_USER_ID", userId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("P_COURSE_ID", courseId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("P_CERTIFICATEURL", certificateUrl, dbType: DbType.String, direction: ParameterDirection.Input);
            await _dbContext.Connection.ExecuteAsync("Certificate_Package.UpdateUserCertificate", p, commandType: CommandType.StoredProcedure);
        }
    }

}
