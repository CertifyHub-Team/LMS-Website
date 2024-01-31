using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Core.DTO
{
    public class AttendanceWithSection
    {     
        public DateTime DateOfAttendance { get; set; }
        public int UserID { get; set; }
        public string Status { get; set; }
        public string SectionName { get; set; }
    }
}
