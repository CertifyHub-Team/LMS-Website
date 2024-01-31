using CertifyHub.Core.Data;
using CertifyHub.Core.Repository;
using CertifyHub.Core.Service;
using CertifyHub.Infra.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CertifyHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialService _materialService;

        public MaterialController(IMaterialService materialService)
        {
            _materialService = materialService;
        }


        [HttpPost]
        public async Task<IActionResult> AddMaterial([FromForm] Material material)
        {
            try
            {
                var file = Request.Form.Files[0];

                if (file != null && file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine("D:\\Downloads\\CertifyHub 2\\CertifyHub\\CertifyHub\\src\\assets\\uploads", fileName);

                    // Save the file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Save the file name to the database
                    material.Materialpath = fileName;

                    await _materialService.AddMaterial(material);
                }
                else
                {
                    await _materialService.AddMaterial(material);
                }

                return Ok(new { Message = "Material added successfully" });
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        public async void DeleteMaterial(int id)
        {
            _materialService.DeleteMaterial(id);
        }




        [HttpGet]
        [Route("GetAllMaterials")]
        public async Task<List<Material>> GetAllMaterials()
        {
            return await _materialService.GetAllMaterials();
        }


         [HttpGet("GetLatestMaterials/{numMaterials}")]
        public async Task<List<Material>> GetLatestMaterials(int numMaterials)
        {
            return await _materialService.GetLatestMaterials(numMaterials);

        }


        [HttpGet]
        [Route("GetMaterialByID/{id}")]
        public async Task<Material> GetMaterialByID(int id)
        {
            return await _materialService.GetMaterialByID(id);
        }



        
        [HttpGet("GetMaterialsByCourse/{courseId}")]
        public async Task<List<Material>> GetMaterialsByCourse(int courseId)
        {
            return await _materialService.GetMaterialsByCourse(courseId);

        }


        [HttpPut]
        public async Task<IActionResult> UpdateMaterial([FromForm] Material material)
        {
            try
            {
                var file = Request.Form.Files.Count > 0 ? Request.Form.Files[0] : null;

                if (file != null && file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine("D:\\Downloads\\CertifyHub 2\\CertifyHub\\CertifyHub\\src\\assets\\uploads", fileName);

                    // Save the file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Update the file name in the material object
                    material.Materialpath = fileName;
                }

                _materialService.UpdateMaterial(material);

                return Ok(new { Message = "Material updated successfully" });
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }

    }
}
