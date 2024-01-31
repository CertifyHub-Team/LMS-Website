using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CertifyHub.Core.Data;

namespace CertifyHub.Core.Repository
{
    public interface IFeedbackRepository
    {

        void CreateFeedback(Feedback feedback );
        void DeleteFeedback(int id);
        Task<List<Feedback>> GetAllFeedback();




    }
}
