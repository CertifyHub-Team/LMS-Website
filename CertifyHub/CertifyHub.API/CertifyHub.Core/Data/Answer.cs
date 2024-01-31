using System;
using System.Collections.Generic;

namespace CertifyHub.Core.Data
{
    public partial class Answer
    {
        public Answer()
        {
            Usersolutions = new HashSet<Usersolution>();
        }

        public decimal Answerid { get; set; }
        public decimal? Questionid { get; set; }
        public string? Answertext { get; set; }
        public string? Iscorrect { get; set; }

        public virtual Question? Question { get; set; }
        public virtual ICollection<Usersolution> Usersolutions { get; set; }
    }
}
