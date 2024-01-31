using System;
using System.Collections.Generic;

namespace CertifyHub.Core.Data
{
    public partial class Notification
    {
        public decimal Notificationid { get; set; }
        public decimal? Sectionid { get; set; }
        public string? Notificationtext { get; set; }
        public DateTime? Notificationenddate { get; set; }
        public DateTime? Notificationstartdate { get; set; }

        public virtual Section? Section { get; set; }
    }
}
