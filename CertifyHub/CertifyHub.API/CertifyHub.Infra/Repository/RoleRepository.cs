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
    public class RoleRepository : IRoleRepository
    {
        private readonly IDbContext _dbContext;

        public RoleRepository(IDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public void CreateRole(Role role)
        {
            var p = new DynamicParameters();
            p.Add("ROLE_NAME", role.Rolename, dbType: DbType.String, direction: ParameterDirection.Input);
            _dbContext.Connection.ExecuteAsync("ROLE_PACKAGE.CREATE_ROLE", p, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void DeleteRole(int id)
        {
            var p = new DynamicParameters();
            p.Add("ROLE_ID", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            _dbContext.Connection.ExecuteAsync("ROLE_PACKAGE.DELETE_ROLE", p, commandType: System.Data.CommandType.StoredProcedure);

        }

        public async Task<List<Role>> GetAllRoles()
        {
            var result = await _dbContext.Connection.QueryAsync<Role>("ROLE_PACKAGE.GET_ALL_ROLES", commandType: System.Data.CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task<Role> GetRoleById(int id)
        {
            var p = new DynamicParameters();
            p.Add("ID", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryAsync<Role>("ROLE_PACKAGE.GET_ROLE_BY_ID", p, commandType: System.Data.CommandType.StoredProcedure);
            return result.FirstOrDefault();
        }

        public void UpdateRole(Role role)
        {
            var p = new DynamicParameters();
            p.Add("ROLE_ID", role.Roleid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("ROLE_NAME", role.Rolename, dbType: DbType.String, direction: ParameterDirection.Input);
            _dbContext.Connection.ExecuteAsync("ROLE_PACKAGE.UPDATE_ROLE", p, commandType: System.Data.CommandType.StoredProcedure);
        }


    }

}
