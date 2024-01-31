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
	public class SectionsRepository : ISectionsRepository
	{
		private readonly IDbContext dbContext;
		public SectionsRepository(IDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		public List<Section> GetAllSections()
		{
			IEnumerable<Section> result = dbContext.Connection.Query<Section>("Section_Package.GetAllSections", commandType: CommandType.StoredProcedure);
			return result.ToList();
		}

		public async Task<List<Section>> GetSectionById(int SectionId, int UserId)
		{
			var g = new DynamicParameters();
			g.Add("Section_Id", SectionId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            g.Add("User_Id", UserId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            

            var result = await dbContext.Connection.QueryAsync<Section, Attendance, Section >(
                "Section_Package.GetSectionById",
                (section, attendance) =>
                {
                    section.Attendances ??= new List<Attendance>();
                    section.Attendances.Add(attendance);
                    return section;
                },
                g,
                splitOn: "attendanceid",
                commandType: CommandType.StoredProcedure);


            return result.ToList();
        }
        public async Task<List<Section>> GetInstructorSections(int instructorId)
        {
            var g = new DynamicParameters();
            g.Add("Instructor_Id", instructorId, dbType: DbType.Int32, direction: ParameterDirection.Input);


            var result = await dbContext.Connection.QueryAsync<Section, Course, Section>(
                "Section_Package.GetInstructorSections",
                (section, course) =>
                {
                    section.Course = course;
                    return section;
                },
                g,
                splitOn: "coursename",
                commandType: CommandType.StoredProcedure);


            return result.ToList();
        }

        public async Task<CourseSectionDTO> GetCourseBySectionId(int SectionId)
        {
            var g = new DynamicParameters();
            g.Add("Section_Id", SectionId, dbType: DbType.Int32, direction: ParameterDirection.Input);


            var result = await dbContext.Connection.QueryAsync<CourseSectionDTO>(
                "Section_Package.GetCourseBySectionId",g,
                commandType: CommandType.StoredProcedure);


            return result.FirstOrDefault();
        }


        public void DeleteSection(int SectionId)
		{
			var d = new DynamicParameters();
			d.Add("Section_Id", SectionId, dbType: DbType.Int32, direction: ParameterDirection.Input);
			var result = dbContext.Connection.Execute("Section_Package.DeleteSection", d, commandType: CommandType.StoredProcedure);
		}
		public async void CreateSection(Section section)
		{
			var c = new DynamicParameters();
			c.Add("Section_Name", section.Sectionname, dbType: DbType.String, direction: ParameterDirection.Input);
			c.Add("Time_Lecture", section.Timelecture, dbType: DbType.String, direction: ParameterDirection.Input);
			c.Add("Meeting_Link", section.Meetinglink, dbType: DbType.String, direction: ParameterDirection.Input);
			c.Add("Course_Id", section.Courseid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			c.Add("Instructor_Id", section.Instructorid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			c.Add("Lecture_Days", section.Lecturedays, dbType: DbType.String, direction: ParameterDirection.Input);
			c.Add("Image_Path", section.Imagepath, dbType: DbType.String, direction: ParameterDirection.Input);
			await dbContext.Connection.ExecuteAsync("Section_Package.CreateSection", c, commandType: CommandType.StoredProcedure);
		

		}
		public void UpdateSection(Section section)
		{
			var u = new DynamicParameters();
			u.Add("Section_Id", section.Sectionid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			u.Add("Section_Name", section.Sectionname, dbType: DbType.String, direction: ParameterDirection.Input);
			u.Add("Time_Lecture", section.Timelecture, dbType: DbType.String, direction: ParameterDirection.Input);
			u.Add("Meeting_Link", section.Meetinglink, dbType: DbType.String, direction: ParameterDirection.Input);
			u.Add("Course_Id", section.Courseid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			u.Add("Instructor_Id", section.Instructorid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			u.Add("Lecture_Days", section.Lecturedays, dbType: DbType.String, direction: ParameterDirection.Input);
			u.Add("Image_Path", section.Imagepath, dbType: DbType.String, direction: ParameterDirection.Input);
			var result = dbContext.Connection.Execute("Section_Package.UpdateSection", u, commandType: CommandType.StoredProcedure);
		}

        public async Task<List<Section>> GetSectionByCourseId(int CourseId)
        {
            var g = new DynamicParameters();
            g.Add("Course_Id", CourseId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = dbContext.Connection.Query<Section>("Section_Package.GetSectionByCourseId", g, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task<Section> GetSectionInfo(int SectionId)
        {
            var g = new DynamicParameters();
            g.Add("Section_Id", SectionId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = dbContext.Connection.Query<Section>("Section_Package.GetSectionInfo", g, commandType: CommandType.StoredProcedure);
            return result.FirstOrDefault();
        }
        public async Task<int> GetSectionCount()
        {
            var p = new DynamicParameters();
            p.Add("SECTION_COUNT", dbType: DbType.Int32, direction: ParameterDirection.Output);
            await dbContext.Connection.ExecuteAsync("Section_Package.GETSECTIONCOUNT", p, commandType: CommandType.StoredProcedure);
            return p.Get<int>("SECTION_COUNT");
        }


        public async Task<int> GetInstructorSectionCount(int instructorId)
        {
            var p = new DynamicParameters();
            p.Add("INSTRUCTOR_ID", instructorId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("SECTION_COUNT", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await dbContext.Connection.ExecuteAsync("Section_Package.GETINSTRUCTORSECTIONCOUNT", p, commandType: CommandType.StoredProcedure);

            return p.Get<int>("SECTION_COUNT");
        }
    }
}
