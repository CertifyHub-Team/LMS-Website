using System;
using System.Collections.Generic;

namespace CertifyHub.Core.Data
{
    public partial class Certificate
    {
        public decimal Certificateid { get; set; }
        public decimal? Userid { get; set; }
        public decimal? Courseid { get; set; }
        public DateTime? Releasedate { get; set; }
        public DateTime? Expiredate { get; set; }
        public string? Certificatecloudinaryurl { get; set; }

        public virtual Course? Course { get; set; }
        public virtual User? User { get; set; }
    }
}
