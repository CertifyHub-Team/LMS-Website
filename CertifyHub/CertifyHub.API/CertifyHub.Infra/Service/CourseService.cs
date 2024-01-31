using CertifyHub.Core.Data;
using CertifyHub.Core.DTO;
using CertifyHub.Core.Repository;
using CertifyHub.Core.Service;
using CertifyHub.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Infra.Service
{
	public class CourseService : ICourseService
	{
		private readonly ICourseRepository courseRepository;

		public CourseService(ICourseRepository courseRepository)
		{
			this.courseRepository = courseRepository;
		}
		public List<Course> GetAllCourses()
		{
			return courseRepository.GetAllCourses();
		}
		public void CreateCourse(Course course)
		{
			courseRepository.CreateCourse(course);
		}
		public void UpdateCourse(Course course)
		{
			courseRepository.UpdateCourse(course);
		}
		public void DeleteCourse(int CourseId)
		{
			courseRepository.DeleteCourse(CourseId);
		}
		public Course GetCourseById(int CourseId)
		{
			return courseRepository.GetCourseById(CourseId);
		}

		public List<ProgramsInformation> GetNumberOfCourses()
		{
			return courseRepository.GetNumberOfCourses();
		}

        public Task<List<Course>> GetCourseByProgramId(int ProgramId)
        {
			return courseRepository.GetCourseByProgramId(ProgramId);
        }
        public async Task<decimal> GetCourseIdByName(string courseName)
        {
            return await courseRepository.GetCourseIdByName(courseName);
        }
        public async Task UpdateCourseImage(int userId, string newImagePath)
		{
			  await courseRepository.UpdateCourseImage(userId, newImagePath);
		}
        public async Task<int> GetCourseCount()
        {
            return await courseRepository.GetCourseCount();

        }
        public async Task<List<CourseWithSectionsAndInstructorsDTO>> GetAllCoursesWithSectionsAndInstructors()
        {
            return await courseRepository.GetAllCoursesWithSectionsAndInstructors();

        }
        public async Task<List<CourseWithSectionsAndInstructorsDTO>> GetCoursesByInstructorId(int instructorId)
        {
            return await courseRepository.GetCoursesByInstructorId(instructorId);
        }
        public async Task<int> GetInstructorCourseCount(int instructorId)
        {
            return await courseRepository.GetInstructorCourseCount(instructorId);

        }
    }
}
