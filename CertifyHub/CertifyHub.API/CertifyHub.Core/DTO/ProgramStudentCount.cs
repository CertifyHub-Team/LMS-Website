using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Core.DTO
{
    public class ProgramStudentCount
    {

        public int ProgramID { get; set; }
        public string TrackName { get; set; }
        public int StudentCount { get; set; }
    }
}