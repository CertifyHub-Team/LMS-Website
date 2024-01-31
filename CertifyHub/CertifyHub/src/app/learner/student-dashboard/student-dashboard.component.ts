import { Component, OnInit, ViewChild } from '@angular/core';
import { LearnerService } from 'src/app/learner.service';
import { MatCalendar, MatCalendarCellClassFunction, MatCalendarCellCssClasses } from '@angular/material/datepicker';
import { ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Renderer2, ElementRef } from '@angular/core';
import { ViewEncapsulation } from '@angular/core';
import { AdminService } from 'src/app/admin.service';
 
 
@Component({
  selector: 'app-student-dashboard',
  templateUrl: './student-dashboard.component.html',
  styleUrls: ['./student-dashboard.component.css'],
  encapsulation: ViewEncapsulation.None
})
 
 
export class StudentDashboardComponent {
  formattedAssessments: any[] = [];
  selectedDateAssessments: any[] = [];
  assessments: any[] = [];
 
 
  @ViewChild(MatCalendar) matCalendar: MatCalendar<Date> | undefined;
 
  constructor(
    public _learnerService: LearnerService,
    private cdRef: ChangeDetectorRef,
    private renderer: Renderer2, private elRef: ElementRef,
    public _adminService : AdminService
  ){}
 
 
  userId = 0
  ngOnInit()
  {
    const userString: string | null = localStorage.getItem('user');

    if (userString) {
      const user = JSON.parse(userString);
      this.userId = user.UserId;
    }
    const id =this.userId;
    this._learnerService.GetUserSections(id)
    this._learnerService.GetUser(id)
    this._learnerService.getUpcomingAssessments().subscribe(
      (assessments: any) => {
        this._learnerService.upcomingAssessments = assessments;
        this.formatAssessmentsForCalendar();
        this.cdRef.detectChanges();
      },
      error => {
        console.error('Error fetching assessments:', error);
      }
    );
 
    this._learnerService.getAssessmentsForThisWeek();
 
  }
 
  formatAssessmentsForCalendar() {
    this.formattedAssessments = this._learnerService.upcomingAssessments.map((assessment: any) => {
      const parts = assessment.startdate.split('-');
      const year = parseInt(parts[0], 10);
      const month = parseInt(parts[1], 10) - 1;
      const day = parseInt(parts[2], 10);
     
      const startDate = new Date(year, month, day);
      startDate.setHours(0, 0, 0, 0);
      return {
        ...assessment,
        startDate: startDate,
        titleForDisplay: assessment.title
      };
 
    });
 
    this.refreshCalendar();
 
  }
 
  private refreshCalendar(): void {
    if (this.matCalendar) {
      this.matCalendar.updateTodaysDate();
      setTimeout(() => this.renderAssessmentTitles(), 0);
    }
  }
 
  private renderAssessmentTitles(): void {
    setTimeout(() => {
      const calendarCells = this.elRef.nativeElement.querySelectorAll('.mat-calendar-body-cell');
      calendarCells.forEach((cell: HTMLElement) => {
        const ariaLabel = cell.getAttribute('aria-label');
        if (ariaLabel) {
          const cellDate = new Date(ariaLabel);
          const assessment = this.findAssessmentForDate(cellDate);
        if (assessment) {
          const titleElement = this.renderer.createElement('div');
          this.renderer.addClass(titleElement, 'assessment-title');
          this.renderer.setProperty(titleElement, 'textContent', assessment.titleForDisplay);
          this.renderer.setStyle(titleElement, 'position', 'absolute');
          this.renderer.setStyle(titleElement, 'bottom', '-10px');
          this.renderer.setStyle(titleElement, 'width', '100%');
          this.renderer.setStyle(titleElement, 'text-align', 'center');
          this.renderer.setStyle(titleElement, 'font-size', '8px');
          this.renderer.setStyle(titleElement, 'color', '#202342');
          this.renderer.setStyle(titleElement, 'font-weight', 'bolder');
 
            this.renderer.appendChild(cell, titleElement);
        }
      }
      });
    }, 10);
  }
 
  getDisplayText(sectionName: string): string {
    if (sectionName.length < 3) {
      return sectionName.toUpperCase();
    } else {
      return (sectionName.substring(0, 2) + sectionName.charAt(sectionName.length - 1)).toUpperCase();
    }
  }
 
 
  dateClass: MatCalendarCellClassFunction<Date> = (cellDate: Date, view: string) => {
    if (view === 'month') {
      const date = cellDate.getDate();
      const month = cellDate.getMonth();
      const year = cellDate.getFullYear();
 
      const isAssessmentStartDate = this.formattedAssessments.some(assessment => {
        const assessmentStartDate = new Date(assessment.startDate);
        return (
          date === assessmentStartDate.getDate() &&
          month === assessmentStartDate.getMonth() &&
          year === assessmentStartDate.getFullYear()
        );
      });
 
      return isAssessmentStartDate ? 'assessment-start-date' : '';
    }
 
    return '';
  };
 
 
  private findAssessmentForDate(date: Date): any {
    return this.formattedAssessments.find(assessment => {
      const assessmentDate = new Date(assessment.startDate);
      return (
        date.getDate() === assessmentDate.getDate() &&
        date.getMonth() === assessmentDate.getMonth() &&
        date.getFullYear() === assessmentDate.getFullYear()
      );
    });
  }
 
  onAddSubmission(assessment: any): void {
    // Handle the add submission logic here
    console.log('Adding submission for assessment:', assessment);
  }
 
  getIconClass(assessmentType: string): string {
    switch (assessmentType) {
      case 'Exam': return 'fa fa-pencil-square-o';
      case 'Quiz': return 'fa fa-question-circle';
      case 'Task': return 'fa fa-tasks';
      default: return 'fa fa-file-text-o';
    }
  }
 
 
}