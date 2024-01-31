using CertifyHub.Core.Data;
using CertifyHub.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CertifyHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewInstructorController : ControllerBase
    {
        private readonly IReviewInstructorService _reviewInstructorService;

        public ReviewInstructorController(IReviewInstructorService reviewInstructorService)
        {
            _reviewInstructorService = reviewInstructorService;
        }

        [HttpPost]
        public void CreateReview(Reviewinstructor reviewinstructor)
        {
            _reviewInstructorService.CreateReview(reviewinstructor);
        }

        [HttpDelete("{id}")]

        public void DeleteReview(int id)
        {
            _reviewInstructorService.DeleteReview(id);
        }

        [HttpGet]
        [Route("Getreview")]
        public Task<List<Reviewinstructor>> GetAllReviews()
        {
            return _reviewInstructorService.GetAllReviews();
        }

        [HttpGet]
        [Route("LastThreeHighRatedReviews")]

        public Task<List<Reviewinstructor>> GetLastThreeHighRatedReviews()
        {
            return _reviewInstructorService.GetLastThreeHighRatedReviews();
        }

       

        }
}
