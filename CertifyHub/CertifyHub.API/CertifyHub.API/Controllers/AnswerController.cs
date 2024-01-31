using CertifyHub.Core.Data;
using CertifyHub.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CertifyHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpGet]
        [Route("GetAnswerById/{id}")]
        public async Task<Answer> GetAnswerById(int id)
        {
            return await _answerService.GetAnswerById(id);
        }

        [HttpGet]
        [Route("ListAnswersByQuestion/{id}")]
        public async Task<List<Answer>> ListAnswersByQuestion(int id)
        {
            return await _answerService.ListAnswersByQuestion(id);
        }

        [HttpPost]
        [Route("CreateAnswer")]
        public void CreateAnswer(Answer answer)
        {
            _answerService.CreateAnswer(answer);
        }

        [HttpDelete]
        [Route("DeleteAnswer/{id}")]
        public void DeleteAnswer(int id)
        {
            _answerService.DeleteAnswer(id);
        }

        [HttpPut]
        [Route("UpdateAnswer")]
        public void UpdateAnswer(Answer answer)
        {
            _answerService.UpdateAnswer(answer);
        }
        [HttpGet]
        [Route("ListQuestionsAndAnswers/{assessmentId}")]
        public Task<List<Assessment>> ListQuestionsAndAnswers(int assessmentId)
        {
            return _answerService.ListQuestionsAndAnswers(assessmentId);
        }
    }
}
