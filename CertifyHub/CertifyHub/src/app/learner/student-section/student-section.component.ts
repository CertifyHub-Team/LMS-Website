
// import { LearnerService } from 'src/app/learner.service';

// import { Component, OnInit, AfterViewInit, ChangeDetectorRef, ViewChild, ElementRef } from '@angular/core';
// import { ActivatedRoute, Router } from '@angular/router';
// import { MatDialog } from '@angular/material/dialog';
// import * as jspdf from 'jspdf';
// import html2canvas from 'html2canvas';
// import 'jspdf-autotable';
// import { Observable, catchError, forkJoin, lastValueFrom, map, of } from 'rxjs';
// import { FormControl, FormGroup, Validators } from '@angular/forms';
// import { ToastrService } from 'ngx-toastr';
// import { Time } from '@angular/common';
// import { AdminService } from 'src/app/admin.service';
// import { ChartType } from 'chart.js';
 
 
 
// // Define interfaces outside the class
// interface AssessmentSection {
//   title: string;
//   assessmentid: number;
//   // Add other properties of section if there are any
// }
 
// interface GradeResponse {
//   studentgrade?: string;  // Include additional properties as needed
// }
 
// interface MyUserInfo {
//   FIRSTNAME: string;
//   USERNAME: string;
// }

 
 
 
// // declare var $: any;
// @Component({
 
//   selector: 'app-student-section',
 
//   templateUrl: './student-section.component.html',
 
//   styleUrls: ['./student-section.component.css']
 
// })
 
// export class StudentSectionComponent implements OnInit {
 
//   itemId: number = 0;
//   course: number = 0;
//   grade: any = {};
 

//  //chart
//  public pieChartType: ChartType = 'pie';
//  public pieChartType2: ChartType = 'bar';

//   @ViewChild('viewmark') MarkDialog: any
//   @ViewChild('viewupload') UploadDialog: any
//   @ViewChild('viewscore') ScoreDialog: any
//   @ViewChild('attemp') AttempDialog: any
//   @ViewChild('myChart') myChart: ElementRef | undefined;
//   constructor(public _learnerService: LearnerService, private route: ActivatedRoute ,  private cdr: ChangeDetectorRef  , public dialog: MatDialog, private toastr: ToastrService, private router:Router, public _adminService : AdminService) {}
//   userId = 0
//    ngOnInit() {
//     const userString: string | null = localStorage.getItem('user');

//     if (userString) {
//       const user = JSON.parse(userString);
//       this.userId = user.UserId;
//     }

//     this.route.paramMap.subscribe( params => {
 
//       this.itemId = Number(params.get('id'));
 
//       this.course = Number(params.get('courseId'));
//        console.log(this.itemId)
//       this._learnerService.GetSectionById(this.itemId, this.userId);
 
//       this._learnerService.GetMaterialsByCourse(this.course);
//       this._learnerService.GetAssessmentBySection(this.itemId);

//       this._learnerService.getSection(this.course,this.itemId);
//       setTimeout(() => {
//         let arr :any= [this._learnerService.presentcount , this._learnerService.absentcount ]
//         let catarr = ["Present" , "Absence"]
//         let colors: string[] = ["#202342", "#6e74b9"];
//          this.pieChartData= {
//           labels: catarr,
//           datasets: [
//             {
//               data: arr,
//               backgroundColor: colors,
 
//             },
//           ],
//         };
//     }, 1000);
     
//     });

    
//     this.CheckGradeAndDate(this.userId, this.itemId)
     
 
//     this._learnerService.GetPresentCount(this.userId,this.itemId);
//     this._learnerService.GetAbsentCount(this.userId,this.itemId);
//     console.log(this.itemId, this.course);
//     interface AssessmentSection {
//       title: string;
//       assessmentid: number;
//     }
     
//     interface GradeResponse {
//       studentgrade?: string;  
//     }
// }
 
 
// dateFormat = 'MMM d, y';
// timeFormat = 'shortTime';
 
 
 
// async getAssessmentTableDataWithGrades(): Promise<any[]> {
//   const allData: any[] = [];
 
//   for (const section of this._learnerService.assessmentSection) {
//     try {
//       const gradesArray = await this._learnerService.GetUserGrade(this.userId, section.assessmentid);
 
//       if (Array.isArray(gradesArray) && gradesArray.length > 0) {
//         // Process the array to extract grade information
//         const studentGrades = gradesArray
//           .filter(grade => grade && typeof grade === 'object' && 'studentgrade' in grade)
//           .map(grade => grade.studentgrade || 'Not Graded');
 
//         // Add each student grade to allData
//         studentGrades.forEach(studentGrade => {
//           allData.push({
//             'Assessment Title': section.title,
//             'Student Grade': studentGrade,
//           });
//         });
//       } else {
//         // Handle case where gradesArray is empty or not an array
//         allData.push({
//           'Assessment Title': section.title,
//           'Student Grade': 'No Data',
//         });
//       }
//     } catch (err) {
//       console.error(`Error fetching grade for assessment ID ${section.assessmentid}:`, err);
//       allData.push({
//         'Assessment Title': section.title,
//         'Student Grade': 'Error',
//       });
//     }
//   }
 
//   return allData;
// }
 
 
 
// addTableToPDF(pdf: any, title: string, tableData: any[]) {
//   // Add a page
//   pdf.addPage();
 
//   // Add title
//   pdf.text(title, 10, 10);
 
//   // Define columns
//   const columns = Object.keys(tableData[0]);
 
//   // Map data to match the columns
//   const data = tableData.map(row => columns.map(column => row[column]));
 
//   // Use autoTable from jspdf-autotable
//   pdf.autoTable({
//     head: [columns],
//     body: data,
//     startY: 20,
//   });
// }
 
 
// getTableData(): any[] {
//   const allData: any[] = [];
 
//   this._learnerService.courseSection.forEach((section:any) => {
//     section.attendances.forEach((attendance: any) => {
//       allData.push({
//         dateofattendance: attendance.dateofattendance,
//         status: attendance.status,
//       });
//     });
//   });
 
//   return allData;
// }
 
// async generatePDF() {
//   const element = document.getElementById('tabs');
//   const chartDataUrl = await this.generateChartImage();
 
//   if (element) {
//     html2canvas(element).then(async (canvas) => {
//       const pdf = new jspdf.jsPDF();
//       const tableData = this.getTableData();
 
//       // Define columns for attendance table
//       const attendanceColumns = ['Date of Attendance', 'Status'];
 
//       // Add title for attendance report
//       pdf.text('Attendance Report', 14, 10);
 
//       // Map attendance data to match the columns
//       const attendanceData = tableData.map((attendance) => [
//         this.formatDate(attendance.dateofattendance),
//         attendance.status
//       ]);
 
//       // Add attendance table with a gap after the title
//       // @ts-ignore
//       pdf.autoTable({
//         head: [attendanceColumns],
//         body: attendanceData,
//         startY: 30,
//         headStyles: { fillColor: [32, 35, 66] },
     
//       });
//       pdf.addPage();
//       pdf.text('Attendance Chart', 14, 10);
//       const pageWidth = 210;
//       const pageHeight = 297;
 
//       const chartWidth = 140;
//       const chartHeight = 140;
//       const centerX = (pageWidth - chartWidth) / 2;
//       const centerY = (pageHeight - chartHeight) / 2;
     
//       if (chartDataUrl) {
//         pdf.addImage(chartDataUrl, 'PNG', centerX, centerY, chartWidth, chartHeight);
//       }
     
//       // Add new page for assessments and title
//       pdf.addPage();
//       pdf.text('Grades Report', 14, 10); // Title for the grades report
 
//       // Define columns for assessment table
//       const assessmentColumns = ['Assessment Title', 'Grade'];
     
 
//       // Map assessment data to match the columns
//       const assessmentData = [];
 
//       for (const assessment of this._learnerService.assessmentSection) {
//         const userGrade = await this.getUserGrade(assessment.assessmentid);
//         assessmentData.push([assessment.title, userGrade]);
//       }
 
//       // Add assessment table with a gap after the title
//       // @ts-ignore
//       pdf.autoTable({
//         head: [assessmentColumns],
//         body: assessmentData,
//         startY: 30,
//         headStyles: { fillColor: [32, 35, 66] },
//       });
 
     
//         // Save the PDF as a Blob
//         const pdfBlob = pdf.output('blob');
 
//         // Create a Blob URL for the PDF
//         const pdfBlobUrl = URL.createObjectURL(pdfBlob);
 
//         // Open the Blob URL in a new window
//         const reportWindow = window.open(pdfBlobUrl, '_blank');
 
//         // Release the Blob URL when the window is closed
//         if (reportWindow) {
//           reportWindow.addEventListener('beforeunload', () => {
//             URL.revokeObjectURL(pdfBlobUrl);
//           });
//         } else {
//           console.error('Unable to open a new window. Please check your browser settings.');
//         }
 
 
//      // pdf.save('attendance_and_assessment.pdf');
 
   
//     });
//   } else {
//     console.error('Table element not found');
//   }
// }
 
 
 
// async generateChartImage(): Promise<string | null> {
//   if (this.myChart && this.myChart.nativeElement) {
//     const chartCanvas = this.myChart.nativeElement;
//     const chartDataUrl = await html2canvas(chartCanvas).then(canvas => canvas.toDataURL('image/png'));
//     return chartDataUrl;
//   } else {
//     console.error('Chart element not found');
//     return null;
//   }
// }

 
 
 
 
 
 
//  isDateTimeGreaterThanNow(
//   endDate: Date,
//   endTime: string,
//   dateFormat: string,
//   timeFormat: string
// ): boolean {
//   // Combine endDate and endTime into a single Date object
//   const combinedDateTime = new Date(
//     `${endDate.toISOString().split('T')[0]}T${endTime}`
//   );
 
//   // Get the current date and time
//   const currentDateTime = new Date();
 
//   // Format the combinedDateTime and currentDateTime
//   const formattedCombinedDateTime = combinedDateTime.toLocaleDateString('en-US', {
//     year: 'numeric',
//     month: dateFormat.includes('MMM') ? 'short' : undefined,
//     day: dateFormat.includes('d') ? 'numeric' : undefined,
//   }) + ' ' + combinedDateTime.toLocaleTimeString('en-US', {
//     timeStyle: timeFormat === 'shortTime' ? 'short' : undefined,
//   });
 
//   const formattedCurrentDateTime = currentDateTime.toLocaleDateString('en-US', {
//     year: 'numeric',
//     month: dateFormat.includes('MMM') ? 'short' : undefined,
//     day: dateFormat.includes('d') ? 'numeric' : undefined,
//   }) + ' ' + currentDateTime.toLocaleTimeString('en-US', {
//     timeStyle: timeFormat === 'shortTime' ? 'short' : undefined,
//   });
 
//   // Compare the two formatted Date strings
//   return formattedCombinedDateTime > formattedCurrentDateTime;
// }
 
// callDate(endDate:Date , endTime: string) {
 
//   const result = this.isDateTimeGreaterThanNow(endDate, endTime, this.dateFormat, this.timeFormat);
//   console.log(result);
//  return result;
 
// }
 
 
 
// student : any ={}
// callgrade(assessmentid:any) {
 
//   var result= this._learnerService.GetUserGrade(this.userId,assessmentid);
//   console.log(result);
//   this.student= result ;
// }
// // getTableData(): any[] {
// //   const rows = document.querySelectorAll('#example tbody tr');
// //   const data: any[] = [];
 
// //   rows.forEach((row) => {
// //     const rowData: any = {};
// //     const cells = row.querySelectorAll('td');
 
// //     // Extract data from each cell and add it to the rowData object
// //     cells.forEach((cell, index) => {
// //       const columnName = index === 0 ? 'Date of Attendance' : 'Status';
// //       rowData[columnName] = cell.textContent?? ' '.trim();
// //     });
 
// //     data.push(rowData);
// //   });
 
// //   return data;
// // }
 
// formatDate(dateString: string): string {
//   // Use the Angular date pipe with a custom format
//   return new Date(dateString).toLocaleDateString('en-US', {
//     month: 'short',
//     day: 'numeric',
//     year: 'numeric',
//   });
// }
 
 
 
 
// isSubmissionDisabled(endDate: string, endTime: string): boolean {
//   // Get the current date and time
//   const currentDate = new Date();
//   const currentDateTime = currentDate.getTime();
 
//   // Convert the end date and time strings to Date objects
//   const endDateObj = new Date(endDate);
//   const endTimeObj = new Date(endTime);
 
//   // Combine the end date and time into a single Date object
//   const endDateTime = new Date(
//     endDateObj.getFullYear(),
//     endDateObj.getMonth(),
//     endDateObj.getDate(),
//     endTimeObj.getHours(),
//     endTimeObj.getMinutes(),
//     endTimeObj.getSeconds()
//   ).getTime();
 
 
//   console.log('Combined End Date/Time:', new Date(endDateTime));
 
//   // Check conditions and return true to disable the button
//   const isDisabled = currentDateTime > endDateTime;
//   console.log('Is Disabled:', isDisabled);
//   return isDisabled;
// }
 
 
 
 
 
// // OpenMarkDialog(c: any) {
// //   this.callgrade(c);
// //   this.dialog.open(this.MarkDialog, {
   
// //     width: '265px',
// //     height: '162px',
// //   });
// // }
 
 
// OpenMarkDialog(assessmentId: any, assessmentType: string) {
//   if(assessmentType === 'Assignment'){
//     this.getUserGrade(assessmentId)
//     .then((grade) => {
//       this.student.grade = grade;
//       this.dialog.open(this.MarkDialog, {
//         width: '265px',
//         height: '162px',
//       });
//     })
//     .catch((error) => {
//       console.error('Error fetching grade:', error);
//     });
//   }
//   else if (assessmentType === 'Exam'){
//      this._learnerService.GetUserScore(assessmentId, this.userId)
//      this.dialog.open(this.ScoreDialog, {
//       width: '265px',
//       height: '162px',
//     });
  
//   }
 
//   }
 
// async getUserGrade(assessmentId: number): Promise<string> {
//   const userId = this.userId;
 
//   try {
//     await lastValueFrom(this._learnerService.GetUserGrade(userId, assessmentId));
//     console.log(this._learnerService.grade);
 
//     return this._learnerService.grade?.studentgrade || 'Not Graded';
//   } catch (error) {
//     console.error(error);
//     throw error;
//   }
// }
 
 
 
 
// CloseMarkDialog() {
//   this.dialog.closeAll();
// }




 
// showFileUploadForm = false;

// CancleToggle(){
//   this.showFileUploadForm = !this.showFileUploadForm;
// }

// async GetUserSolutions(id:number){
//   await this._learnerService.GetUserSolution(this.userId, id);
// }

// userSol = ''

// async toggleFileUploadForm(id: number,  startDateString: string, endDateString: string, startTimeString: string, endTimeString: string) {
//   try {
//     await this._learnerService.GetUserSolution(this.userId, id);

//     const startDate = new Date(startDateString);
//     const endDate = new Date(endDateString);
//     const startTime = new Date(startTimeString);
//     const endTime = new Date(endTimeString);
  
//     console.log(startDate);
//     console.log(endDate);
//     console.log(startTime);
//     console.log(endTime);
  
//     const startDateTime = new Date(startDate);
//     startDateTime.setHours(startTime.getHours(), startTime.getMinutes());
    
//     const endDateTime = new Date(endDate);
//     endDateTime.setHours(endTime.getHours(), endTime.getMinutes());
  
//     console.log(startDateTime);
//     console.log(endDateTime);
  
//     const now = new Date();
  
    


//     if (this._learnerService.userSolutions && this._learnerService.userSolutions.usersolutiontext !== null) {
//       this.userSol = this._learnerService.userSolutions.usersolutiontext
//       this.dialog.open(this.UploadDialog, {
//         width: '300px',
//         height: '220px',
//       });

//       // Reset userSolutions
//       this._learnerService.userSolutions = null;

//       // Optionally, you can handle any additional logic for when a solution exists
//     } 
//     else if (now <= startDateTime || now >= endDateTime) {
//       this.toastr.warning('Submission period is closed. Please check the submission timeline.');
//     }
//     else {
//       // No solution uploaded, show file upload form
//       this.showFileUploadForm = true;

//       // Set the Assessmentid in the form
//       await this.UploadFormSolution.patchValue({
//         Assessmentid: id,
//       });

//       // Optionally, you can handle any additional logic for when no solution exists
//     }
//   } catch (error) {
//     // Handle error if GetUserSolution fails
//     console.error('Error in GetUserSolution', error);
//   }
// }

// UploadFormSolution: FormGroup = new FormGroup({
//   Userid: new FormControl(this.userId), 
//   Assessmentid: new FormControl(),     
//   Usersolutiontext: new FormControl('', [Validators.required]),
//   Attemptdate: new FormControl(new Date().toISOString()),
//   Questionid: new FormControl(1),
//   Answerid: new FormControl(3)
// });



// selectedFileName: string | null = null;

// UploadFile(event: Event) {
//   const element = event.currentTarget as HTMLInputElement;
//   let file = element.files?.[0];

//   if (file) {
//     this.selectedFileName = file.name;
//     // Update the form control with the actual file object
//     this.UploadFormSolution.patchValue({
//       Usersolutiontext: file
//     });
//     // Call this method if you are using Angular 8+
//     this.UploadFormSolution.get('Usersolutiontext')?.updateValueAndValidity();
//   }
// }





// onSubmit() {
//   if (this.UploadFormSolution.valid) {
//     const formData = new FormData();
//     const fileControl = this.UploadFormSolution.get('Usersolutiontext');
//     const file = fileControl?.value;

//     if (file && file instanceof File) {
//       formData.append('file', file);
//     }

//     // Append other form data
//     formData.append('Userid', this.UploadFormSolution.get('Userid')?.value);
//     formData.append('Assessmentid', this.UploadFormSolution.get('Assessmentid')?.value);
//     // formData.append('Usersolutiontext', this.selectedFileName ?? '');
//     formData.append('Attemptdate', this.UploadFormSolution.get('Attemptdate' )?.value);
//     formData.append('Questionid', this.UploadFormSolution.get('Questionid')?.value);
//     formData.append('Answerid', this.UploadFormSolution.get('Answerid')?.value);

//     // Call the service method to upload the file
//     this._learnerService.uploadFile(formData);  // Pass formData to the service method
//     this.showFileUploadForm = !this.showFileUploadForm;
//   } else {
//     console.error('Form is invalid');
//     // Handle form validation error
//   }
// }



// async CheckGradeAndDate(userId: number, sectionId: number) {
//   try {
//     // Get user info, student's grade, and course info
//     await this._learnerService.GetUserInfo(this.userId);
//     await this._learnerService.GetStudentGrade(this.userId, this.itemId);
//     await this._learnerService.GetCourseBySectionId(sectionId);
 
//     const currentDate = new Date();
//     const endDate = new Date(this._learnerService.sectionCourse.enddate);
//     const grade = this._learnerService.studentGrade.totalgrade;
//     const flag = this._learnerService.studentGrade.flag;
//     const email = this._learnerService.userInfo.username;
 
//     console.log("Grade:", grade, "Email:", email, "Course End Date:", endDate);
 
 
//     if (currentDate >= endDate && grade >= 50 && !flag) {
//       // Generate certificate
//       this._learnerService.GenerateCertificate(userId, sectionId).subscribe(
//         (certificateBytes: Uint8Array) => {
//           // Send certificate and email
//           this._learnerService.sendCertificateAndEmail(certificateBytes, email).then(
//             (success:any) => {
//               if (success) {
//                 this._learnerService.SetFlag(sectionId, userId)
//                 console.log('Proceed with further actions...');
//               } else {
//                 // Handle failure
//                 console.error('Failed to send certificate and email.');
//               }
//             }
//           )
//         },
//         (error) => {
//           console.error("Error while generating certificate:", error);
//         }
//       );
 
//     }
   
//   } catch (error) {
//     console.error("Error in CheckGradeAndDate:", error);
//   }
// }

// getLocalFilePath(): string {
//   return `assets/uploads/${this.userSol}`;
// }

// startdate : Date = new Date()
// enddate : Date = new Date()
// starttime : Date = new Date()
// endtime : Date = new Date()

// ////
// assessmentId:number = 0
// examTitle :string = ''
// async OpenAttempDialog(assessmentId: any, title:any, startdate : string, enddate:string, starttime:string, endtime:string) {
//   await this.GetUserSolutions(assessmentId)
  
//     this.assessmentId = assessmentId
//     this.examTitle = title
//     this.startdate = new Date(startdate);
//     this.enddate = new Date(enddate);
//     this.starttime = new Date(starttime);
//     this.endtime = new Date(endtime);
//     this.dialog.open(this.AttempDialog, {
//       width: '400px',
//       height: '180px'
//     });
  
  
//  }
 

// //  GoToExam(assessmentId : number, title :string, startdate : Date, enddate:Date, starttime:Date, endtime:Date)
// //  {
// //   if(!this._learnerService.userSolutions){
// //    this.router.navigate(['learner/Exam',assessmentId, title]).then(() => {
// //      this.cdr.detectChanges();
// //  });
// //  this.dialog.closeAll()
// // }
// // else if ()
// // else{
// //   this.toastr.warning('You have attemped this exam.')
// // }
// //  }


// GoToExam(assessmentId: number, title: string, startDateString: string, endDateString: string, startTimeString: string, endTimeString: string) {
//   const startDate = new Date(startDateString);
//   const endDate = new Date(endDateString);
//   const startTime = new Date(startTimeString);
//   const endTime = new Date(endTimeString);

//   console.log(startDate);
//   console.log(endDate);
//   console.log(startTime);
//   console.log(endTime);

//   const startDateTime = new Date(startDate);
//   startDateTime.setHours(startTime.getHours(), startTime.getMinutes());
  
//   const endDateTime = new Date(endDate);
//   endDateTime.setHours(endTime.getHours(), endTime.getMinutes());

//   console.log(startDateTime);
//   console.log(endDateTime);

//   const now = new Date();

//   if (now >= startDateTime && now <= endDateTime) {
//     if (!this._learnerService.userSolutions) {
//       this.router.navigate(['learner/Exam', assessmentId, title]).then(() => {
//         this.cdr.detectChanges();
//       });
//       this.dialog.closeAll();
//     } else {
//       this.toastr.warning('You have attempted this exam.');
//     }
//   } else {
//     this.toastr.warning('The exam is not available.');
//   }
// }


// GetSectionNotifications(sectionId:number){
//   this._learnerService.GetNotificationsBySection(sectionId)
// }

// isEndDatePassed(date:Date): boolean {
//   const endDateTime = new Date(date);
//   const now = new Date();
//   console.log(now)
//   console.log(endDateTime)
//   return now > endDateTime;
// }


// pieChartData: any


// }


import { LearnerService } from 'src/app/learner.service';
 
import { Component, OnInit, AfterViewInit, ChangeDetectorRef, ViewChild, ElementRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import * as jspdf from 'jspdf';
import html2canvas from 'html2canvas';
import 'jspdf-autotable';
import { Observable, catchError, forkJoin, lastValueFrom, map, of } from 'rxjs';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Time } from '@angular/common';
import { AdminService } from 'src/app/admin.service';
import { ChartType } from 'chart.js';
 
 
 
// Define interfaces outside the class
interface AssessmentSection {
  title: string;
  assessmentid: number;
  // Add other properties of section if there are any
}
 
interface GradeResponse {
  studentgrade?: string;  // Include additional properties as needed
}
 
interface MyUserInfo {
  FIRSTNAME: string;
  USERNAME: string;
}
 
 
 
 
// declare var $: any;
@Component({
 
  selector: 'app-student-section',
 
  templateUrl: './student-section.component.html',
 
  styleUrls: ['./student-section.component.css']
 
})
 
export class StudentSectionComponent implements OnInit {
 
  itemId: number = 0;
  course: number = 0;
  grade: any = {};
 
 
 //chart
 public pieChartType: ChartType = 'pie';
 public pieChartType2: ChartType = 'bar';
 
  @ViewChild('viewmark') MarkDialog: any
  @ViewChild('viewupload') UploadDialog: any
  @ViewChild('viewscore') ScoreDialog: any
  @ViewChild('attemp') AttempDialog: any
  @ViewChild('myChart') myChart: ElementRef | undefined;
  @ViewChild('updateSubmission') updateSubmissionDialog: any
  @ViewChild('deleteSubmission') deleteSubmissionDialog: any
 
 
  constructor(public _learnerService: LearnerService, private route: ActivatedRoute ,  private cdr: ChangeDetectorRef  , public dialog: MatDialog, private toastr: ToastrService, private router:Router, public _adminService : AdminService) {}
  userId = 0
   ngOnInit() {
    const userString: string | null = localStorage.getItem('user');
 
    if (userString) {
      const user = JSON.parse(userString);
      this.userId = user.UserId;
 
      this.UploadFormSolution.get('Userid')?.setValue(this.userId);
 
    }
 
 
    this.route.paramMap.subscribe( params => {
 
      this.itemId = Number(params.get('id'));
 
      this.course = Number(params.get('courseId'));
       console.log(this.itemId)
      this._learnerService.GetSectionById(this.itemId, this.userId);
 
      this._learnerService.GetMaterialsByCourse(this.course);
      this._learnerService.GetAssessmentBySection(this.itemId);
 
      this._learnerService.getSection(this.course,this.itemId);
      setTimeout(() => {
        let arr :any= [this._learnerService.presentcount , this._learnerService.absentcount ]
        let catarr = ["Present" , "Absence"]
        let colors: string[] = ["#202342", "#6e74b9"];
         this.pieChartData= {
          labels: catarr,
          datasets: [
            {
              data: arr,
              backgroundColor: colors,
 
            },
          ],
        };
    }, 1000);
     
    console.log('userid')
    console.log(this.userId)
    });
 
   
    this.CheckGradeAndDate(this.userId, this.itemId)
     
 
    this._learnerService.GetPresentCount(this.userId,this.itemId);
    this._learnerService.GetAbsentCount(this.userId,this.itemId);
    console.log(this.itemId, this.course);
    interface AssessmentSection {
      title: string;
      assessmentid: number;
    }
     
    interface GradeResponse {
      studentgrade?: string;  
    }
 
 
}
 
 
dateFormat = 'MMM d, y';
timeFormat = 'shortTime';
 
 
 
async getAssessmentTableDataWithGrades(): Promise<any[]> {
  const allData: any[] = [];
 
  for (const section of this._learnerService.assessmentSection) {
    try {
      const gradesArray = await this._learnerService.GetUserGrade(this.userId, section.assessmentid);
 
      if (Array.isArray(gradesArray) && gradesArray.length > 0) {
        // Process the array to extract grade information
        const studentGrades = gradesArray
          .filter(grade => grade && typeof grade === 'object' && 'studentgrade' in grade)
          .map(grade => grade.studentgrade || 'Not Graded');
 
        // Add each student grade to allData
        studentGrades.forEach(studentGrade => {
          allData.push({
            'Assessment Title': section.title,
            'Student Grade': studentGrade,
          });
        });
      } else {
        // Handle case where gradesArray is empty or not an array
        allData.push({
          'Assessment Title': section.title,
          'Student Grade': 'No Data',
        });
      }
    } catch (err) {
      console.error(`Error fetching grade for assessment ID ${section.assessmentid}:`, err);
      allData.push({
        'Assessment Title': section.title,
        'Student Grade': 'Error',
      });
    }
  }
 
  return allData;
}
 
 
 
addTableToPDF(pdf: any, title: string, tableData: any[]) {
  // Add a page
  pdf.addPage();
 
  // Add title
  pdf.text(title, 10, 10);
 
  // Define columns
  const columns = Object.keys(tableData[0]);
 
  // Map data to match the columns
  const data = tableData.map(row => columns.map(column => row[column]));
 
  // Use autoTable from jspdf-autotable
  pdf.autoTable({
    head: [columns],
    body: data,
    startY: 20,
  });
}
 
 
getTableData(): any[] {
  const allData: any[] = [];
 
  this._learnerService.courseSection.forEach((section:any) => {
    section.attendances.forEach((attendance: any) => {
      allData.push({
        dateofattendance: attendance.dateofattendance,
        status: attendance.status,
      });
    });
  });
 
  return allData;
}
 
async generatePDF() {
  const element = document.getElementById('tabs');
  const chartDataUrl = await this.generateChartImage();
 
  if (element) {
    html2canvas(element).then(async (canvas) => {
      const pdf = new jspdf.jsPDF();
      const tableData = this.getTableData();
 
      // Define columns for attendance table
      const attendanceColumns = ['Date of Attendance', 'Status'];
 
      // Add title for attendance report
      pdf.text('Attendance Report', 14, 10);
 
      // Map attendance data to match the columns
      const attendanceData = tableData.map((attendance) => [
        this.formatDate(attendance.dateofattendance),
        attendance.status
      ]);
 
      // Add attendance table with a gap after the title
      // @ts-ignore
      pdf.autoTable({
        head: [attendanceColumns],
        body: attendanceData,
        startY: 30,
        headStyles: { fillColor: [32, 35, 66] },
     
      });
      pdf.addPage();
      pdf.text('Attendance Chart', 14, 10);
      const pageWidth = 210;
      const pageHeight = 297;
 
      const chartWidth = 140;
      const chartHeight = 140;
      const centerX = (pageWidth - chartWidth) / 2;
      const centerY = (pageHeight - chartHeight) / 2;
     
      if (chartDataUrl) {
        pdf.addImage(chartDataUrl, 'PNG', centerX, centerY, chartWidth, chartHeight);
      }
     
      // Add new page for assessments and title
      pdf.addPage();
      pdf.text('Grades Report', 14, 10); // Title for the grades report
 
      // Define columns for assessment table
      const assessmentColumns = ['Assessment Title', 'Grade'];
     
 
      // Map assessment data to match the columns
      const assessmentData = [];
 
      for (const assessment of this._learnerService.assessmentSection) {
        const userGrade = await this.getUserGrade(assessment.assessmentid);
        assessmentData.push([assessment.title, userGrade]);
      }
 
      // Add assessment table with a gap after the title
      // @ts-ignore
      pdf.autoTable({
        head: [assessmentColumns],
        body: assessmentData,
        startY: 30,
        headStyles: { fillColor: [32, 35, 66] },
      });
 
     
        // Save the PDF as a Blob
        const pdfBlob = pdf.output('blob');
 
        // Create a Blob URL for the PDF
        const pdfBlobUrl = URL.createObjectURL(pdfBlob);
 
        // Open the Blob URL in a new window
        const reportWindow = window.open(pdfBlobUrl, '_blank');
 
        // Release the Blob URL when the window is closed
        if (reportWindow) {
          reportWindow.addEventListener('beforeunload', () => {
            URL.revokeObjectURL(pdfBlobUrl);
          });
        } else {
          console.error('Unable to open a new window. Please check your browser settings.');
        }
 
 
     // pdf.save('attendance_and_assessment.pdf');
 
   
    });
  } else {
    console.error('Table element not found');
  }
}
 
 
 
async generateChartImage(): Promise<string | null> {
  if (this.myChart && this.myChart.nativeElement) {
    const chartCanvas = this.myChart.nativeElement;
    const chartDataUrl = await html2canvas(chartCanvas).then(canvas => canvas.toDataURL('image/png'));
    return chartDataUrl;
  } else {
    console.error('Chart element not found');
    return null;
  }
}
 
 
 
 
 
 
 
 isDateTimeGreaterThanNow(
  endDate: Date,
  endTime: string,
  dateFormat: string,
  timeFormat: string
): boolean {
  // Combine endDate and endTime into a single Date object
  const combinedDateTime = new Date(
    `${endDate.toISOString().split('T')[0]}T${endTime}`
  );
 
  // Get the current date and time
  const currentDateTime = new Date();
 
  // Format the combinedDateTime and currentDateTime
  const formattedCombinedDateTime = combinedDateTime.toLocaleDateString('en-US', {
    year: 'numeric',
    month: dateFormat.includes('MMM') ? 'short' : undefined,
    day: dateFormat.includes('d') ? 'numeric' : undefined,
  }) + ' ' + combinedDateTime.toLocaleTimeString('en-US', {
    timeStyle: timeFormat === 'shortTime' ? 'short' : undefined,
  });
 
  const formattedCurrentDateTime = currentDateTime.toLocaleDateString('en-US', {
    year: 'numeric',
    month: dateFormat.includes('MMM') ? 'short' : undefined,
    day: dateFormat.includes('d') ? 'numeric' : undefined,
  }) + ' ' + currentDateTime.toLocaleTimeString('en-US', {
    timeStyle: timeFormat === 'shortTime' ? 'short' : undefined,
  });
 
  // Compare the two formatted Date strings
  return formattedCombinedDateTime > formattedCurrentDateTime;
}
 
callDate(endDate:Date , endTime: string) {
 
  const result = this.isDateTimeGreaterThanNow(endDate, endTime, this.dateFormat, this.timeFormat);
  console.log(result);
 return result;
 
}
 
 
 
student : any ={}
callgrade(assessmentid:any) {
 
  var result= this._learnerService.GetUserGrade(this.userId,assessmentid);
  console.log(result);
  this.student= result ;
}
// getTableData(): any[] {
//   const rows = document.querySelectorAll('#example tbody tr');
//   const data: any[] = [];
 
//   rows.forEach((row) => {
//     const rowData: any = {};
//     const cells = row.querySelectorAll('td');
 
//     // Extract data from each cell and add it to the rowData object
//     cells.forEach((cell, index) => {
//       const columnName = index === 0 ? 'Date of Attendance' : 'Status';
//       rowData[columnName] = cell.textContent?? ' '.trim();
//     });
 
//     data.push(rowData);
//   });
 
//   return data;
// }
 
formatDate(dateString: string): string {
  // Use the Angular date pipe with a custom format
  return new Date(dateString).toLocaleDateString('en-US', {
    month: 'short',
    day: 'numeric',
    year: 'numeric',
  });
}
 
 
 
 
isSubmissionDisabled(endDate: string, endTime: string): boolean {
  // Get the current date and time
  const currentDate = new Date();
  const currentDateTime = currentDate.getTime();
 
  // Convert the end date and time strings to Date objects
  const endDateObj = new Date(endDate);
  const endTimeObj = new Date(endTime);
 
  // Combine the end date and time into a single Date object
  const endDateTime = new Date(
    endDateObj.getFullYear(),
    endDateObj.getMonth(),
    endDateObj.getDate(),
    endTimeObj.getHours(),
    endTimeObj.getMinutes(),
    endTimeObj.getSeconds()
  ).getTime();
 
 
  console.log('Combined End Date/Time:', new Date(endDateTime));
 
  // Check conditions and return true to disable the button
  const isDisabled = currentDateTime > endDateTime;
  console.log('Is Disabled:', isDisabled);
  return isDisabled;
}
 
 
 
 
 
// OpenMarkDialog(c: any) {
//   this.callgrade(c);
//   this.dialog.open(this.MarkDialog, {
   
//     width: '265px',
//     height: '162px',
//   });
// }
 
 
OpenMarkDialog(assessmentId: any, assessmentType: string) {
  if(assessmentType === 'Assignment'){
    this.getUserGrade(assessmentId)
    .then((grade) => {
      this.student.grade = grade;
      this.dialog.open(this.MarkDialog, {
        width: '265px',
        height: '162px',
      });
    })
    .catch((error) => {
      console.error('Error fetching grade:', error);
    });
  }
  else if (assessmentType === 'Exam'){
     this._learnerService.GetUserScore(assessmentId, this.userId)
     this.dialog.open(this.ScoreDialog, {
      width: '265px',
      height: '162px',
    });
 
  }
 
  }
 
async getUserGrade(assessmentId: number): Promise<string> {
  const userId = this.userId;
 
  try {
    await lastValueFrom(this._learnerService.GetUserGrade(userId, assessmentId));
    console.log(this._learnerService.grade);
 
    return this._learnerService.grade?.studentgrade || 'Not Graded';
  } catch (error) {
    console.error(error);
    throw error;
  }
}
 
 
 
 
CloseMarkDialog() {
  this.dialog.closeAll();
}
 
 
 
 
 
showFileUploadForm = false;
 
CancleToggle(){
  this.showFileUploadForm = !this.showFileUploadForm;
}
 
async GetUserSolutions(id:number){
  await this._learnerService.GetUserSolution(this.userId, id);
}
 
userSol = ''
userSolution = []
userSolutionId=0
async toggleFileUploadForm(id: number,  startDateString: string, endDateString: string, startTimeString: string, endTimeString: string) {
  try {
    await this._learnerService.GetUserSolution(this.userId, id);
 
    const startDate = new Date(startDateString);
    const endDate = new Date(endDateString);
    const startTime = new Date(startTimeString);
    const endTime = new Date(endTimeString);
 
    console.log(startDate);
    console.log(endDate);
    console.log(startTime);
    console.log(endTime);
 
    const startDateTime = new Date(startDate);
    startDateTime.setHours(startTime.getHours(), startTime.getMinutes());
   
    const endDateTime = new Date(endDate);
    endDateTime.setHours(endTime.getHours(), endTime.getMinutes());
 
    console.log(startDateTime);
    console.log(endDateTime);
 
    const now = new Date();
 
   
 
 
    if (this._learnerService.userSolutions && this._learnerService.userSolutions.usersolutiontext !== null) {
      this.userSol = this._learnerService.userSolutions.usersolutiontext
      this.userSolution = this._learnerService.userSolutions
      this.userSolutionId = this._learnerService.userSolutions.solutionid
      this.dialog.open(this.UploadDialog, {
        width: '300px',
        height: '220px',
      });
 
      // Reset userSolutions
      this._learnerService.userSolutions = null;
 
      // Optionally, you can handle any additional logic for when a solution exists
    }
    else if (now <= startDateTime || now >= endDateTime) {
      this.toastr.warning('Submission period is closed. Please check the submission timeline.');
    }
    else {
      // No solution uploaded, show file upload form
      this.showFileUploadForm = true;
 
      // Set the Assessmentid in the form
      await this.UploadFormSolution.patchValue({
        Assessmentid: id,
      });
 
      // Optionally, you can handle any additional logic for when no solution exists
    }
  } catch (error) {
    // Handle error if GetUserSolution fails
    console.error('Error in GetUserSolution', error);
  }
}
 
 
UploadFormSolution: FormGroup = new FormGroup({
 
  Userid: new FormControl(),
  Assessmentid: new FormControl(),
  Usersolutiontext: new FormControl('', [Validators.required]),
  Attemptdate: new FormControl(new Date().toISOString()),
  Questionid: new FormControl(1),
  Answerid: new FormControl(3)
});
 
 
UpdateUploadFormSolution: FormGroup = new FormGroup({
  Solutionid:new FormControl(),
  Userid: new FormControl(),
  Assessmentid: new FormControl(),
  Usersolutiontext: new FormControl(),
  Attemptdate: new FormControl(),
  Questionid: new FormControl(1),
  Answerid: new FormControl(3)
});
 
 
async OpenUpdateSubmisssionDialog(c : any) {
  console.log('c is: ' , c);
    this.selectedFileName = c.usersolutiontext ? c.usersolutiontext.name : null;
    console.log(c.usersolutiontext)
    await this.UpdateUploadFormSolution.patchValue({
      Solutionid: c.solutionid,
      Userid: c.userid,
      Assessmentid: c.assessmentid,
      Usersolutiontext: c.usersolutiontext,
      Attemptdate: c.attemptdate,
      Questionid: c.questionid,
      Answerid: c.answerid
    });
   
 
  this.dialog.open(this.updateSubmissionDialog, {
    width: '450px',
    height: '580px',
    enterAnimationDuration: 1000,
    exitAnimationDuration: 1000,
  });
}
 
CloseUpdateSubmissionDialog(){
  this.dialog.closeAll( )
 
  }
async UpdateSubmission() {
  console.log('UpdateSubmission called');
 
  const formData = new FormData();
 
  console.log(this.UpdateUploadFormSolution.get('Solutionid')!.value)
  formData.append('Solutionid', this.UpdateUploadFormSolution.get('Solutionid')!.value);
  formData.append('Userid', this.UpdateUploadFormSolution.get('Userid')!.value);
  formData.append('Assessmentid', this.UpdateUploadFormSolution.get('Assessmentid')!.value);
  formData.append('Questionid', this.UpdateUploadFormSolution.get('Questionid')!.value);
  formData.append('Answerid', this.UpdateUploadFormSolution.get('Answerid')!.value);
 
  // Append file if it's available
  if (this.selectedFileName) {
      formData.append('Usersolutiontext',this.UpdateUploadFormSolution.value.Usersolutiontext.name)
      formData.append('Attemptdate', new Date().toISOString());
 
  }
 
  try {
    console.log(formData)
      await this._learnerService.UpdateSubmission(formData);
      await this.CloseUpdateSubmissionDialog();
  } catch (error) {
      console.error('Error in UpdateMaterial:', error);
  }
}
 
editSubmissionFile(event: Event) {
  const element = event.currentTarget as HTMLInputElement;
  const file = element.files?.[0];
 
  if (file) {
    this.selectedFileName = file.name;
    // Update the form control with the actual file object
    this.UpdateUploadFormSolution.patchValue({
      Usersolutiontext: file
    });
    // Call this method if you are using Angular 8+
    this.UpdateUploadFormSolution.get('Usersolutiontext')?.updateValueAndValidity();
  }
}
 
 
selectedFileName: string | null = null;
 
UploadFile(event: Event) {
 
  const element = event.currentTarget as HTMLInputElement;
  let file = element.files?.[0];
 
  if (file) {
    this.selectedFileName = file.name;
    // Update the form control with the actual file object
    this.UploadFormSolution.patchValue({
      Usersolutiontext: file
    });
    // Call this method if you are using Angular 8+
    this.UploadFormSolution.get('Usersolutiontext')?.updateValueAndValidity();
  }
}
 
OpenConfirmDeleteSubmissionDialog(solutionid : number)
{
  var dialog = this.dialog.open(this.deleteSubmissionDialog)
dialog.afterClosed().subscribe(
 
  (result)=>{
    if(result == 'yes')
    this.DeleteSubmission(solutionid);
  else
  this.deleteSubmissionDialog();
  }
    )
 
}
 
async DeleteSubmission(id : number)
{
  await this._learnerService.DeleteSubmistion(id);
  console.log(id)
 
 
    await this.CloseDeleteSubmisiionDialog()
 
}
 
CloseDeleteSubmisiionDialog(){
  this.dialog.closeAll( )
 
  }
 
 
 
 
 
onSubmit() {
  if (this.UploadFormSolution.valid) {
    const formData = new FormData();
    const fileControl = this.UploadFormSolution.get('Usersolutiontext');
    const file = fileControl?.value;
 
    if (file && file instanceof File) {
      formData.append('file', file);
    }
 
    // Append other form data
    console.log('userid is : ' , this.UploadFormSolution.get('Userid')?.value)
    formData.append('Userid', this.UploadFormSolution.get('Userid')?.value);
    formData.append('Assessmentid', this.UploadFormSolution.get('Assessmentid')?.value);
    // formData.append('Usersolutiontext', this.selectedFileName ?? '');
    formData.append('Attemptdate', this.UploadFormSolution.get('Attemptdate' )?.value);
    formData.append('Questionid', this.UploadFormSolution.get('Questionid')?.value);
    formData.append('Answerid', this.UploadFormSolution.get('Answerid')?.value);
 
    // Call the service method to upload the file
    this._learnerService.uploadFile(formData);  // Pass formData to the service method
    this.showFileUploadForm = !this.showFileUploadForm;
  } else {
    console.error('Form is invalid');
    // Handle form validation error
  }
}
 
 
 
async CheckGradeAndDate(userId: number, sectionId: number) {
  try {
    // Get user info, student's grade, and course info
    await this._learnerService.GetUserInfo(this.userId);
    await this._learnerService.GetStudentGrade(this.userId, this.itemId);
    await this._learnerService.GetCourseBySectionId(sectionId);
 
    const currentDate = new Date();
    const endDate = new Date(this._learnerService.sectionCourse.enddate);
    const grade = this._learnerService.studentGrade.totalgrade;
    const flag = this._learnerService.studentGrade.flag;
    const email = this._learnerService.userInfo.username;
 
    console.log("Grade:", grade, "Email:", email, "Course End Date:", endDate);
 
 
    if (currentDate >= endDate && grade >= 50 && !flag) {
      // Generate certificate
      this._learnerService.GenerateCertificate(userId, sectionId).subscribe(
        (certificateBytes: Uint8Array) => {
          // Send certificate and email
          this._learnerService.sendCertificateAndEmail(certificateBytes, email).then(
            (success:any) => {
              if (success) {
                this._learnerService.SetFlag(sectionId, userId)
                console.log('Proceed with further actions...');
              } else {
                // Handle failure
                console.error('Failed to send certificate and email.');
              }
            }
          )
        },
        (error) => {
          console.error("Error while generating certificate:", error);
        }
      );
 
    }
   
  } catch (error) {
    console.error("Error in CheckGradeAndDate:", error);
  }
}
 
getLocalFilePath(): string {
  return `assets/uploads/${this.userSol}`;
}
 
startdate : Date = new Date()
enddate : Date = new Date()
starttime : Date = new Date()
endtime : Date = new Date()
 
////
assessmentId:number = 0
examTitle :string = ''
async OpenAttempDialog(assessmentId: any, title:any, startdate : string, enddate:string, starttime:string, endtime:string) {
  await this.GetUserSolutions(assessmentId)
 
    this.assessmentId = assessmentId
    this.examTitle = title
    this.startdate = new Date(startdate);
    this.enddate = new Date(enddate);
    this.starttime = new Date(starttime);
    this.endtime = new Date(endtime);
    this.dialog.open(this.AttempDialog, {
      width: '400px',
      height: '180px'
    });
 
 
 }
 
 
//  GoToExam(assessmentId : number, title :string, startdate : Date, enddate:Date, starttime:Date, endtime:Date)
//  {
//   if(!this._learnerService.userSolutions){
//    this.router.navigate(['learner/Exam',assessmentId, title]).then(() => {
//      this.cdr.detectChanges();
//  });
//  this.dialog.closeAll()
// }
// else if ()
// else{
//   this.toastr.warning('You have attemped this exam.')
// }
//  }
 
 
GoToExam(assessmentId: number, title: string, startDateString: string, endDateString: string, startTimeString: string, endTimeString: string) {
  const startDate = new Date(startDateString);
  const endDate = new Date(endDateString);
  const startTime = new Date(startTimeString);
  const endTime = new Date(endTimeString);
 
  console.log(startDate);
  console.log(endDate);
  console.log(startTime);
  console.log(endTime);
 
  const startDateTime = new Date(startDate);
  startDateTime.setHours(startTime.getHours(), startTime.getMinutes());
 
  const endDateTime = new Date(endDate);
  endDateTime.setHours(endTime.getHours(), endTime.getMinutes());
 
  console.log(startDateTime);
  console.log(endDateTime);
 
  const now = new Date();
 
  if (now >= startDateTime && now <= endDateTime) {
    if (!this._learnerService.userSolutions) {
      this.router.navigate(['learner/Exam', assessmentId, title]).then(() => {
        this.cdr.detectChanges();
      });
      this.dialog.closeAll();
    } else {
      this.toastr.warning('You have attempted this exam.');
    }
  } else {
    this.toastr.warning('The exam is not available.');
  }
}
 
 
GetSectionNotifications(sectionId:number){
  this._learnerService.GetNotificationsBySection(sectionId)
}
 
isEndDatePassed(date:Date): boolean {
  const endDateTime = new Date(date);
  const now = new Date();
  console.log(now)
  console.log(endDateTime)
  return now > endDateTime;
}
 
 
pieChartData: any
 
 
}