using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CertifyHub.Core.Data;
using CertifyHub.Core.Repository;
using CertifyHub.Core.Service;

namespace CertifyHub.Infra.Service
{
    public class UsersService : IUsersService
    {

        private readonly IUsersRepository  _userRepository;

        public UsersService(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<int> CreateUser(User user)
        {
            return await _userRepository.CreateUser(user);
        }

        public void UpdateUser(User user)
        {
            _userRepository.UpdateUser(user);
        }

        public void DeleteUser(int id)
        {
            _userRepository.DeleteUser(id);
        }

        public void UpdateUserPhone(int userId, string newPhoneNumber)
        {
            _userRepository.UpdateUserPhone(userId, newPhoneNumber);
        }

        public void UpdateUserImage(int userId, string newImagePath)
        {
            _userRepository.UpdateUserImage(userId, newImagePath);
        }

        public void UpdateUserIsActive(int userId, int isActive)
        {
            _userRepository.UpdateUserIsActive(userId, isActive);
        }
        public Task<List<User>> GetAllStudents()
        {
            return _userRepository.GetAllStudents();
        }
        public async Task<List<User>> GetAllInstructors()
        {

            return await _userRepository.GetAllInstructors();
        }
        public async Task<List<User>> GetStudentsCvs()
        {
            return await  _userRepository.GetStudentsCvs();
        }
    }
}
