using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Core.DTO
{
    public class StudentInfo
    {

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Imagepath { get; set; }
        public string Username { get; set; }
        public int Studentid { get; set; }
        public int Sectionid { get; set; }
        public DateTime? DateOfAttendance { get; set; }
        public string Status { get; set; }
    }
}
