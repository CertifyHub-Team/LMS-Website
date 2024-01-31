using CertifyHub.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Core.Service
{
    public interface IAssessmentService
    {
        void CreateAssessment(Assessment assessment);
        void UpdateAssessment(Assessment assessment);
        void DeleteAssessment(int Assessmentid);
        Task<List<Assessment>> ListAssessmentsByType(string AssessmentType);
        Task<int> CountAssessmentsBySection(int sectionId);
        Task<List<Assessment>> FilterAssessmentsByDate(DateTime startDate, DateTime endDate);
        Task<List<Assessment>> GetUpcomingAssessments();
        Task<int> CountAssessmentsByType(string AssessmentType);
        Task<List<Assessment>> GetAssessmentBysectionId(int section_id);
    }
}
