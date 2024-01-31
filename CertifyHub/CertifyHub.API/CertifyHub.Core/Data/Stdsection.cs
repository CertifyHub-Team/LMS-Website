using System;
using System.Collections.Generic;

namespace CertifyHub.Core.Data
{
    public partial class Stdsection
    {
        public decimal Stdsectionid { get; set; }
        public decimal? Sectionid { get; set; }
        public decimal? Studentid { get; set; }
        public bool? Flag { get; set; }

        public virtual Section? Section { get; set; }
        public virtual User? Student { get; set; }
    }
}
