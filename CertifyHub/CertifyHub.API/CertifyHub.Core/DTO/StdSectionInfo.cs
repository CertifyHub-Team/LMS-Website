using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Core.DTO
{
    public class StdSectionInfo
    {
        public string? Coursename { get; set; }
        public string? Sectionname { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Imagepath { get; set; }
        public string? Address { get; set; }
        public decimal? Userid { get; set; }
        public string? Username { get; set; }
        public int TotalGrade { get; set; }
    }
}
