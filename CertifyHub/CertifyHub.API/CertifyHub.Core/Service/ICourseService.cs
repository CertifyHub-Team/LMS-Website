using CertifyHub.Core.Data;
using CertifyHub.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Core.Service
{
	public interface ICourseService
	{
		public List<Course> GetAllCourses();
		public List<ProgramsInformation> GetNumberOfCourses();
		public void CreateCourse(Course course);
		public void DeleteCourse(int CourseId);
		public void UpdateCourse(Course course);
		Course GetCourseById(int CourseId);
        Task<List<Course>> GetCourseByProgramId(int ProgramId);
        Task<decimal> GetCourseIdByName(string courseName);
        Task UpdateCourseImage(int userId, string newImagePath);
        Task<int> GetCourseCount();

        Task<List<CourseWithSectionsAndInstructorsDTO>> GetAllCoursesWithSectionsAndInstructors();
        Task<List<CourseWithSectionsAndInstructorsDTO>> GetCoursesByInstructorId(int instructorId);

        Task<int> GetInstructorCourseCount(int instructorId);
    }
}
