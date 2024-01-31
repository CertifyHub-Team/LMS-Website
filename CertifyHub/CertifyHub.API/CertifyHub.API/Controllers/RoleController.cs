using CertifyHub.Core.Data;
using CertifyHub.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CertifyHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        [Route("CreateRole")]
        public void CreateRole(Role role)
        {
             _roleService.CreateRole(role);
        }

        [HttpPut]
        [Route("UpdateRole")]
        public void UpdateRole(Role role)
        {
             _roleService.UpdateRole(role);
        }

        [HttpDelete]
        [Route("DeleteRole/{id}")]
        public void DeleteRole(int id)
        {
             _roleService.DeleteRole(id);
        }

        [HttpGet]
        [Route("GetAllRoles")]
        public async Task<List<Role>> GetAllRoles()
        {
            return await _roleService.GetAllRoles();
        }

        [HttpGet]
        [Route("GetRoleById/{id}")]
        public async Task<Role> GetRoleById(int id)
        {
            return await _roleService.GetRoleById(id);
        }
    }
}
