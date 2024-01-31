using CertifyHub.Core.Data;
using CertifyHub.Core.DTO;
using CertifyHub.Core.Service;
using CertifyHub.Infra.Service;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CertifyHub.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SectionController : ControllerBase
	{
		private readonly ISectionsService sectionService;
		public SectionController(ISectionsService sectionService)
		{
			this.sectionService = sectionService;
		}

		[HttpGet]
		[Route("GetAllSections")]
		public List<Section> GetAllSections()
		{
			return sectionService.GetAllSections();
		}

		[HttpPost]
		[Route("CreateSection")]
		public void CreateSection(Section section)
		{
			sectionService.CreateSection(section);
		}

		[HttpPut]
		[Route("UpdateSection")]
		public void UpdateSection(Section section)
		{
			sectionService.UpdateSection(section);
		}

		[HttpDelete]
		[Route("DeleteSection/{SectionId}")]
		public void DeleteSection(int SectionId)
		{
			sectionService.DeleteSection(SectionId);
		}

		[HttpGet]
		[Route("GetSectionById/{SectionId}/{UserId}")]
        public Task<List<Section>> GetSectionById(int SectionId, int UserId)
		{
			return sectionService.GetSectionById(SectionId, UserId);
		}

		[Route("UploadSectionImage")]
		[HttpPost]
		public Section UploadSectionIMage()
		{
			var file = Request.Form.Files[0];
			var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
			var fullPath = Path.Combine("Images", fileName);
			using (var stream = new FileStream(fullPath, FileMode.Create))
			{
				file.CopyTo(stream);
			}
			Section item = new Section();
			item.Imagepath = fileName;
			return item;
		}

        [HttpGet]
        [Route("GetSectionByCourseId/{CourseId}")]
        public Task<List<Section>> GetSectionByCourseId(int CourseId)
		{
			return sectionService.GetSectionByCourseId(CourseId);
		}

        [HttpGet]
        [Route("GetCourseBySectionId/{SectionId}")]
        public Task<CourseSectionDTO> GetCourseBySectionId(int SectionId)
		{
			return sectionService.GetCourseBySectionId(SectionId);
		}

        [HttpGet]
        [Route("GetSectionInfo/{SectionId}")]
        public Task<Section> GetSectionInfo(int SectionId)
		{
			return sectionService.GetSectionInfo(SectionId);
		}

        [HttpGet]
        [Route("GetInstructorSections/{instructorId}")]
        public Task<List<Section>> GetInstructorSections(int instructorId)
		{
			return sectionService.GetInstructorSections(instructorId);
		}
        [HttpGet]
        [Route("GetSectionCount")]
        public async Task<int> GetSectionCount()
        {
            return await sectionService.GetSectionCount();
        }

        [HttpGet]
        [Route("GetInstructorSectionCount/{instructorId}")]
        public async Task<int> GetInstructorSectionCount(int instructorId)
        {
            return await sectionService.GetInstructorSectionCount(instructorId);

        }

    }
}
