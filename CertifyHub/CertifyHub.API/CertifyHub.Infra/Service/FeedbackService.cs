using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CertifyHub.Core.Data;
using CertifyHub.Core.Repository;
using CertifyHub.Core.Service;
using CertifyHub.Infra.Repository;

namespace CertifyHub.Infra.Service
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }
        public void CreateFeedback(Feedback feedback)
        {
            _feedbackRepository.CreateFeedback(feedback);
        }

        public void DeleteFeedback(int id)
        {
            _feedbackRepository.DeleteFeedback(id);

        }

        public async Task<List<Feedback>> GetAllFeedback()
        {
            return await _feedbackRepository.GetAllFeedback();  
        }

       
    }
}
