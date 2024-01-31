using CertifyHub.Core.Common;
using CertifyHub.Core.Data;
using CertifyHub.Core.DTO;
using CertifyHub.Core.Repository;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Infra.Repository
{
    public class CvRepository : ICvRepository
    {
        private readonly IDbContext _dbContext;

        public CvRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void DeleteCV(int id)
        {
            var p = new DynamicParameters();
            p.Add("cv_id", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            _dbContext.Connection.ExecuteAsync("CVs_Package.DeleteCV", p, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<List<Cv>> GetAllCVs()
        {
            var result = await _dbContext.Connection.QueryAsync<Cv>("CVs_Package.GetAllCVs", commandType: CommandType.StoredProcedure);

            return result.ToList();
        }

        public async Task<List<Cv>> GetCVByQrCode(string qrUrl)
        {
            var p = new DynamicParameters();
            p.Add("QrCode_url", qrUrl, dbType: DbType.String, direction: ParameterDirection.Input);

            var result = await _dbContext.Connection.QueryAsync<Cv>("CVs_Package.GetCVByQrCode", p, commandType: CommandType.StoredProcedure);

            return result.ToList();
        }

        public async Task<Cv> GetCVDetails(int id)
        {
            var p = new DynamicParameters();
            p.Add("cv_id", id, dbType: DbType.Int32, ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryAsync<Cv>("CVs_Package.GetCVDetails", p, commandType: CommandType.StoredProcedure);
            return result.FirstOrDefault();
        }

        public async Task<List<Cv>> GetCVsByUser(int userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("user_id", userId, dbType: DbType.Int32, ParameterDirection.Input);

            var result = await _dbContext.Connection.QueryAsync<Cv>("CVs_Package.GetCVsByUser", parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task<string> GetQRCodeUrlByCVId(int cvId)
        {
            var p = new DynamicParameters();
            p.Add("cv_id", cvId, dbType: DbType.Int32, ParameterDirection.Input);

            var result = await _dbContext.Connection.QueryFirstOrDefaultAsync<Cv>(
                "CVs_Package.GetCVDetails",
                p,
                commandType: CommandType.StoredProcedure);

            return result?.Qrcodeurl;
        }

        public void UpdateCV(Cv cv)
        {
            var p = new DynamicParameters();
            p.Add("cv_id", cv.Cvid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("user_id", cv.Userid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("GH_Link", cv.Githublink, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("user_experience", cv.Experience, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("u_gpa", cv.Gpa, dbType: DbType.Double, direction: ParameterDirection.Input);
            p.Add("rate", cv.Rating, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("u_certificate", cv.Certificates, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("u_Education", cv.Education, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("u_major", cv.Major, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("u_projects", cv.Projects, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("u_interests", cv.Interests, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("LI_Link", cv.Linkedintlink, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("u_skills", cv.Skills, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("u_languages", cv.Languages, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("QrCode_url", cv.Qrcodeurl, dbType: DbType.String, direction: ParameterDirection.Input);
            _dbContext.Connection.ExecuteAsync("CVs_Package.UpdateCV", p, commandType: System.Data.CommandType.StoredProcedure);

        }

        public void UploadCV(Cv cv)
        {
            var p = new DynamicParameters();
            p.Add("user_id", cv.Userid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("GH_Link", cv.Githublink, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("user_experience", cv.Experience, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("u_gpa", cv.Gpa, dbType: DbType.Double, direction: ParameterDirection.Input);
            p.Add("rate", cv.Rating, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("u_certificate", cv.Certificates, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("u_Education", cv.Education, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("u_major", cv.Major, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("u_projects", cv.Projects, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("u_interests", cv.Interests, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("LI_Link", cv.Linkedintlink, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("u_skills", cv.Skills, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("u_languages", cv.Languages, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("QrCode_url", cv.Qrcodeurl, dbType: DbType.String, direction: ParameterDirection.Input);
            _dbContext.Connection.ExecuteAsync("CVs_Package.UploadCV", p, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void UpdateQrCode(int userId, string qrCode)
        {


            var p = new DynamicParameters();
            p.Add("user_id", userId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("QrCode_url", qrCode, dbType: DbType.String, direction: ParameterDirection.Input);
            _dbContext.Connection.ExecuteAsync("CVs_Package.UpdateQrCode", p, commandType: System.Data.CommandType.StoredProcedure);


        }

        public async Task<UserCvInfo> GetUserCvInfo(int userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("user_id", userId, dbType: DbType.Int32, ParameterDirection.Input);

            var result = await _dbContext.Connection.QueryAsync<UserCvInfo>("CVs_Package.GetUserCvInfo", parameters, commandType: CommandType.StoredProcedure);
            return result.FirstOrDefault();
        }

        public async Task<UserInfo> GetUserInfo(int userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("user_id", userId, dbType: DbType.Int32, ParameterDirection.Input);

            var result = await _dbContext.Connection.QueryAsync<UserInfo>("CVs_Package.GetUserInfo", parameters, commandType: CommandType.StoredProcedure);
            return result.FirstOrDefault();
        }
    }
}
