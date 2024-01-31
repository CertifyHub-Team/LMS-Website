using System;
using System.Collections.Generic;

namespace CertifyHub.Core.Data
{
    public partial class Cv
    {
        public decimal Cvid { get; set; }
        public string? Githublink { get; set; }
        public string? Experience { get; set; }
        public decimal? Gpa { get; set; }
        public string? Rating { get; set; }
        public string? Certificates { get; set; }
        public string? Education { get; set; }
        public string? Major { get; set; }
        public string? Projects { get; set; }
        public string? Interests { get; set; }
        public string? Linkedintlink { get; set; }
        public string? Skills { get; set; }
        public string? Languages { get; set; }
        public decimal? Userid { get; set; }
        public string? Qrcodeurl { get; set; }

        public virtual User? User { get; set; }
    }
}
