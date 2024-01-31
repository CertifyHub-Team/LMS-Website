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
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;
        public AnswerService(IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }

        public void CreateAnswer(Answer answer)
        {
            _answerRepository.CreateAnswer(answer);
        }

        public void DeleteAnswer(int id)
        {
            _answerRepository.DeleteAnswer(id); 
        }

        public async Task<Answer> GetAnswerById(int id)
        {
            return await _answerRepository.GetAnswerById(id);
        }

        public async Task<List<Answer>> ListAnswersByQuestion(int id)
        {
            return await _answerRepository.ListAnswersByQuestion(id);
        }

        public void UpdateAnswer(Answer answer)
        {
           _answerRepository.UpdateAnswer(answer);
        }
        public async Task<List<Assessment>> ListQuestionsAndAnswers(int assessmentId)
        {
            return await _answerRepository.ListQuestionsAndAnswers(assessmentId);
        }
    }
}
