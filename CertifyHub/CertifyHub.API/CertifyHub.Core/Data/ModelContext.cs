using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CertifyHub.Core.Data
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aboutu> Aboutus { get; set; } = null!;
        public virtual DbSet<Answer> Answers { get; set; } = null!;
        public virtual DbSet<Assessment> Assessments { get; set; } = null!;
        public virtual DbSet<Attendance> Attendances { get; set; } = null!;
        public virtual DbSet<Batch> Batches { get; set; } = null!;
        public virtual DbSet<Certificate> Certificates { get; set; } = null!;
        public virtual DbSet<Contact> Contacts { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Cv> Cvs { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<Grade> Grades { get; set; } = null!;
        public virtual DbSet<Login> Logins { get; set; } = null!;
        public virtual DbSet<Material> Materials { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Poll> Polls { get; set; } = null!;
        public virtual DbSet<Polloption> Polloptions { get; set; } = null!;
        public virtual DbSet<Pollresponse> Pollresponses { get; set; } = null!;
        public virtual DbSet<Program> Programs { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<Reviewinstructor> Reviewinstructors { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Section> Sections { get; set; } = null!;
        public virtual DbSet<Slider> Sliders { get; set; } = null!;
        public virtual DbSet<Stdsection> Stdsections { get; set; } = null!;
        public virtual DbSet<Testimonial> Testimonials { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Usersolution> Usersolutions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = XEPDB1)));User Id=TEST;Password=12345;Persist Security Info=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TEST")
                .UseCollation("USING_NLS_COMP");

            modelBuilder.Entity<Aboutu>(entity =>
            {
                entity.HasKey(e => e.Aboutusid)
                    .HasName("SYS_C008934");

                entity.ToTable("ABOUTUS");

                entity.Property(e => e.Aboutusid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ABOUTUSID");

                entity.Property(e => e.Aboutustext)
                    .HasColumnType("CLOB")
                    .HasColumnName("ABOUTUSTEXT");

                entity.Property(e => e.Lastupdated)
                    .HasColumnType("DATE")
                    .HasColumnName("LASTUPDATED");
            });

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("ANSWER");

                entity.Property(e => e.Answerid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ANSWERID");

                entity.Property(e => e.Answertext)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("ANSWERTEXT");

                entity.Property(e => e.Iscorrect)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("ISCORRECT");

                entity.Property(e => e.Questionid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("QUESTIONID");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.Questionid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_QUESTION");
            });

            modelBuilder.Entity<Assessment>(entity =>
            {
                entity.ToTable("ASSESSMENTS");

                entity.Property(e => e.Assessmentid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ASSESSMENTID");

                entity.Property(e => e.Assessmentscore)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ASSESSMENTSCORE");

                entity.Property(e => e.Assessmenttype)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ASSESSMENTTYPE");

                entity.Property(e => e.Attachfile)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("ATTACHFILE");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Enddate)
                    .HasColumnType("DATE")
                    .HasColumnName("ENDDATE");

                entity.Property(e => e.Endtime)
                    .HasPrecision(6)
                    .HasColumnName("ENDTIME");

                entity.Property(e => e.Sectionid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("SECTIONID");

                entity.Property(e => e.Startdate)
                    .HasColumnType("DATE")
                    .HasColumnName("STARTDATE");

                entity.Property(e => e.Starttime)
                    .HasPrecision(6)
                    .HasColumnName("STARTTIME");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TITLE");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Assessments)
                    .HasForeignKey(d => d.Sectionid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SECTION");
            });

            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.ToTable("ATTENDANCE");

                entity.Property(e => e.Attendanceid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ATTENDANCEID");

                entity.Property(e => e.Dateofattendance)
                    .HasColumnType("DATE")
                    .HasColumnName("DATEOFATTENDANCE");

                entity.Property(e => e.Sectionid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("SECTIONID");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.Sectionid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C008873");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C008874");
            });

            modelBuilder.Entity<Batch>(entity =>
            {
                entity.HasKey(e => e.Batchesid)
                    .HasName("SYS_C008856");

                entity.ToTable("BATCHES");

                entity.Property(e => e.Batchesid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("BATCHESID");

                entity.Property(e => e.Numberofbatches)
                    .HasColumnType("NUMBER")
                    .HasColumnName("NUMBEROFBATCHES");

                entity.Property(e => e.Programid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PROGRAMID");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.Batches)
                    .HasForeignKey(d => d.Programid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C008857");
            });

            modelBuilder.Entity<Certificate>(entity =>
            {
                entity.ToTable("CERTIFICATES");

                entity.Property(e => e.Certificateid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CERTIFICATEID");

                entity.Property(e => e.Certificatecloudinaryurl)
                    .HasMaxLength(400)
                    .IsUnicode(false)
                    .HasColumnName("CERTIFICATECLOUDINARYURL");

                entity.Property(e => e.Courseid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COURSEID");

                entity.Property(e => e.Expiredate)
                    .HasColumnType("DATE")
                    .HasColumnName("EXPIREDATE");

                entity.Property(e => e.Releasedate)
                    .HasColumnType("DATE")
                    .HasColumnName("RELEASEDATE");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Certificates)
                    .HasForeignKey(d => d.Courseid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C008878");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Certificates)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C008877");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("CONTACT");

                entity.Property(e => e.Contactid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CONTACTID");

                entity.Property(e => e.Contactdate)
                    .HasColumnType("DATE")
                    .HasColumnName("CONTACTDATE");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Message)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Phonenumber)
                    .HasPrecision(10)
                    .HasColumnName("PHONENUMBER");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("COURSE");

                entity.Property(e => e.Courseid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COURSEID")
                    .HasDefaultValueSql("\"TEST\".\"S_COURSE_SEQ\".\"NEXTVAL\"");

                entity.Property(e => e.Coursename)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("COURSENAME");

                entity.Property(e => e.Enddate)
                    .HasColumnType("DATE")
                    .HasColumnName("ENDDATE");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(400)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Numberofsections)
                    .HasColumnType("NUMBER")
                    .HasColumnName("NUMBEROFSECTIONS");

                entity.Property(e => e.Prerequisite)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("PREREQUISITE");

                entity.Property(e => e.Programid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PROGRAMID");

                entity.Property(e => e.Startdate)
                    .HasColumnType("DATE")
                    .HasColumnName("STARTDATE");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.Programid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C008859");
            });

            modelBuilder.Entity<Cv>(entity =>
            {
                entity.ToTable("CVS");

                entity.Property(e => e.Cvid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CVID");

                entity.Property(e => e.Certificates)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("CERTIFICATES");

                entity.Property(e => e.Education)
                    .HasMaxLength(800)
                    .IsUnicode(false)
                    .HasColumnName("EDUCATION");

                entity.Property(e => e.Experience)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("EXPERIENCE");

                entity.Property(e => e.Githublink)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("GITHUBLINK");

                entity.Property(e => e.Gpa)
                    .HasColumnType("NUMBER(6,2)")
                    .HasColumnName("GPA");

                entity.Property(e => e.Interests)
                    .HasMaxLength(800)
                    .IsUnicode(false)
                    .HasColumnName("INTERESTS");

                entity.Property(e => e.Languages)
                    .HasColumnType("CLOB")
                    .HasColumnName("LANGUAGES");

                entity.Property(e => e.Linkedintlink)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("LINKEDINTLINK");

                entity.Property(e => e.Major)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("MAJOR");

                entity.Property(e => e.Projects)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("PROJECTS");

                entity.Property(e => e.Qrcodeurl)
                    .HasMaxLength(600)
                    .IsUnicode(false)
                    .HasColumnName("QRCODEURL");

                entity.Property(e => e.Rating)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("RATING");

                entity.Property(e => e.Skills)
                    .HasColumnType("CLOB")
                    .HasColumnName("SKILLS");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Cvs)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C008881");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("FEEDBACK");

                entity.Property(e => e.Feedbackid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("FEEDBACKID");

                entity.Property(e => e.Feedbackcontent)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("FEEDBACKCONTENT");

                entity.Property(e => e.Feedbackdate)
                    .HasColumnType("DATE")
                    .HasColumnName("FEEDBACKDATE");

                entity.Property(e => e.Rating)
                    .HasColumnType("NUMBER")
                    .HasColumnName("RATING");

                entity.Property(e => e.Sectionid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("SECTIONID");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.Sectionid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C008899");
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.ToTable("GRADES");

                entity.Property(e => e.Gradeid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("GRADEID");

                entity.Property(e => e.Assessmantid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ASSESSMANTID");

                entity.Property(e => e.Notes)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("NOTES");

                entity.Property(e => e.Studentgrade)
                    .HasColumnType("NUMBER")
                    .HasColumnName("STUDENTGRADE");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.Assessmant)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.Assessmantid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C008929");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C008928");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("LOGIN");

                entity.HasIndex(e => e.Username, "SYS_C008850")
                    .IsUnique();

                entity.Property(e => e.Loginid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("LOGINID");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ROLEID");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.Roleid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C008851");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C008852");
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.ToTable("MATERIAL");

                entity.Property(e => e.Materialid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("MATERIALID");

                entity.Property(e => e.Courseid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COURSEID");

                entity.Property(e => e.Materialname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("MATERIALNAME");

                entity.Property(e => e.Materialpath)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("MATERIALPATH");

                entity.Property(e => e.Videourl)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("VIDEOURL");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Materials)
                    .HasForeignKey(d => d.Courseid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C008866");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("MESSAGES");

                entity.Property(e => e.Messageid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("MESSAGEID");

                entity.Property(e => e.Filename)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("FILENAME");

                entity.Property(e => e.Messagetext)
                    .HasColumnType("CLOB")
                    .HasColumnName("MESSAGETEXT");

                entity.Property(e => e.Receiverid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("RECEIVERID");

                entity.Property(e => e.Senddate)
                    .HasColumnType("DATE")
                    .HasColumnName("SENDDATE");

                entity.Property(e => e.Senderid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("SENDERID");

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.MessageReceivers)
                    .HasForeignKey(d => d.Receiverid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C008890");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.MessageSenders)
                    .HasForeignKey(d => d.Senderid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C008889");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("NOTIFICATIONS");

                entity.Property(e => e.Notificationid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("NOTIFICATIONID");

                entity.Property(e => e.Notificationenddate)
                    .HasColumnType("DATE")
                    .HasColumnName("NOTIFICATIONENDDATE");

                entity.Property(e => e.Notificationstartdate)
                    .HasColumnType("DATE")
                    .HasColumnName("NOTIFICATIONSTARTDATE");

                entity.Property(e => e.Notificationtext)
                    .HasColumnType("CLOB")
                    .HasColumnName("NOTIFICATIONTEXT");

                entity.Property(e => e.Sectionid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("SECTIONID");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.Sectionid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C008893");
            });

            modelBuilder.Entity<Poll>(entity =>
            {
                entity.ToTable("POLLS");

                entity.Property(e => e.Pollid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("POLLID");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Enddate)
                    .HasColumnType("DATE")
                    .HasColumnName("ENDDATE");

                entity.Property(e => e.Sectionid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("SECTIONID");

                entity.Property(e => e.Startdate)
                    .HasColumnType("DATE")
                    .HasColumnName("STARTDATE");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TITLE");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Polls)
                    .HasForeignKey(d => d.Sectionid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("F_SECTION");
            });

            modelBuilder.Entity<Polloption>(entity =>
            {
                entity.HasKey(e => e.Optionid)
                    .HasName("SYS_C008919");

                entity.ToTable("POLLOPTIONS");

                entity.Property(e => e.Optionid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("OPTIONID");

                entity.Property(e => e.Optiontext)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("OPTIONTEXT");

                entity.Property(e => e.Pollid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("POLLID");

                entity.HasOne(d => d.Poll)
                    .WithMany(p => p.Polloptions)
                    .HasForeignKey(d => d.Pollid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_POLL");
            });

            modelBuilder.Entity<Pollresponse>(entity =>
            {
                entity.ToTable("POLLRESPONSES");

                entity.Property(e => e.Pollresponseid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("POLLRESPONSEID");

                entity.Property(e => e.Optionid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("OPTIONID");

                entity.Property(e => e.Pollid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("POLLID");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.Option)
                    .WithMany(p => p.Pollresponses)
                    .HasForeignKey(d => d.Optionid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_OPTIONRESPONSE");

                entity.HasOne(d => d.Poll)
                    .WithMany(p => p.Pollresponses)
                    .HasForeignKey(d => d.Pollid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_POLLRESPONSE");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Pollresponses)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_USERPOLLRESPONSE");
            });

            modelBuilder.Entity<Program>(entity =>
            {
                entity.ToTable("PROGRAM");

                entity.Property(e => e.Programid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PROGRAMID");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Programperiod)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PROGRAMPERIOD");

                entity.Property(e => e.Trackname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("TRACKNAME");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("QUESTIONS");

                entity.Property(e => e.Questionid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("QUESTIONID");

                entity.Property(e => e.Assessmentid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ASSESSMENTID");

                entity.Property(e => e.Marks)
                    .HasColumnType("NUMBER")
                    .HasColumnName("MARKS");

                entity.Property(e => e.Questiontext)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("QUESTIONTEXT");

                entity.HasOne(d => d.Assessment)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.Assessmentid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C008905");
            });

            modelBuilder.Entity<Reviewinstructor>(entity =>
            {
                entity.HasKey(e => e.Reviewid)
                    .HasName("SYS_C008931");

                entity.ToTable("REVIEWINSTRUCTOR");

                entity.Property(e => e.Reviewid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("REVIEWID");

                entity.Property(e => e.Rating)
                    .HasColumnType("NUMBER")
                    .HasColumnName("RATING");

                entity.Property(e => e.Reviewcontent)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("REVIEWCONTENT");

                entity.Property(e => e.Reviewdate)
                    .HasColumnType("DATE")
                    .HasColumnName("REVIEWDATE");

                entity.Property(e => e.Sectionid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("SECTIONID");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Reviewinstructors)
                    .HasForeignKey(d => d.Sectionid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C008932");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("ROLE");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ROLEID");

                entity.Property(e => e.Rolename)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ROLENAME");
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.ToTable("SECTION");

                entity.Property(e => e.Sectionid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("SECTIONID");

                entity.Property(e => e.Courseid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COURSEID");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Instructorid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("INSTRUCTORID");

                entity.Property(e => e.Lecturedays)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("LECTUREDAYS");

                entity.Property(e => e.Meetinglink)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("MEETINGLINK");

                entity.Property(e => e.Sectionname)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("SECTIONNAME");

                entity.Property(e => e.Timelecture)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TIMELECTURE");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.Courseid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C008862");

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.Instructorid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C008863");
            });

            modelBuilder.Entity<Slider>(entity =>
            {
                entity.ToTable("SLIDER");

                entity.Property(e => e.Sliderid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("SLIDERID");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");
            });

            modelBuilder.Entity<Stdsection>(entity =>
            {
                entity.ToTable("STDSECTION");

                entity.Property(e => e.Stdsectionid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("STDSECTIONID");

                entity.Property(e => e.Flag)
                    .HasPrecision(1)
                    .HasColumnName("FLAG")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Sectionid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("SECTIONID");

                entity.Property(e => e.Studentid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("STUDENTID");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Stdsections)
                    .HasForeignKey(d => d.Sectionid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C008869");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Stdsections)
                    .HasForeignKey(d => d.Studentid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C008870");
            });

            modelBuilder.Entity<Testimonial>(entity =>
            {
                entity.ToTable("TESTIMONIAL");

                entity.Property(e => e.Testimonialid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("TESTIMONIALID");

                entity.Property(e => e.Testimonialdate)
                    .HasColumnType("DATE")
                    .HasColumnName("TESTIMONIALDATE");

                entity.Property(e => e.Testimonialstatus)
                    .HasPrecision(1)
                    .HasColumnName("TESTIMONIALSTATUS")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Testimonialtext)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TESTIMONIALTEXT");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Testimonials)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C008896");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USERS");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("USERID");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.Dateofbirth)
                    .HasColumnType("DATE")
                    .HasColumnName("DATEOFBIRTH");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Isactive)
                    .HasPrecision(1)
                    .HasColumnName("ISACTIVE");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LASTNAME");

                entity.Property(e => e.Phonenumber)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("PHONENUMBER");

                entity.Property(e => e.Registrationdate)
                    .HasColumnType("DATE")
                    .HasColumnName("REGISTRATIONDATE");
            });

            modelBuilder.Entity<Usersolution>(entity =>
            {
                entity.HasKey(e => e.Solutionid)
                    .HasName("SYS_C008910");

                entity.ToTable("USERSOLUTION");

                entity.Property(e => e.Solutionid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("SOLUTIONID");

                entity.Property(e => e.Answerid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ANSWERID");

                entity.Property(e => e.Assessmentid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ASSESSMENTID");

                entity.Property(e => e.Attemptdate)
                    .HasColumnType("DATE")
                    .HasColumnName("ATTEMPTDATE");

                entity.Property(e => e.Questionid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("QUESTIONID");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.Property(e => e.Usersolutiontext)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("USERSOLUTIONTEXT");

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.Usersolutions)
                    .HasForeignKey(d => d.Answerid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C008913");

                entity.HasOne(d => d.Assessment)
                    .WithMany(p => p.Usersolutions)
                    .HasForeignKey(d => d.Assessmentid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK2_ASSESSMENT");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Usersolutions)
                    .HasForeignKey(d => d.Questionid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C008912");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Usersolutions)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C008911");
            });

            modelBuilder.HasSequence("S_COURSE_SEQ");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
