using CertifyHub.Core.Data;
using CertifyHub.Core.DTO;
using CertifyHub.Core.Repository;
using CertifyHub.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Infra.Service
{
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository _gradeRepository; 

        public GradeService(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }
        public Task<double> CalculateUserGrade(int userid)
        {
            return _gradeRepository.CalculateUserGrade(userid);
        }

        public Task<string> ConvertToLetterGrade(double finalGrade)
        {
            return _gradeRepository.ConvertToLetterGrade(finalGrade);
        }

        public void CreateGrade(Grade grade)
        {
            _gradeRepository.CreateGrade(grade);
        }

        public void DeleteGrade(int gradeid)
        {
            _gradeRepository.DeleteGrade(gradeid);
        }

        public Task<Grade> GetGradeById(int gradeid)
        {
            return _gradeRepository.GetGradeById(gradeid);
        }

        public Task<Grade> GetUserGrade(int userid , int assessmentid)
        {
           return _gradeRepository.GetUserGrade(userid , assessmentid);
        }

        public void UpdateGrade(Grade grade)
        {
            _gradeRepository.UpdateGrade(grade);
        }
        public Task<List<StudentGradesDTO>> GetGradeByUserId(int userid)
        {
            return _gradeRepository.GetGradeByUserId(userid);
        }
    }
}
