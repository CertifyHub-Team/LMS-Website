using CertifyHub.Core.Data;
using CertifyHub.Core.DTO;
using CertifyHub.Core.Service;
using CertifyHub.Infra.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CertifyHub.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CourseController : ControllerBase
	{
		private readonly ICourseService courseService;
		public CourseController(ICourseService courseService)
		{
			this.courseService = courseService;
		}

		[HttpGet]
		[Route("GetAllCourses")]
		public List<Course> GetAllCourses()
		{
			return courseService.GetAllCourses();
		}

		[HttpGet]
		[Route("GetNumberOfCourses")]
		public List<ProgramsInformation> GetNumberOfCourses()
		{
			return courseService.GetNumberOfCourses();
		}
		[HttpPost]
		[Route("CreateCourse")]
		public void CreateCourse(Course course)
		{
			courseService.CreateCourse(course);
		}
        [HttpPost]
        [Route("UploadImage")]
        public Course UploadImage()
        {
            var file = Request.Form.Files[0];//the file is within the request with => form data => file,if uploded more than one file, in this case only one file so its index is 0
            var fileName = $"{Guid.NewGuid().ToString()}_{file.FileName}"; //change the name to avoid duplicate
            //var fileName = Guid.NewGuid().ToString() + "_" + file.FileName";
            var fullPath = Path.Combine("D:\\Downloads\\CertifyHub 2\\CertifyHub\\CertifyHub\\src\\assets\\Images", fileName); //define the path string
            using (var stream = new FileStream(fullPath, FileMode.Create)) //create the path
            {
                file.CopyTo(stream); //append the file to  the path
            }

            Course course = new Course(); //it is better to return object than a single value
            course.Imagepath= fileName;

            return course;
        }
        [HttpPut]
		[Route("UpdateCourse")]
		public void UpdateCourse(Course course)
		{
			courseService.UpdateCourse(course);
		}

		[HttpDelete]
		[Route("DeleteCourse/{CourseId}")]
		public void DeleteCourse(int CourseId)
		{
			courseService.DeleteCourse(CourseId);
		}

		[HttpGet]
		[Route("GetCourseById/{CourseId}")]
		public Course GetCourseById(int CourseId)
		{
			return courseService.GetCourseById(CourseId);
		}

        [HttpGet]
        [Route("GetCourseByProgramId/{ProgramId}")]
        public Task<List<Course>> GetCourseByProgramId(int ProgramId)
        {
            return courseService.GetCourseByProgramId(ProgramId);
        }

        [Route("uploadCourseImage")]
		[HttpPost]
		public Course UploadCourseIMage()
		{
			var file = Request.Form.Files[0];
			var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
			var fullPath = Path.Combine("Images", fileName);
			using (var stream = new FileStream(fullPath, FileMode.Create))
			{
				file.CopyTo(stream);
			}
			Course item = new Course();
			item.Imagepath = fileName;
			return item;
		}

        [HttpGet]
        [Route("GetCourseIdByName/{courseName}")]
        public async Task<decimal> GetCourseIdByName(string courseName)
        {
            return await courseService.GetCourseIdByName(courseName);
        }
        [HttpPut]
        [Route("UpdateCourseImage/{id}/{newImagePath}")]
        public async Task UpdateCourseImage(int id, string newImagePath)
        {
           await courseService.UpdateCourseImage(id, newImagePath);
        }

        [HttpGet]
        [Route("GetCourseCount")]
        public async Task<int> GetCourseCount()
        {
            return await courseService.GetCourseCount();

        }


        [HttpGet]
        [Route("GetAllCoursesWithSectionsAndInstructors")]
        public async Task<List<CourseWithSectionsAndInstructorsDTO>> GetAllCoursesWithSectionsAndInstructors()
        {
            return await courseService.GetAllCoursesWithSectionsAndInstructors();

        }

        [HttpGet]
        [Route("GetCoursesByInstructorId/{instructorId}")]

        public async Task<List<CourseWithSectionsAndInstructorsDTO>> GetCoursesByInstructorId(int instructorId)
        {

            return await courseService.GetCoursesByInstructorId(instructorId);

        }


        [HttpGet]
        [Route("GetInstructorCourseCount/{instructorId}")]

        public async Task<int> GetInstructorCourseCount(int instructorId)
        {
            return await courseService.GetInstructorCourseCount(instructorId);

        }

    }
}
