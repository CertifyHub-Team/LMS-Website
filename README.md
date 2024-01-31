# **CertifyHub - Learning Management System (LMS) 🚀**

## **About**

CertifyHub is a comprehensive Learning Management System (LMS) designed to facilitate the delivery, management, and tracking of training programs. It serves various educational institutions, businesses, and organizations, offering a robust platform with user authentication, course management, assessment tools, attendance tracking, progress reporting, and certificate generation functionalities.

## **Key Features 🌟**

1. **User Authentication 🔐**
   - Three user roles: Instructor, Learner, Admin
   - Admin manages user roles and permissions.

2. **Courses Management 📚**
   - Admin manages educational periods, plans, programs, courses, and sections.
   - Defines the sequence for educational components.

3. **Assessment and Evaluation 📝**
   - Instructors create assessments and exams.
   - Learners access and submit solutions.

4. **Attendance Management 📅**
   - Instructors manage learner attendance and absence through the system.

5. **Progress Tracking and Reporting 📊**
   - Learners receive reports on attendance, absence, and grades.
   - Instructors receive reports for learners in each section.
   - Admin receives comprehensive reports on learner performance.

6. **Generate Certificates 🎓**
   - Automated certificate generation upon course completion.
   - Certificates contain QR codes displaying learner CV information.

## **User Roles 🧑‍💼**

1. **Guest User 👤:**
   - Browse learner's CV information.

2. **Learner User 📚:**
   - Download lecture content.
   - Submit assignments and exams.
   - Access lecture links.
   - Download reports and certificates.

3. **Instructor User 👩‍🏫:**
   - Upload course materials.
   - CRUD assignments and exams.
   - Take attendance.
   - Download reports.

4. **Admin User 👨‍💼:**
   - CRUD Users and determine user roles.
   - CRUD learning period, program, plan, course, and sections.
   - Add learners to sections.
   - Generate certificates.
   - Download certificates and reports.

5. **System 🤖:**
   - Automated certificate generation.
   - QR code generation on each certificate.
   - Page link generation for each learner CV.
   - Email notifications for registration and certificates.

## **System Architecture 🏗️**

The CertifyHub system comprises three main components:

1. **Frontend 💻**
   - User interface utilizing Angular for an intuitive learning experience.

2. **Backend ⚙️**
   - Central control system handling business logic.
   - Technologies: C#, Web API, external service APIs.

3. **Database 🗄️**
   - Oracle Database for structured data storage.
   - Stores user data, course details, attendance records, and certificates.
