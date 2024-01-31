using System;
using System.Collections.Generic;

namespace CertifyHub.Core.Data
{
    public partial class Assessment
    {
        public Assessment()
        {
            Grades = new HashSet<Grade>();
            Questions = new HashSet<Question>();
            Usersolutions = new HashSet<Usersolution>();
        }

        public decimal Assessmentid { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Assessmenttype { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }
        public DateTime? Starttime { get; set; }
        public DateTime? Endtime { get; set; }
        public decimal? Assessmentscore { get; set; }
        public string? Attachfile { get; set; }
        public decimal? Sectionid { get; set; }

        public virtual Section? Section { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Usersolution> Usersolutions { get; set; }
    }
}
