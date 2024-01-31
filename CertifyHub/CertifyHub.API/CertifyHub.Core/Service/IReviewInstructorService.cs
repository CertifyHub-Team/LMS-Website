﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CertifyHub.Core.Data;

namespace CertifyHub.Core.Service
{
    
       public interface IReviewInstructorService
    {
        void CreateReview(Reviewinstructor reviewinstructor);
        void DeleteReview(int id);
        Task<List<Reviewinstructor>> GetAllReviews();
        Task<List<Reviewinstructor>> GetLastThreeHighRatedReviews();
    }
}
