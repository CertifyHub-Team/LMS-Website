using CertifyHub.Core.Data;
using CertifyHub.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Core.Repository
{
    public  interface IGradeRepository
    {
        void CreateGrade(Grade grade);
        Task<Grade> GetGradeById(int gradeid);
        Task<Grade> GetUserGrade(int userid , int assessmentid);
        void UpdateGrade(Grade grade);
        void DeleteGrade(int gradeid);
        Task<double> CalculateUserGrade(int userid);
        Task<string> ConvertToLetterGrade(double finalGrade);
        Task<List<StudentGradesDTO>> GetGradeByUserId(int userid);
    }
}
