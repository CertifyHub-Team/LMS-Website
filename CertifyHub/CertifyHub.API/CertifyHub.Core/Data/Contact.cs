using System;
using System.Collections.Generic;

namespace CertifyHub.Core.Data
{
    public partial class Contact
    {
        public decimal Contactid { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int? Phonenumber { get; set; }
        public string? Message { get; set; }
        public DateTime? Contactdate { get; set; }
    }
}
