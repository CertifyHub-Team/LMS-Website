using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Core.DTO
{
    public class StudentsSolutionBySectionDTO
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public decimal? Userid { get; set; }
        public string? Title { get; set; }
        public string? Usersolutiontext { get; set; }
        public DateTime? Attemptdate { get; set; }
        public decimal? Studentgrade { get; set; }

    }
}
