using CertifyHub.Core.Data;
using CertifyHub.Core.DTO;
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
    public class ProgramService : IProgramService
    {
        private readonly IProgramRepository _programRepository;

        public ProgramService(IProgramRepository programRepository)
        {
            _programRepository = programRepository;
        }

        public async void CreateProgram(Program program)
        {
             _programRepository.CreateProgram(program);
        }

        public async void DeleteProgram(int id)
        {
            _programRepository.DeleteProgram(id);
        }

        public async Task<List<Program>> GetAllPrograms()
        {
            return await _programRepository.GetAllPrograms();
        }

        public async Task<List<Program>> GetAdminPrograms()
        {
            return await _programRepository.GetAdminPrograms();
        }

        public async Task<Program> GetProgramByID(int id)
        {
            return await _programRepository.GetProgramByID(id);
        }

        public async Task<List<Program>> GetStudentProgram(int id)
        {
            return await _programRepository.GetStudentProgram(id);
        }

        public async void UpdateProgram(Program program)
        {
           _programRepository.UpdateProgram(program);
        }
        public void UpdateProgramImage(int userId, string newImagePath)
        {
            _programRepository.UpdateProgramImage(userId, newImagePath);
        }
        public async Task<List<ProgramStudentCount>> GetProgramStudentCounts()
        {
            return await _programRepository.GetProgramStudentCounts();

        }
    }
}
