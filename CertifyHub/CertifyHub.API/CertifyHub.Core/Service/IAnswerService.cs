using CertifyHub.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Core.Service
{
    public interface IAnswerService
    {
        Task<Answer> GetAnswerById(int id);
        Task<List<Answer>> ListAnswersByQuestion(int id);
        void CreateAnswer(Answer answer);
        void DeleteAnswer(int id);
        void UpdateAnswer(Answer answer);
        Task<List<Assessment>> ListQuestionsAndAnswers(int assessmentId);
    }
}
