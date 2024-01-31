using System;
using System.Collections.Generic;

namespace CertifyHub.Core.Data
{
    public partial class Grade
    {
        public decimal Gradeid { get; set; }
        public decimal? Studentgrade { get; set; }
        public decimal? Userid { get; set; }
        public decimal? Assessmantid { get; set; }
        public string? Notes { get; set; }

        public virtual Assessment? Assessmant { get; set; }
        public virtual User? User { get; set; }
    }
}
