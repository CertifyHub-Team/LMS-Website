import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StudentDashboardComponent } from './student-dashboard/student-dashboard.component';
import { StudentProfileComponent } from './student-profile/student-profile.component';
import { StudentCvComponent } from './student-cv/student-cv.component';
import { StudentProgramComponent } from './student-program/student-program.component';
import { StudentSectionComponent } from './student-section/student-section.component';
import { CertificateComponent } from './certificate/certificate.component';
import { StudentExamComponent } from './student-exam/student-exam.component';

const routes: Routes = [
  {
    path: 'StudentDashboard',
    component: StudentDashboardComponent
  },
  {
    path: 'StudentProfile',
    component: StudentProfileComponent
  },
  {
    path: 'StudentCv/:id',
    component: StudentCvComponent
  },
  {
    path: 'MyCourses',
    component: StudentProgramComponent
  },
  { path: 'CourseSection/:id/:courseId',
    component:  StudentSectionComponent
  },
  {
    path:'Certificate',
   component: CertificateComponent
  },
  {
    path:'Exam/:assessmentid/:examTitle',
    component:StudentExamComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LearnerRoutingModule { }
