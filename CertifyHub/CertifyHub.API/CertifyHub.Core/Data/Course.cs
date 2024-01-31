using System;
using System.Collections.Generic;

namespace CertifyHub.Core.Data
{
    public partial class Course
    {
        public Course()
        {
            Certificates = new HashSet<Certificate>();
            Materials = new HashSet<Material>();
            Sections = new HashSet<Section>();
        }

        public decimal Courseid { get; set; }
        public string? Coursename { get; set; }
        public decimal? Programid { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }
        public string? Imagepath { get; set; }
        public decimal? Numberofsections { get; set; }
        public string? Prerequisite { get; set; }

        public virtual Program? Program { get; set; }
        public virtual ICollection<Certificate> Certificates { get; set; }
        public virtual ICollection<Material> Materials { get; set; }
        public virtual ICollection<Section> Sections { get; set; }
    }
}
