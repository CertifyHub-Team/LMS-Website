using CertifyHub.Core.Data;
using CertifyHub.Core.Repository;
using CertifyHub.Core.Service;
using CertifyHub.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Infra.Service
{
    public class UserSolutionService : IUserSolutionService
    {
        private readonly IUserSolutionRepository _userSolutionRepository;

        public UserSolutionService(IUserSolutionRepository userSolutionRepository)
        {
            _userSolutionRepository = userSolutionRepository;
        }

        public async Task AddUserSolution(Usersolution usersolution)
        {
           await  _userSolutionRepository.AddUserSolution(usersolution);
        }

        public Task<Usersolution> getUserSolutionByAssessment(int assessmentId, int userId)
        {
            return _userSolutionRepository.getUserSolutionByAssessment(assessmentId, userId);
        }

        public async Task<int> GetUserScore(int assessmentId, int userId)
        {
            return await _userSolutionRepository.GetUserScore(assessmentId, userId);
        }
        public async Task UpdateUserSolution(Usersolution usersolution)
        {
            await _userSolutionRepository.UpdateUserSolution(usersolution);
        }

        public async Task DeleteUserSolution(int solutionId)
        {
            await _userSolutionRepository.DeleteUserSolution(solutionId);
        }
        //public Task<List<Usersolution>> getUserSolutionByAssessment()
        //{
        //    return _userSolutionRepository.getUserSolutionByAssessment();
        //}
    }
}
