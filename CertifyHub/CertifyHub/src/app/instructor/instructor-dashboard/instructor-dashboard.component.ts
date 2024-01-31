import { Component, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ChartType } from 'chart.js';
import { InstructorService } from 'src/app/instructor.service';
 
@Component({
  selector: 'app-instructor-dashboard',
  templateUrl: './instructor-dashboard.component.html',
  styleUrls: ['./instructor-dashboard.component.css']
})
 
export class InstructorDashboardComponent {
  public pieChartType: ChartType = 'pie';
  pieChartOptions: any = {
    responsive: true,
    maintainAspectRatio: false,
    legend: {
        position: 'top',
    },
  };
  userId = 0
  sectionId : number = 0
 
  @ViewChild('myChart') myChart: ElementRef | undefined ;
  topStudents: any[] = [];
 
  constructor(public _instructorService : InstructorService ,private router : ActivatedRoute,private route: Router){}
 
 
 
ngOnInit() {
 
const userString: string | null = localStorage.getItem('user');
 
if (userString) {
  const user = JSON.parse(userString);
  this.userId = user.UserId;
}
 
this._instructorService.GetInstructorSections(this.userId)
this._instructorService.GetInstructorCourseCount(this.userId)
 
this._instructorService.GetInstructorSectionCount(this.userId)
 
this._instructorService.instructorSections.forEach((section:any) => {
  const courseName = section?.course?.COURSENAME;
  console.log(courseName);
});
 
 
 
this._instructorService.GetStudentCount()
 
this._instructorService.GetCoursesByInstructorId(this.userId).subscribe(
  (sectionCounts: any[]) => {
    console.log('Instructor Courses:', sectionCounts);
    let labels: string[] = sectionCounts.map((item: any) => item.courseName);
    let data: number[] = sectionCounts.map((item: any) => item.numberOfSections);
 
    let backgroundColors: string[] = ["#84A9AC" ,"#407088", "#202342", "#959acc", "#FF4081"];
 
    this.pieChartData = {
      labels: labels,
      datasets: [{
        data: data,
        backgroundColor: backgroundColors,
      }],
    };
  },
  (error:any) => {
    console.error('Error fetching instructor courses:', error);
  }
);
 
this._instructorService.GetTopStudents(this.userId).subscribe(
  (topStudents: any[]) => {
    console.log('Top Students:', topStudents);
    this.topStudents = topStudents;
  },
  (error) => {
    console.error('Error fetching top students:', error);
  }
);
 
 
}
 
 
pieChartData: any={}
 
getSectionImagePath(section: any): string {
  return section.imagepath || this.getDefaultImagePath(section.sectionname);
}
 
getAltText(section: any): string {
  return section.imagepath ? section.sectionname : 'Section Image';
}
 
getDefaultImagePath(sectionName: string): string {
  if (sectionName && sectionName.trim() !== '') {
    const firstLetter = sectionName.charAt(0).toUpperCase();
    const secondPart = sectionName.split(' ').slice(1).join('');
    const defaultImageUrl = `https://via.placeholder.com/80/959acc/ffffff?text=${firstLetter}${secondPart}`;
 
    return defaultImageUrl;
  } else {
    return 'https://via.placeholder.com/80/959acc/ffffff?text=Default';
  }
}
 
 
 
ViewSection(sectionId: number)
{
this.route.navigate(['instructor/InstructorSection', sectionId]);
}
}