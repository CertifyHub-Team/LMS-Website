using System;
using System.Collections.Generic;

namespace CertifyHub.Core.Data
{
    public partial class Usersolution
    {
        public decimal Solutionid { get; set; }
        public decimal? Userid { get; set; }
        public decimal? Assessmentid { get; set; }
        public string? Usersolutiontext { get; set; }
        public DateTime? Attemptdate { get; set; }
        public decimal? Questionid { get; set; }
        public decimal? Answerid { get; set; }

        public virtual Answer? Answer { get; set; }
        public virtual Assessment? Assessment { get; set; }
        public virtual Question? Question { get; set; }
        public virtual User? User { get; set; }
    }
}
