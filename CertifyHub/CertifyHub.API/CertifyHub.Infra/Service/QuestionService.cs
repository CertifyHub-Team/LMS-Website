using CertifyHub.Core.Data;
using CertifyHub.Core.Repository;
using CertifyHub.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Infra.Service
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository; 

        public QuestionService (IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }
        public void CreateQuestion(Question question)
        {
            _questionRepository.CreateQuestion(question);
        }

        public void DeleteQuestion(int Questionid)
        {
            _questionRepository.DeleteQuestion(Questionid);
        }

        public Task<Question> GetQuestionByID(int Questionid)
        {
            return _questionRepository.GetQuestionByID(Questionid);
        }

        public Task<List<Question>> ListQuestionsByAssessment(int AssessmentId)
        {
            return _questionRepository.ListQuestionsByAssessment(AssessmentId);
        }

        public void UpdateQuestion(Question question)
        {
            _questionRepository.UpdateQuestion(question);
        }

        public void UpdateQuestionMarks(int Questionid, int newMark)
        {
            _questionRepository.UpdateQuestionMarks(Questionid, newMark);
        }
    }
}
