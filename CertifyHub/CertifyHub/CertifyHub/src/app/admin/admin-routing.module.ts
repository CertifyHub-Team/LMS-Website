import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminSectionComponent } from './admin-section/admin-section.component';
import { AdminCoursesComponent } from './admin-courses/admin-courses.component';
import { AdminProgramComponent } from './admin-program/admin-program.component';
import { AdminStudentsComponent } from './admin-students/admin-students.component';
import { AdminAllstudentsComponent } from './admin-allstudents/admin-allstudents.component';
import { StudentRegisterationComponent } from './student-registeration/student-registeration.component';
import { AdminProfileComponent } from './admin-profile/admin-profile.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { CourseComponent } from './course/course.component';
import { CoursedetailsComponent } from './coursedetails/coursedetails.component';

const routes: Routes = [
  {
    path: 'AdminPrograms',
    component: AdminProgramComponent
  },
  {
    path: 'AdminCourses/:id',
    component: AdminCoursesComponent
  },
  {
    path: 'AdminSections/:id',
    component: AdminSectionComponent
  },
  {
    path: 'Students/:id',
    component: AdminStudentsComponent
  },
  {
    path: 'AllStudents',
    component: AdminAllstudentsComponent
  },
  {
    path: 'StudentsRegisteration',
    component: StudentRegisterationComponent
  },
  {
    path: 'AdminProfile',
    component: AdminProfileComponent
  },
  {
    path:'AdminDashboard',
    component:AdminDashboardComponent,
   },
   {
    path:'Course',
    component:CourseComponent ,
  },
  {
    path:'CourseDetails',
    component:CoursedetailsComponent ,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
