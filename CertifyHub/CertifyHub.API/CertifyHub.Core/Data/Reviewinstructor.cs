using System;
using System.Collections.Generic;

namespace CertifyHub.Core.Data
{
    public partial class Reviewinstructor
    {
        public decimal Reviewid { get; set; }
        public string? Reviewcontent { get; set; }
        public decimal? Rating { get; set; }
        public DateTime? Reviewdate { get; set; }
        public decimal? Sectionid { get; set; }

        public virtual Section? Section { get; set; }
    }
}
