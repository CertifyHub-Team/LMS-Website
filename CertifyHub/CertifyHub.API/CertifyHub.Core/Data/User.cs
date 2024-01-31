using System;
using System.Collections.Generic;

namespace CertifyHub.Core.Data
{
    public partial class User
    {
        public User()
        {
            Attendances = new HashSet<Attendance>();
            Certificates = new HashSet<Certificate>();
            Cvs = new HashSet<Cv>();
            Grades = new HashSet<Grade>();
            Logins = new HashSet<Login>();
            MessageReceivers = new HashSet<Message>();
            MessageSenders = new HashSet<Message>();
            Pollresponses = new HashSet<Pollresponse>();
            Sections = new HashSet<Section>();
            Stdsections = new HashSet<Stdsection>();
            Testimonials = new HashSet<Testimonial>();
            Usersolutions = new HashSet<Usersolution>();
        }

        public decimal Userid { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public DateTime? Dateofbirth { get; set; }
        public string? Imagepath { get; set; }
        public string? Address { get; set; }
        public string? Phonenumber { get; set; }
        public DateTime? Registrationdate { get; set; }
        public bool? Isactive { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<Certificate> Certificates { get; set; }
        public virtual ICollection<Cv> Cvs { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
        public virtual ICollection<Login> Logins { get; set; }
        public virtual ICollection<Message> MessageReceivers { get; set; }
        public virtual ICollection<Message> MessageSenders { get; set; }
        public virtual ICollection<Pollresponse> Pollresponses { get; set; }
        public virtual ICollection<Section> Sections { get; set; }
        public virtual ICollection<Stdsection> Stdsections { get; set; }
        public virtual ICollection<Testimonial> Testimonials { get; set; }
        public virtual ICollection<Usersolution> Usersolutions { get; set; }
    }
}
