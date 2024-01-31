import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudentHeaderComponent } from './student-header/student-header.component';
import { StudentSidebarComponent } from './student-sidebar/student-sidebar.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import { BrowserAnimationsModule, provideAnimations } from '@angular/platform-browser/animations';
import {MatDialogModule} from '@angular/material/dialog';
import { provideToastr } from 'ngx-toastr';
import { BrowserModule } from '@angular/platform-browser';
import { NgxSpinnerModule } from "ngx-spinner";
import { RouterModule } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import {MatTabsModule} from '@angular/material/tabs';
import { PdfViewerModule } from 'ng2-pdf-viewer';
import { AdminHeaderComponent } from './admin-header/admin-header.component';
import { AdminSidebarComponent } from './admin-sidebar/admin-sidebar.component';
import {MatNativeDateModule} from '@angular/material/core';
import { MatCalendarCellClassFunction, MatDatepickerModule } from '@angular/material/datepicker';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatSelectModule } from '@angular/material/select';
import { AdminService } from '../admin.service';
import { NgChartsModule } from 'ng2-charts';
import {MatPaginatorModule} from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { InstructorHeaderComponent } from './instructor-header/instructor-header.component';
import { InstructorSidebarComponent } from './instructor-sidebar/instructor-sidebar.component';

@NgModule({
  declarations: [
    StudentHeaderComponent,
    StudentSidebarComponent,
    AdminHeaderComponent,
    AdminSidebarComponent,
    InstructorHeaderComponent,
    InstructorSidebarComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,
    MatTabsModule,
   PdfViewerModule,
   NgxSpinnerModule,
   MatSelectModule
  ],
  exports: [
    StudentHeaderComponent,
    StudentSidebarComponent,
    AdminHeaderComponent,
    AdminSidebarComponent,
    ReactiveFormsModule,
    MatFormFieldModule,
    NgxSpinnerModule,
    MatCardModule,
    MatProgressBarModule,
    MatTabsModule,
    PdfViewerModule,
    MatDialogModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatTooltipModule,
    NgxSpinnerModule,
    MatSelectModule,
    NgChartsModule,
    MatPaginatorModule,
    MatTableModule,
    MatSortModule,
    InstructorHeaderComponent,
    InstructorSidebarComponent,
    FormsModule,
  ],
  providers:[
    provideToastr(),
    AdminService
  ]
})
export class SharedModule { }
