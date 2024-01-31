using System;
using System.Collections.Generic;

namespace CertifyHub.Core.Data
{
    public partial class Program
    {
        public Program()
        {
            Batches = new HashSet<Batch>();
            Courses = new HashSet<Course>();
        }

        public decimal Programid { get; set; }
        public string? Trackname { get; set; }
        public string? Imagepath { get; set; }
        public string? Description { get; set; }
        public string? Programperiod { get; set; }

        public virtual ICollection<Batch> Batches { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
