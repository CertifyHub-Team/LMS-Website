using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Core.DTO
{
	public class LearnerReport
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string CourseName { get; set; }
		public int? SectionId { get; set; }
		public DateTime DateOfAttendance { get; set; }
		public string Status { get; set; }

		public int StudentGrade { get; set; }
	}
}
