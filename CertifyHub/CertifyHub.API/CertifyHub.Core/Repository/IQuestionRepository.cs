using CertifyHub.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Core.Repository
{
    public interface IQuestionRepository
    {
        void CreateQuestion(Question question); 
        void UpdateQuestion(Question question);
        Task<Question> GetQuestionByID(int Questionid);
        void DeleteQuestion(int Questionid);
        Task<List<Question>> ListQuestionsByAssessment(int AssessmentId);
        void UpdateQuestionMarks(int Questionid, int newMark); 
    }
}
