using System;
using System.Collections.Generic;

namespace CertifyHub.Core.Data
{
    public partial class Attendance
    {
        public decimal Attendanceid { get; set; }
        public decimal? Sectionid { get; set; }
        public DateTime? Dateofattendance { get; set; }
        public decimal? Userid { get; set; }
        public string? Status { get; set; }

        public virtual Section? Section { get; set; }
        public virtual User? User { get; set; }
    }
}
