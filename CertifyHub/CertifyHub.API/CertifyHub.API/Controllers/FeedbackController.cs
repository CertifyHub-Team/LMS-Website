using CertifyHub.Core.Data;
using CertifyHub.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CertifyHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet]
        public Task<List<Feedback>> GetAllFeedback()
        {
            return _feedbackService.GetAllFeedback();
        }

        [HttpPost]
        public async void CreateFeedback(Feedback feedback)
        {
            _feedbackService.CreateFeedback(feedback);
        }


        [HttpDelete("{id}")]
        public async void DeleteFeedback(int id)
        {
            _feedbackService.DeleteFeedback(id);
        }
    }
}
