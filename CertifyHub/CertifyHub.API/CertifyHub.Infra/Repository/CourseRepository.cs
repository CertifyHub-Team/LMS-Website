using CertifyHub.Core.Common;
using CertifyHub.Core.Data;
using CertifyHub.Core.DTO;
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
	public class CourseRepository:ICourseRepository
	{
		private readonly IDbContext dbContext;
		public CourseRepository(IDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
        public List<Course> GetAllCourses()
        {
            IEnumerable<Course> result = dbContext.Connection.Query<Course, Program, Course>("Course_Package.GetAllCourses",
                (course, program) => {
                    course.Program = program;
                    return course;
                },
                splitOn: "ProgramId"
                , commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public List<ProgramsInformation> GetNumberOfCourses()
		{
			var result =
		   dbContext.Connection.Query<ProgramsInformation>("Course_Package.GetNumberOfCourses", commandType: CommandType.StoredProcedure);
			return result.ToList();
		}
		public void CreateCourse(Course course)
		{
			var c = new DynamicParameters();
			c.Add("Course_Name", course.Coursename, dbType: DbType.String, direction: ParameterDirection.Input);
			c.Add("Program_Id", course.Programid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			c.Add("Start_Date", course.Startdate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
			c.Add("End_Date", course.Enddate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
			c.Add("Image_Path", course.Imagepath, dbType: DbType.String, direction: ParameterDirection.Input);
			c.Add("NumberSections", course.Numberofsections, dbType: DbType.Int32, direction: ParameterDirection.Input);
			c.Add("CPrerequisite", course.Prerequisite, dbType: DbType.String, direction: ParameterDirection.Input);
			var result = dbContext.Connection.Execute("Course_Package.CREATECOURSE", c, commandType: CommandType.StoredProcedure);

		}
		public void UpdateCourse(Course course)
		{
			var u = new DynamicParameters();
			u.Add("Course_Id", course.Courseid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			u.Add("Course_Name", course.Coursename, dbType: DbType.String, direction: ParameterDirection.Input);
			u.Add("Program_Id", course.Programid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			u.Add("Start_Date", course.Startdate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
			u.Add("End_Date", course.Enddate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
			u.Add("Image_Path", course.Imagepath, dbType: DbType.String, direction: ParameterDirection.Input);
			u.Add("NumberSections", course.Numberofsections, dbType: DbType.Int32, direction: ParameterDirection.Input);
			u.Add("CPrerequisite", course.Prerequisite, dbType: DbType.String, direction: ParameterDirection.Input);
			var result = dbContext.Connection.Execute("Course_Package.UPDATECOURSE", u, commandType: CommandType.StoredProcedure);
		}

		public void DeleteCourse(int CourseId)
		{
			var d = new DynamicParameters();
			d.Add("Course_Id", CourseId, dbType: DbType.Int32, direction: ParameterDirection.Input);
			var result = dbContext.Connection.Execute("Course_Package.DeleteCourse", d, commandType: CommandType.StoredProcedure);
		}
		public Course GetCourseById(int CourseId)
		{
			var g = new DynamicParameters();
			g.Add("Course_Id", CourseId, dbType: DbType.Int32,direction: ParameterDirection.Input);
			IEnumerable<Course> result = dbContext.Connection.Query<Course>("Course_Package.GetCourseById", g, commandType: CommandType.StoredProcedure);
			return result.FirstOrDefault();
		}
        public async Task<List<Course>> GetCourseByProgramId(int ProgramId)
        {
            var g = new DynamicParameters();
            g.Add("Program_Id", ProgramId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = dbContext.Connection.Query<Course>("Course_Package.GetCourseByProgramId", g, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task<decimal> GetCourseIdByName(string courseName)
        {
            var p = new DynamicParameters();
            p.Add("Course_Name", courseName, DbType.String, ParameterDirection.Input);
            p.Add("v_Course_Id", dbType: DbType.Decimal, direction: ParameterDirection.Output);


            await dbContext.Connection.ExecuteAsync("Course_Package.GetCourseIdByName", p, commandType: CommandType.StoredProcedure);

            return p.Get<decimal>("v_Course_Id");
        }
        public async Task UpdateCourseImage(int userId, string newImagePath)
        {
            var p = new DynamicParameters();
            p.Add("COURSE_ID", userId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("NEW_IMG", newImagePath, dbType: DbType.String, direction: ParameterDirection.Input);

            await dbContext.Connection.ExecuteAsync("Course_Package.UPDATE_COURSE_IMAGE", p, commandType: CommandType.StoredProcedure);
        }
        public async Task<int> GetCourseCount()
        {
            var p = new DynamicParameters();
            p.Add("COURSE_COUNT", dbType: DbType.Int32, direction: ParameterDirection.Output);
            await dbContext.Connection.ExecuteAsync("Course_Package.GETCOURSECOUNT", p, commandType: CommandType.StoredProcedure);
            return p.Get<int>("COURSE_COUNT");
        }


        public async Task<List<CourseWithSectionsAndInstructorsDTO>> GetAllCoursesWithSectionsAndInstructors()
        {
            var result = await dbContext.Connection.QueryAsync<CourseWithSectionsAndInstructorsDTO>("Course_Package.GetAllCoursesWithSectionsAndInstructors",
                commandType: CommandType.StoredProcedure);
            return result.ToList();
        }


        public async Task<List<CourseWithSectionsAndInstructorsDTO>> GetCoursesByInstructorId(int instructorId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Instructor_Id", instructorId, DbType.Int32, ParameterDirection.Input);

            var result = dbContext.Connection.Query<CourseWithSectionsAndInstructorsDTO>("Course_Package.GetCoursesByInstructorId", parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }


        public async Task<int> GetInstructorCourseCount(int instructorId)
        {
            var p = new DynamicParameters();
            p.Add("Instructor_Id", instructorId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("COURSE_COUNT", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await dbContext.Connection.ExecuteAsync("Course_Package.GetInstructorCourseCount", p, commandType: CommandType.StoredProcedure);

            return p.Get<int>("COURSE_COUNT");
        }

    }

}


