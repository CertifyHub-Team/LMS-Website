using CertifyHub.Core.Data;
using CertifyHub.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Core.Repository
{
	public interface ITestimonialRepositoty
	{
		public List<Testimonial> GetAllTestimonials();
		public void CreateTestimonial(Testimonial testimonial);
		public void UpdateTestimonialStatus(Testimonial testimonial);
		public void DeleteTestimonial(int TestimonialId);
		public List<Testimonial> GetTestimonialsByStatus();
	}
}
