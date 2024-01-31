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
    public class AssessmentService : IAssessmentService
    {
        private readonly IAssessmentRepository _assessmentRepository;
        public AssessmentService(IAssessmentRepository assessmentRepository)
        {
            _assessmentRepository = assessmentRepository;
        }
        public Task<int> CountAssessmentsBySection(int sectionId)
        {
            return _assessmentRepository.CountAssessmentsBySection(sectionId);
        }

        public Task<int> CountAssessmentsByType(string AssessmentType)
        {
            return _assessmentRepository.CountAssessmentsByType(AssessmentType);
        }

        public void CreateAssessment(Assessment assessment)
        {
            _assessmentRepository.CreateAssessment(assessment);
        }

        public void DeleteAssessment(int Assessmentid)
        {
            _assessmentRepository.DeleteAssessment(Assessmentid);
        }

        public Task<List<Assessment>> FilterAssessmentsByDate(DateTime startDate, DateTime endDate)
        {
            return _assessmentRepository.FilterAssessmentsByDate(startDate, endDate); 
        }

        public Task<List<Assessment>> GetUpcomingAssessments()
        {
            return _assessmentRepository.GetUpcomingAssessments();
        }

        public Task<List<Assessment>> ListAssessmentsByType(string AssessmentType)
        {
            return _assessmentRepository.ListAssessmentsByType(AssessmentType);
        }

        public void UpdateAssessment(Assessment assessment)
        {
            _assessmentRepository.UpdateAssessment(assessment);
        }
        public Task<List<Assessment>> GetAssessmentBysectionId(int section_id)
        {
            return _assessmentRepository.GetAssessmentBysectionId(section_id);
        }
    }
}
