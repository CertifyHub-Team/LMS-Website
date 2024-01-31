using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Core.DTO
{
    public class TopStudentAttendances
    {
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int AttendanceCount { get; set; }
    }
}
