using System;
using System.Collections.Generic;

namespace CertifyHub.Core.Data
{
    public partial class Batch
    {
        public decimal Batchesid { get; set; }
        public decimal? Programid { get; set; }
        public decimal? Numberofbatches { get; set; }

        public virtual Program? Program { get; set; }
    }
}
