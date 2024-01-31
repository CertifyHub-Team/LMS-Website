import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { InstructorProfileComponent } from './instructor-profile/instructor-profile.component';
import { InstructorDashboardComponent } from './instructor-dashboard/instructor-dashboard.component';
import { InstructorSectionsComponent } from './instructor-sections/instructor-sections.component';
import { InstructorSectionComponent } from './instructor-section/instructor-section.component';
import { StudentsSolutionsComponent } from './students-solutions/students-solutions.component';
import { ExamDetailsComponent } from './exam-details/exam-details.component';
import { ExamGradesComponent } from './exam-grades/exam-grades.component';

const routes: Routes = [
  {
    path: 'InstructorProfile',
    component: InstructorProfileComponent
  },
  {
    path: 'InstructorDashboard',
    component: InstructorDashboardComponent
  },
  {
    path: 'InstructorSections',
    component: InstructorSectionsComponent
  },
  {
    path: 'InstructorSection/:id',
    component: InstructorSectionComponent
  },
  {
    path:'StudentsSubmissions/:sectionId/:assessmentId',
    component:StudentsSolutionsComponent
  },
  {
    path:'ExamDetails/:sectionId/:assessmentId',
    component:ExamDetailsComponent
  },
  {
    path:'StudentsGrades/:sectionId/:assessmentId',
    component:ExamGradesComponent
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class InstructorRoutingModule { }
