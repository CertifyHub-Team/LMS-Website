using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CertifyHub.Core.Data;
using CertifyHub.Core.DTO;

namespace CertifyHub.Core.Service
{
	public interface ISectionsService
	{
		public List<Section> GetAllSections();
		public void CreateSection(Section section);
		public void DeleteSection(int SectionId);
		public void UpdateSection(Section section);
        public Task<List<Section>> GetSectionById(int SectionId, int UserId);
		public Task<List<Section>> GetSectionByCourseId(int CourseId);
        public Task<CourseSectionDTO> GetCourseBySectionId(int SectionId);
        public Task<Section> GetSectionInfo(int SectionId);
		public  Task<List<Section>> GetInstructorSections(int instructorId);
        public Task<int> GetSectionCount();

        public Task<int> GetInstructorSectionCount(int instructorId);


    }
}
