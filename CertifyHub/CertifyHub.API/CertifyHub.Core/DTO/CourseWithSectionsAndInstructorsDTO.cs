using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Core.DTO
{
    public class CourseWithSectionsAndInstructorsDTO
    {

        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string SectionName { get; set; }
        public int InstructorId { get; set; }
        public string InstructorName { get; set; }
        public int NumberOfSections { get; set; }
    }
}
