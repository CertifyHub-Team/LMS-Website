using CertifyHub.Core.Common;
using CertifyHub.Core.Data;
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
    public class MaterialRepository : IMaterialRepository
    {

        private readonly IDbContext _dbContext;
        public MaterialRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;  
        }

        public async Task AddMaterial(Material material)
        {
            var p = new DynamicParameters();
            p.Add("Material_Name", material.Materialname, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Material_Path", material.Materialpath, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Course_ID", material.Courseid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("Video_Url", material.Videourl, dbType: DbType.String, direction: ParameterDirection.Input);
            await _dbContext.Connection.ExecuteAsync("Material_Package.AddMaterial", p, commandType: CommandType.StoredProcedure);
          
        }

        public async Task DeleteMaterial(int id)
        {
            var p = new DynamicParameters();
            p.Add("Material_ID", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            await _dbContext.Connection.ExecuteAsync("Material_Package.DeleteMaterial", p, commandType: CommandType.StoredProcedure);
        }
    

        public async Task<List<Material>> GetAllMaterials()
        {

            var result = await _dbContext.Connection.QueryAsync<Material>("Material_Package.GetAllMaterials", commandType: CommandType.StoredProcedure);

            return result.ToList();
        }

        public async Task<List<Material>> GetLatestMaterials(int numMaterials)
        {
            var p = new DynamicParameters();
            p.Add("NumMaterials", numMaterials, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryAsync<Material>("Material_Package.GetLatestMaterials", p, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task<Material> GetMaterialByID(int id)
        {
            var p = new DynamicParameters();
            p.Add("Material_ID", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryAsync<Material>("Material_Package.GetMaterialByID", p, commandType: CommandType.StoredProcedure);
            return  result.FirstOrDefault();
        }

        public async Task<List<Material>> GetMaterialsByCourse(int courseId)
        {
            var p = new DynamicParameters();
            p.Add("Course_ID", courseId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryAsync<Material>("Material_Package.GetMaterialsByCourse", p, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task UpdateMaterial(Material material)
        {
            var p = new DynamicParameters();
            p.Add("Material_ID", material.Materialid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("Material_Name", material.Materialname, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Material_Path", material.Materialpath, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Course_ID", material.Courseid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("Video_Url", material.Videourl, dbType: DbType.String, direction: ParameterDirection.Input);
            await _dbContext.Connection.ExecuteAsync("Material_Package.UpdateMaterial", p, commandType: CommandType.StoredProcedure);


        }
    }
}
