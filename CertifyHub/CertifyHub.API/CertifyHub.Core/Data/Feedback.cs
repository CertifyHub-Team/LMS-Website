using System;
using System.Collections.Generic;

namespace CertifyHub.Core.Data
{
    public partial class Feedback
    {
        public decimal Feedbackid { get; set; }
        public string? Feedbackcontent { get; set; }
        public decimal? Rating { get; set; }
        public DateTime? Feedbackdate { get; set; }
        public decimal? Sectionid { get; set; }

        public virtual Section? Section { get; set; }
    }
}
