using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Core.DTO
{
    public class CertificateDto
    {
        public int USERID { get; set; }
        public string FIRSTNAME { get; set; }
        public string LASTNAME { get; set; }
        public string CourseName { get; set; }
        public int Totalgrade { get; set; }
        public string QRCODEURL { get; set; }
        public int flag { get; set; }
    }
}
