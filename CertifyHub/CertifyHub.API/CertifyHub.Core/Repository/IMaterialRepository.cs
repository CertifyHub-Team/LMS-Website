using CertifyHub.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Core.Repository
{
    public interface IMaterialRepository
    {
        Task AddMaterial(Material material);

        Task UpdateMaterial(Material material);

        Task DeleteMaterial(int id);

        Task<Material> GetMaterialByID(int id);

        Task<List<Material>> GetAllMaterials();
        Task<List<Material>> GetMaterialsByCourse(int courseId);

        Task<List<Material>> GetLatestMaterials(int numMaterials);

    }
}