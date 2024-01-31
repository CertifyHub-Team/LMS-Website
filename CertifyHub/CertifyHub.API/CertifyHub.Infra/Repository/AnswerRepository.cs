using CertifyHub.Core.Common;
using CertifyHub.Core.Data;
using CertifyHub.Core.Repository;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace CertifyHub.Infra.Repository
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly IDbContext _dbContext;

        public AnswerRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateAnswer(Answer answer)
        {
            var p = new DynamicParameters();
            p.Add("QUESTION_ID", answer.Questionid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("ANSWER_TEXT", answer.Answertext, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("IS_CORRECT", answer.Iscorrect, dbType: DbType.String, direction: ParameterDirection.Input);
            _dbContext.Connection.ExecuteAsync("ANSWER_PACKAGE.CREATE_ANSWER", p, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void DeleteAnswer(int id)
        {
            var p = new DynamicParameters();
            p.Add("ANSWER_ID", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            _dbContext.Connection.ExecuteAsync("ANSWER_PACKAGE.DELETE_ANSWER", p, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<Answer> GetAnswerById(int id)
        {
            var p = new DynamicParameters();
            p.Add("ANSWER_ID", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryAsync<Answer>("ANSWER_PACKAGE.GET_ANSWER_BY_ID", p, commandType: System.Data.CommandType.StoredProcedure);
            return result.FirstOrDefault();
        }

        public async Task<List<Answer>> ListAnswersByQuestion(int id)
        {
            var p = new DynamicParameters();
            p.Add("QUESTION_ID", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryAsync<Answer>("ANSWER_PACKAGE.LIST_ANSWERS_BY_QUESTION", p, commandType: System.Data.CommandType.StoredProcedure);
            return result.ToList();
        }
        public async Task<List<Assessment>> ListQuestionsAndAnswers(int assessmentId)
        {
            var p = new DynamicParameters();
            p.Add("ASSESSMENT_ID", assessmentId, dbType: DbType.Int32, direction: ParameterDirection.Input);

            var questionDictionary = new Dictionary<int, Question>();

            var result = await _dbContext.Connection.QueryAsync<Assessment, Question, Answer, Assessment>(
                "ANSWER_PACKAGE.LIST_QUESTION_AND_ANSWERS",
                (assessment, question, answer) =>
                {
                    assessment ??= new Assessment();

                    if (assessment.Questions == null)
                        assessment.Questions = new List<Question>();

                    // Check if the question already exists in the dictionary
                    if (!questionDictionary.TryGetValue((int)question.Questionid, out var existingQuestion))
                    {
                        // If the question doesn't exist, add it to the dictionary
                        question.Answers = new List<Answer>();
                        question.Answers.Add(answer);
                        questionDictionary.Add((int)question.Questionid, question);

                        assessment.Questions.Add(question);
                    }
                    else
                    {
                        // If the question already exists, add the answer to its existing list
                        existingQuestion.Answers.Add(answer);
                    }

                    return assessment;
                },
                p,
                splitOn: "ASSESSMENTID,QUESTIONID,ANSWERID", // Adjust to match the actual column names
                commandType: System.Data.CommandType.StoredProcedure
            );

            // Filter out assessments without questions
            var filteredResult = result.Where(assessment => assessment.Questions.Count > 0).ToList();

            return filteredResult;
        }




        public void UpdateAnswer(Answer answer)
        {
            var p = new DynamicParameters();
            p.Add("ANSWER_ID", answer.Answerid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("QUESTION_ID", answer.Questionid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("ANSWER_TEXT", answer.Answertext, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("IS_CORRECT", answer.Iscorrect, dbType: DbType.String, direction: ParameterDirection.Input);
            _dbContext.Connection.ExecuteAsync("ANSWER_PACKAGE.UPDATE_ANSWER", p, commandType: System.Data.CommandType.StoredProcedure);

        }
    }
}
