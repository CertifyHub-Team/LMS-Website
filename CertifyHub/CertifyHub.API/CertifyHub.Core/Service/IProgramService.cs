using CertifyHub.Core.Data;
using CertifyHub.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Core.Service
{
    public interface IProgramService
    {
        void CreateProgram(Program program);
        void UpdateProgram(Program program);
        void DeleteProgram(int id);
        Task<Program> GetProgramByID(int id);
        Task<List<Program>> GetAllPrograms();
        Task<List<Program>> GetAdminPrograms();
        Task<List<Program>> GetStudentProgram(int id);
        void UpdateProgramImage(int userId, string newImagePath);
        Task<List<ProgramStudentCount>> GetProgramStudentCounts();
    }
}
