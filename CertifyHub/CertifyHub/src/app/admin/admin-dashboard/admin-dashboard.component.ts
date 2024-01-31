import { Component, ElementRef, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ChartType } from 'chart.js';
import { AdminService } from 'src/app/admin.service';
 
 
@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent {
 
 //chart
 public pieChartType: ChartType = 'pie';
 public pieChartType2: ChartType = 'bar';
 pieChartOptions: any = {
  responsive: true,
  maintainAspectRatio: false,
  legend: {
      position: 'top',
  },
};
courseLabels: string[] = [];
sectionCounts: number[] = [];
 
  @ViewChild(MatPaginator) paginator!:MatPaginator
  @ViewChild(MatSort) sort!:MatSort
  @ViewChild('myChart') myChart: ElementRef | undefined ;
 
 
 
 
 
constructor(public _adminService:AdminService){}
 
 
ngOnInit(){
  this._adminService.GetInstructorCount()
  this._adminService.GetStudentCount()
  this._adminService.GetSectionCount()
  this._adminService.GetCourseCount()
 // this.GetStdInfo()
 
 
 this._adminService.GetProgramStudentCount().subscribe(
  (programStudentCounts: any[]) => {
    console.log('Program Student Counts:', programStudentCounts);
 
    let labels: string[] = programStudentCounts.map((item: any) => item.trackName);
    let data: number[] = programStudentCounts.map((item: any) => item.studentCount);
    let backgroundColors: string[] = ["#84A9AC" ,"#407088", "#202342", "#959acc", "#FF4081"];
 
    this.pieChartData = {
      labels: labels,
      datasets: [
        {
          data: data,
          backgroundColor: backgroundColors,
        },
      ],
    };
  },
  (error) => {
    console.error('Error fetching program student counts:', error);
  }
);
 
this._adminService
.GetAllCoursesWithSectionsAndInstructors()
.subscribe(
  (coursesData: any[]) => {
    console.log('Courses Data:', coursesData);
 
    // Process data for the chart
    this.courseLabels = [];
    this.sectionCounts = [];
    let sectionDetails: {
      [courseId: number]: { sections: { name: string; instructor: string }[] };
    } = {};
 
    // Group data by course and count sections
    coursesData.forEach((item: any) => {
      const courseId = item.courseId;
      const sectionName = item.sectionName;
      const instructorName = item.instructorName;
 
      if (!sectionDetails[courseId]) {
        sectionDetails[courseId] = { sections: [] };
        this.courseLabels.push(item.courseName);
      }
 
      sectionDetails[courseId].sections.push({
        name: sectionName,
        instructor: instructorName,
      });
    });
 
    this.courseLabels.forEach((courseLabel) => {
      const courseId = coursesData.find(
        (item) => item.courseName === courseLabel
      )?.courseId;
      const { sections } = sectionDetails[courseId] || { sections: [] };
 
      const sectionCount = sections.length;
      this.sectionCounts.push(sectionCount);
 
      console.log(`Sections for ${courseLabel}:`);
      sections.forEach((section) => {
        console.log(
          ` - ${section.name}, Instructor: ${section.instructor}`
        );
      });
    });
 
    this.coursesChartData = {
      labels: this.courseLabels,
      datasets: [
        {
          label: 'Number of Sections',
          data: this.sectionCounts,
          backgroundColor: 'rgba(75,192,192,0.2)',
          borderColor: 'rgba(75,192,192,1)',
          borderWidth: 1,
        },
      ],
    };
    this.coursesChartOptions = {
      plugins: {
        tooltip: {
          callbacks: {
            label: (tooltipItem: any, data: any) => {
              console.log('Tooltip Data:', tooltipItem, data);
              const index = tooltipItem.index;
              const courseLabel = data.labels[index];
              const courseId = coursesData.find(
                (item: any) => item.courseName === courseLabel
              )?.courseId;
   
              const { sections } = sectionDetails[courseId] || { sections: [] };
              let tooltipText = `Sections for ${courseLabel}:`;
   
              sections.forEach((section) => {
                tooltipText += `\n - ${section.name}, Instructor: ${section.instructor}`;
              });
   
              console.log('Tooltip Text:', tooltipText);
   
              return tooltipText;
            },
          },
        },
      },
     
    };
  },
  (error:any) => {
    console.error('Error fetching courses data:', error);
  }
);
}
 
coursesChartData: any = {};
coursesChartOptions: any = {};
 
pieChartData: any = {};
 
 
 
 
}
 