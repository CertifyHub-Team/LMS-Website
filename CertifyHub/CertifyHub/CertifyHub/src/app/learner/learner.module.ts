import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LearnerRoutingModule } from './learner-routing.module';
import { StudentDashboardComponent } from './student-dashboard/student-dashboard.component';
import { SharedModule } from '../shared/shared.module';
import { StudentProfileComponent } from './student-profile/student-profile.component';
import { LearnerService } from '../learner.service';
import { MatDialog } from '@angular/material/dialog';
import { StudentCvComponent } from './student-cv/student-cv.component';
import { StudentProgramComponent } from './student-program/student-program.component';
import { StudentSectionComponent } from './student-section/student-section.component';
import { HttpClientModule } from '@angular/common/http';
import { CertificateComponent } from './certificate/certificate.component';
import { StudentExamComponent } from './student-exam/student-exam.component';



@NgModule({
  declarations: [
    StudentDashboardComponent,
    StudentProfileComponent,
    StudentCvComponent,
    StudentProgramComponent,
    StudentSectionComponent,
    CertificateComponent,
    StudentExamComponent
  ],
  imports: [
    CommonModule,
    LearnerRoutingModule,
    SharedModule,
    HttpClientModule,
  ],
  providers:[
    LearnerService
  ]
})
export class LearnerModule { }
