using CertifyHub.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Core.Service
{
    public interface IUserSolutionService
    {
        Task AddUserSolution(Usersolution usersolution);

        Task<Usersolution> getUserSolutionByAssessment(int assessmentId, int userId);
        Task<int> GetUserScore(int assessmentId, int userId);
        Task UpdateUserSolution(Usersolution usersolution);
        Task DeleteUserSolution(int solutionId);
    }
}
