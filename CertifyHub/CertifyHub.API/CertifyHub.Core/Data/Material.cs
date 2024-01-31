using System;
using System.Collections.Generic;

namespace CertifyHub.Core.Data
{
    public partial class Material
    {
        public decimal Materialid { get; set; }
        public string? Materialname { get; set; }
        public string? Materialpath { get; set; }
        public decimal? Courseid { get; set; }
        public string? Videourl { get; set; }

        public virtual Course? Course { get; set; }
    }
}
