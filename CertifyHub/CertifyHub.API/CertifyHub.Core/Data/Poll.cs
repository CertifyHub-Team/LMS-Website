using System;
using System.Collections.Generic;

namespace CertifyHub.Core.Data
{
    public partial class Poll
    {
        public Poll()
        {
            Polloptions = new HashSet<Polloption>();
            Pollresponses = new HashSet<Pollresponse>();
        }

        public decimal Pollid { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }
        public decimal? Sectionid { get; set; }

        public virtual Section? Section { get; set; }
        public virtual ICollection<Polloption> Polloptions { get; set; }
        public virtual ICollection<Pollresponse> Pollresponses { get; set; }
    }
}
