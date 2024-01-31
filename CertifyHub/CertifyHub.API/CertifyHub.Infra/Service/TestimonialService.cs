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
	public class TestimonialService : ITestimonialService
	{
		private readonly ITestimonialRepositoty testimonialRepository;

		public TestimonialService(ITestimonialRepositoty testimonialRepository)
		{
			this.testimonialRepository = testimonialRepository;
		}
		public List<Testimonial> GetAllTestimonials()
		{
			return testimonialRepository.GetAllTestimonials();
		}
		public List<Testimonial> GetTestimonialsByStatus()
		{
			return testimonialRepository.GetTestimonialsByStatus();
		}
		public void CreateTestimonial(Testimonial testimonial)
		{
			testimonialRepository.CreateTestimonial(testimonial);
		}
		public void UpdateTestimonialStatus(Testimonial testimonial)
		{
			testimonialRepository.UpdateTestimonialStatus(testimonial);
		}
		public void DeleteTestimonial(int TestimonialId)
		{
			testimonialRepository.DeleteTestimonial(TestimonialId);
		}
	}
}
