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
	public class TestimonialController : ControllerBase
	{
		private readonly ITestimonialService testimonialService;
		public TestimonialController(ITestimonialService testimonialService)
		{
			this.testimonialService = testimonialService;
		}

		[HttpGet]
		[Route("GetAllTestimonials")]
		public List<Testimonial> GetAllTestimonials()
		{
			return testimonialService.GetAllTestimonials();
		}
		[HttpGet]
		[Route("GetTestimonialsByStatus")]
		public List<Testimonial> GetTestimonialsByStatus()
		{
			return testimonialService.GetTestimonialsByStatus();
		}

		[HttpPost]
		[Route("CreateTestimonial")]
		public void CreateTestimonial(Testimonial testimonial)
		{
			testimonialService.CreateTestimonial(testimonial);
		}

		[HttpPut]
		[Route("UpdateTestimonialStatus")]
		public void UpdateTestimonialStatus(Testimonial testimonial)
		{
			testimonialService.UpdateTestimonialStatus(testimonial);
		}


		[HttpDelete]
		[Route("DeleteTestimonial/{TestimonialId}")]
		public void DeleteTestimonial(int TestimonialId)
		{
			testimonialService.DeleteTestimonial(TestimonialId);
		}

	}
}
