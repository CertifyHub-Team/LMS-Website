using CertifyHub.Core.Data;
using CertifyHub.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CertifyHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController (IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpPost]
        public void CreateQuestion(Question question)
        {
            _questionService.CreateQuestion(question);
        }

        [HttpDelete("{Questionid}")]
    
        public void DeleteQuestion(int Questionid)
        {
            _questionService.DeleteQuestion(Questionid);
        }

        [HttpGet]
        [Route("GetQuestionByID/{Questionid}")]
        public Task<Question> GetQuestionByID(int Questionid)
        {
            return _questionService.GetQuestionByID(Questionid);
        }

        [HttpGet]
        [Route("ListQuestionsByAssessment/{AssessmentId}")]
        public Task<List<Question>> ListQuestionsByAssessment(int AssessmentId)
        {
            return _questionService.ListQuestionsByAssessment(AssessmentId);
        }

        [HttpPut]
        [Route("UpdateQuestion")]
        public void UpdateQuestion(Question question)
        {
            _questionService.UpdateQuestion(question);
        }

        [HttpPut]
        [Route("UpdateQuestionMarks")]
        public void UpdateQuestionMarks(int Questionid, int newMark)
        {
            _questionService.UpdateQuestionMarks(Questionid, newMark);
        }
    }
}
