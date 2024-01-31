using System;
using System.Collections.Generic;

namespace CertifyHub.Core.Data
{
    public partial class Pollresponse
    {
        public decimal Pollresponseid { get; set; }
        public decimal? Pollid { get; set; }
        public decimal? Userid { get; set; }
        public decimal? Optionid { get; set; }

        public virtual Polloption? Option { get; set; }
        public virtual Poll? Poll { get; set; }
        public virtual User? User { get; set; }
    }
}
