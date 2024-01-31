import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { AdminSectionComponent } from './admin-section/admin-section.component';
import { SharedModule } from '../shared/shared.module';
import { AdminService } from '../admin.service';
import { HttpClientModule } from '@angular/common/http';
import { AdminCoursesComponent } from './admin-courses/admin-courses.component';
import { AdminProgramComponent } from './admin-program/admin-program.component';
import { AdminStudentsComponent } from './admin-students/admin-students.component';
import { AdminAllstudentsComponent } from './admin-allstudents/admin-allstudents.component';
import { StudentRegisterationComponent } from './student-registeration/student-registeration.component';
import { AdminProfileComponent } from './admin-profile/admin-profile.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { CourseComponent } from './course/course.component';
import { CoursedetailsComponent } from './coursedetails/coursedetails.component';


@NgModule({
  declarations: [
    AdminSectionComponent,
    AdminCoursesComponent,
    AdminProgramComponent,
    AdminStudentsComponent,
    AdminAllstudentsComponent,
    StudentRegisterationComponent,
    AdminProfileComponent,
    AdminDashboardComponent,
    CourseComponent,
    CoursedetailsComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    SharedModule,
    HttpClientModule
  ],
  providers:[
    AdminService
  ]
})
export class AdminModule { }
