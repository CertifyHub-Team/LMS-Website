using CertifyHub.Core.Repository;
using CertifyHub.Core.Service;
using CertifyHub.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CertifyHub.Core.Data;
using CertifyHub.Core.DTO;




namespace CertifyHub.Infra.Service
{
	public class SectionsService : ISectionsService
	{
		private readonly ISectionsRepository sectionRepository;

		public SectionsService(ISectionsRepository sectionRepository)
		{
			this.sectionRepository = sectionRepository;
		}
		public List<Section> GetAllSections()
		{
			return sectionRepository.GetAllSections();
		}
		public void CreateSection(Section section)
		{
			sectionRepository.CreateSection(section);
		}
		public void UpdateSection(Section section)
		{
			sectionRepository.UpdateSection(section);
		}
		public void DeleteSection(int SectionId)
		{
			sectionRepository.DeleteSection(SectionId);
		}
        public Task<List<Section>> GetSectionById(int SectionId, int UserId)
		{
			return sectionRepository.GetSectionById(SectionId, UserId);
		}
        public Task<List<Section>> GetSectionByCourseId(int CourseId)
		{
			return sectionRepository.GetSectionByCourseId(CourseId);
		}

        public async Task<CourseSectionDTO> GetCourseBySectionId(int SectionId)
		{
			return await sectionRepository.GetCourseBySectionId(SectionId);
		}

        public async Task<Section> GetSectionInfo(int SectionId)
        {
			return await sectionRepository.GetSectionInfo(SectionId);
        }
        public async Task<List<Section>> GetInstructorSections(int instructorId)
		{
			return await sectionRepository.GetInstructorSections(instructorId);
		}
        public async Task<int> GetSectionCount()
        {
            return await sectionRepository.GetSectionCount();
        }


        public async Task<int> GetInstructorSectionCount(int instructorId)
        {
            return await sectionRepository.GetInstructorSectionCount(instructorId);

        }
    }
}
