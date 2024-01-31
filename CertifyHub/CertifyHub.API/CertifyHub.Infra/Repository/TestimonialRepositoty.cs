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
	public class TestimonialRepositoty : ITestimonialRepositoty
	{
		private readonly IDbContext dbContext;
		public TestimonialRepositoty(IDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public void CreateTestimonial(Testimonial testimonial)
		{
			var c = new DynamicParameters();
			c.Add("Testimonial_Text",testimonial.Testimonialtext, dbType: DbType.String, direction: ParameterDirection.Input);
			c.Add("User_Id", testimonial.Userid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			var result = dbContext.Connection.Execute("Testimonial_Package.CreateTestimonial", c, commandType: CommandType.StoredProcedure);
		}
		public void UpdateTestimonialStatus(Testimonial testimonial)
		{
			var u = new DynamicParameters();
			u.Add("Testimonial_Id", testimonial.Testimonialid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			u.Add("Testimonial_Status", testimonial.Testimonialstatus, dbType: DbType.Int32, direction: ParameterDirection.Input);
			var result = dbContext.Connection.Execute("Testimonial_Package.UpdateTestimonialStatus", u, commandType: CommandType.StoredProcedure);
		}


		public void DeleteTestimonial(int TestimonialId)
		{
			var d = new DynamicParameters();
			d.Add("Testimonial_Id", TestimonialId, dbType: DbType.Int32, direction: ParameterDirection.Input);
			var result = dbContext.Connection.Execute("Testimonial_Package.DeleteTestimonial", d, commandType: CommandType.StoredProcedure);
		}
	

		public List<Testimonial> GetAllTestimonials()
		{
			IEnumerable<Testimonial> result = dbContext.Connection.Query<Testimonial>("Testimonial_Package.GetAllTestimonials", commandType: CommandType.StoredProcedure);
			return result.ToList();
		}

		public List<Testimonial> GetTestimonialsByStatus()
		{
			var result = dbContext.Connection.Query<Testimonial>("Testimonial_Package.GetTestimonialsByStatus", commandType: CommandType.StoredProcedure);
			return result.ToList();
		}

		
	}
}
