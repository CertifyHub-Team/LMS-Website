using System;
using System.Collections.Generic;

namespace CertifyHub.Core.Data
{
    public partial class Testimonial
    {
        public decimal Testimonialid { get; set; }
        public string? Testimonialtext { get; set; }
        public DateTime? Testimonialdate { get; set; }
        public bool? Testimonialstatus { get; set; }
        public decimal? Userid { get; set; }

        public virtual User? User { get; set; }
    }
}
