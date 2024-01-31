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
    public class ReviewInstructorService : IReviewInstructorService
    {
        private readonly IReviewInstructorRepository _reviewInstructorRepository;

        public ReviewInstructorService(IReviewInstructorRepository repository)
        {
            _reviewInstructorRepository = repository;
        }

        public void CreateReview(Reviewinstructor reviewinstructor)
        {
            _reviewInstructorRepository.CreateReview(reviewinstructor);
        }

        public void DeleteReview(int id)
        {

            _reviewInstructorRepository.DeleteReview(id);
        }


        public Task<List<Reviewinstructor>> GetAllReviews()
        {

            return _reviewInstructorRepository.GetAllReviews();
        }

        public Task<List<Reviewinstructor>> GetLastThreeHighRatedReviews()
        {
            return _reviewInstructorRepository.GetLastThreeHighRatedReviews();
        }

        

    }
}
