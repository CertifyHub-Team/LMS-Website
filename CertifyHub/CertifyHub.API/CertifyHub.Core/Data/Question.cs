using System;
using System.Collections.Generic;

namespace CertifyHub.Core.Data
{
    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
            Usersolutions = new HashSet<Usersolution>();
        }

        public decimal Questionid { get; set; }
        public string? Questiontext { get; set; }
        public decimal? Marks { get; set; }
        public decimal? Assessmentid { get; set; }

        public virtual Assessment? Assessment { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Usersolution> Usersolutions { get; set; }
    }
}
