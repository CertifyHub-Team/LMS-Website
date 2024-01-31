import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';

import { InstructorRoutingModule } from './instructor-routing.module';
import { InstructorSectionComponent } from './instructor-section/instructor-section.component';
import { InstructorProfileComponent } from './instructor-profile/instructor-profile.component';
import { InstructorDashboardComponent } from './instructor-dashboard/instructor-dashboard.component';
import { InstructorSectionsComponent } from './instructor-sections/instructor-sections.component';
import { InstructorService } from '../instructor.service';
import { SharedModule } from '../shared/shared.module';
import { HttpClientModule } from '@angular/common/http';
import { StudentsSolutionsComponent } from './students-solutions/students-solutions.component';
import { FormsModule } from '@angular/forms';
import { ExamDetailsComponent } from './exam-details/exam-details.component';
import { ExamGradesComponent } from './exam-grades/exam-grades.component';


@NgModule({
  declarations: [
    InstructorSectionComponent,
    InstructorProfileComponent,
    InstructorDashboardComponent,
    InstructorSectionsComponent,
    StudentsSolutionsComponent,
    ExamDetailsComponent,
    ExamGradesComponent
  ],
  imports: [
    CommonModule,
    InstructorRoutingModule,
    SharedModule,
    HttpClientModule,
    
  ],
  providers:[
    InstructorService
  ]
})
export class InstructorModule { }
