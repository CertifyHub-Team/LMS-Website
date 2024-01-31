using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Core.DTO
{
    public class UserCvInfo
    {
        public string FIRSTNAME { get; set; }
        public string LASTNAME { get; set; }
        public string ADDRESS { get; set; }
        public string PHONENUMBER { get; set; }
        public decimal USERID { get; set; }
        public string USERNAME { get; set; }
        public decimal? CVID { get; set; }
        public string? GitHubLink { get; set; }
        public string? Experience { get; set; }
        public decimal? GPA { get; set; }
        public string? Education { get; set; }
        public string? Major { get; set; }
        public string? Projects { get; set; }
        public string? Interests { get; set; }
        public string? LINKEDINTLINK { get; set; }
        public string? Skills { get; set; }
        public string? Languages { get; set; }
    }

}
