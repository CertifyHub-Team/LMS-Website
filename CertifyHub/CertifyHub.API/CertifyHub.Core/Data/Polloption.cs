using System;
using System.Collections.Generic;

namespace CertifyHub.Core.Data
{
    public partial class Polloption
    {
        public Polloption()
        {
            Pollresponses = new HashSet<Pollresponse>();
        }

        public decimal Optionid { get; set; }
        public decimal? Pollid { get; set; }
        public string? Optiontext { get; set; }

        public virtual Poll? Poll { get; set; }
        public virtual ICollection<Pollresponse> Pollresponses { get; set; }
    }
}
