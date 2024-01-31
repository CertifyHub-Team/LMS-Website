using System;
using System.Collections.Generic;

namespace CertifyHub.Core.Data
{
    public partial class Section
    {
        public Section()
        {
            Assessments = new HashSet<Assessment>();
            Attendances = new HashSet<Attendance>();
            Feedbacks = new HashSet<Feedback>();
            Notifications = new HashSet<Notification>();
            Polls = new HashSet<Poll>();
            Reviewinstructors = new HashSet<Reviewinstructor>();
            Stdsections = new HashSet<Stdsection>();
        }

        public decimal Sectionid { get; set; }
        public string? Sectionname { get; set; }
        public string? Timelecture { get; set; }
        public string? Meetinglink { get; set; }
        public decimal? Courseid { get; set; }
        public decimal? Instructorid { get; set; }
        public string? Lecturedays { get; set; }
        public string? Imagepath { get; set; }

        public virtual Course? Course { get; set; }
        public virtual User? Instructor { get; set; }
        public virtual ICollection<Assessment> Assessments { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Poll> Polls { get; set; }
        public virtual ICollection<Reviewinstructor> Reviewinstructors { get; set; }
        public virtual ICollection<Stdsection> Stdsections { get; set; }
    }
}
