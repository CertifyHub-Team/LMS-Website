using CertifyHub.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Core.Service
{
	public interface ITestimonialService
	{
		public List<Testimonial> GetAllTestimonials();
		public void CreateTestimonial(Testimonial testimonial);
		public void UpdateTestimonialStatus(Testimonial testimonial);
		public void DeleteTestimonial(int TestimonialId);
		public List<Testimonial> GetTestimonialsByStatus();
	}
}
