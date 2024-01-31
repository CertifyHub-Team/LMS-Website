using System;
using System.Collections.Generic;

namespace CertifyHub.Core.Data
{
    public partial class Message
    {
        public decimal Messageid { get; set; }
        public decimal? Senderid { get; set; }
        public decimal? Receiverid { get; set; }
        public string? Messagetext { get; set; }
        public string? Filename { get; set; }
        public DateTime? Senddate { get; set; }

        public virtual User? Receiver { get; set; }
        public virtual User? Sender { get; set; }
    }
}
