import { HttpClient, HttpErrorResponse, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import html2canvas from 'html2canvas';
import * as jspdf from 'jspdf';
import 'jspdf-autotable';
import { jwtDecode } from 'jwt-decode';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Observable, catchError, map, tap, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class AdminService {

  constructor(private http : HttpClient, private toastr: ToastrService,  private spinner: NgxSpinnerService, private router : Router) { }

  programs : any = []
  GetAdminPrograms(){
    this.http.get("https://localhost:7000/api/Program/GetAdminPrograms").subscribe(
      {
        next: (x)=>{this.programs = x;
          console.log(this.programs)
          },
        error: (e)=>{console.log(e);
        }
      }
    )
  }

  programCourses : any = []
  GetCoursesByProgram(id : number){
    this.http.get("https://localhost:7000/api/Course/GetCourseByProgramId/" + id).subscribe(
      {
        next: (x)=>{this.programCourses = x;
          console.log(this.programCourses)
          },
        error: (e)=>{console.log(e);
        }
      }
    )
  }

  courseSections : any = []
  GetSectionsByCourse(id : number){
    this.http.get("https://localhost:7000/api/Section/GetSectionByCourseId/" + id).subscribe(
      {
        next: (x)=>{this.courseSections = x;
          console.log(this.courseSections)
          },
        error: (e)=>{console.log(e);
        }
      }
    )
  }

  sectionStudents : any = []
  GetStudentsByCSection(id : number){
    this.http.get("https://localhost:7000/api/StdSection/GetStdSectionsInfoBySectionId/" + id).subscribe(
      {
        next: (x)=>{this.sectionStudents = x;
          console.log(this.sectionStudents)
          },
        error: (e)=>{console.log(e);
        }
      }
    )
  }
  formatDate(dateString: string): string {
    // Use the Angular date pipe with a custom format
    return new Date(dateString).toLocaleDateString('en-US', {
      month: 'short',
      day: 'numeric',
      year: 'numeric',
    });
  }

  async generateSectionPDF() {
    const element = document.getElementById('tab');
 
    if (element) {
      html2canvas(element).then(async (canvas) => {
        const pdf = new jspdf.jsPDF();
 
        // Page 1: Section Report
        pdf.text('Section Report', 14, 10);
        const sectionColumns = ['Course Name', 'Section Name', 'First Name', 'Last Name', 'Username', 'Total Grade'];
        const sectionData = this.sectionStudents.map((student: { coursename: any; sectionname: any; firstname: any; lastname: any; username: any; totalGrade: any; }) => [
          student.coursename,
          student.sectionname,
          student.firstname,
          student.lastname,
          student.username,
          student.totalGrade
        ]);
        // @ts-ignore
        pdf.autoTable({
          head: [sectionColumns],
          body: sectionData,
          startY: 30,
          headStyles: { fillColor: [32, 35, 66] },
        });
 
        // Add a new page for attendance
        pdf.addPage();
        pdf.text('Attendance Section Report', 14, 10);
 
        // Page 2: Attendance Report
        const attendanceColumns = ['First Name', 'Last Name', 'Date of Attendance', 'Status'];
        const attendanceData = this.studentInfo.map((attendance: { firstName: any; lastName: any; dateOfAttendance: any; status: any; }) => [
          attendance.firstName,
          attendance.lastName,
          this.formatDate(attendance.dateOfAttendance),  
          attendance.status
        ]);
        // @ts-ignore
        pdf.autoTable({
          head: [attendanceColumns],
          body: attendanceData,
          startY: 30,
          headStyles: { fillColor: [32, 35, 66] },
        });
 
        // Save the PDF
        const pdfBlob = pdf.output('blob');
        const pdfBlobUrl = URL.createObjectURL(pdfBlob);
 
        const reportWindow = window.open(pdfBlobUrl, '_blank');
 
        if (reportWindow) {
          reportWindow.addEventListener('beforeunload', () => {
            URL.revokeObjectURL(pdfBlobUrl);
          });
        } else {
          console.error('Unable to open a new window. Please check your browser settings.');
        }
      });
    } else {
      console.error('Table element not found');
    }
  }
 
   


  studentGrades : any = []
  GetGradeByUserId(id : number){
    this.http.get("https://localhost:7000/api/Grade/GetGradeByUserId/" + id).subscribe(
      {
        next: (x)=>{this.studentGrades = x;
          console.log(this.studentGrades)
          },
        error: (e)=>{console.log(e);
        }
      }
    )
  }

  async generateStudentPDF() {
    const element = document.getElementById('reports');
 
    if (element) {
      html2canvas(element).then(async (canvas) => {
        const pdf = new jspdf.jsPDF();
 
        pdf.text('Student Report', 14, 10);
 
        const studentColumns = ['Title', 'Course Name', 'First Name', 'Last Name', 'Student Grade'];
        const studentData = this.studentGrades.map((student: { title: any; coursename: any; firstname: any; lastname: any; studentgrade: any; }) => [
          student.title || '',
          student.coursename || '',
          student.firstname || '',
          student.lastname || '',
          student.studentgrade !== null ? student.studentgrade : ''
        ]);
    //@ts-ignore
 
        pdf.autoTable({
          head: [studentColumns],
          body: studentData,
          startY: 30,
          headStyles: { fillColor: [32, 35, 66] },
        });
 
        // Add a new page for student attendance report
        pdf.addPage();
        pdf.text('Student Attendance Report', 14, 10);
 
        // Define columns for the student attendance report table
        const attendanceColumns = ['Section Name', 'Date of Attendance', 'Status'];
        const attendanceData = this.studentAttendance.map((attendance: { sectionName: any; dateOfAttendance: any; status: any; }) => [
          attendance.sectionName,
          this.formatDate(attendance.dateOfAttendance),  
          attendance.status
        ]);
  //@ts-ignore
        pdf.autoTable({
          head: [attendanceColumns],
          body: attendanceData,
          startY: 30,
          headStyles: { fillColor: [32, 35, 66] },
        });
 
       
        const pdfBlob = pdf.output('blob');
 
        const pdfBlobUrl = URL.createObjectURL(pdfBlob);
 
        const reportWindow = window.open(pdfBlobUrl, '_blank');
 
        if (reportWindow) {
          reportWindow.addEventListener('beforeunload', () => {
            URL.revokeObjectURL(pdfBlobUrl);
          });
        } else {
          console.error('Unable to open a new window. Please check your browser settings.');
        }
      });
    } else {
      console.error('Table element not found');
    }
  }

 
  allStudents : any = []
  filteredStudents : any= [];
  GetAllStudents(): Observable<any> {
    return this.http.get("https://localhost:7000/api/Users/GetAllStudents");
  }

sectionCourse :any = {}
  GetCourseBySectionId(sectionId:number){
    this.http.get("https://localhost:7000/api/Section/GetCourseBySectionId/"+sectionId).subscribe(
      {
        next: (x)=>{this.sectionCourse = x;
          console.log(this.sectionCourse)
          },
        error: (e)=>{console.log(e);
        }
      }
    )
  }

  


studentLogin :any = {}
CreateLogin(login:any){
    this.http.post("https://localhost:7000/api/Login/CreateLogin" , login).subscribe(
      {
        next: (x)=>{this.studentLogin = x;
          console.log(this.studentLogin)
          this.toastr.success("login!")

          },
        error: (e)=>{console.log(e);

          this.toastr.warning("not login!")

        }
      }
    )
  }



  
createUsers :any ={};
  CreateUser(user: any): Observable<any> {  
     // Assuming the API returns the created user object, including the userid 
      return this.http.post("https://localhost:7000/api/Users/CreateUser", user); 
    }


  Registration(registration:any){
  this.http.post("https://localhost:7000/api/Users/Registration" , registration).subscribe(
    {
      next: (x)=>{
        
       console.log(registration)

        this.toastr.success("Send Successfully!")
        },
        
      error: (e)=>{console.log(e);
        this.toastr.warning("not send!")

      }
    }
  )

 } 


  
 createSections :any ={};
 CreateSection(section:any){
     this.http.post("https://localhost:7000/api/Section/CreateSection" , section).subscribe(
       {
         next: (x)=>{
           console.log(section)
           this.GetSectionsByCourse(this.testcourse)

           this.toastr.success("Created Successfully!")
           },
         error: (e)=>{console.log(e);
           this.toastr.warning("not Created!")
 
         }
       }
     )
   }



     getAllCourses :any =[];
    GetAllCourses(): Observable<any[]> {
      return this.http.get<any[]>('https://localhost:7000/api/Course/GetAllCourses');
    }
    
    getallInstructors :any =[];

    GetAllInstructors(): Observable<any[]> {
      return this.http.get<any[]>('https://localhost:7000/api/Users/GetAllInstructors');
    }
   
 
    courseInfo :any={} 
    GetCourseById(courseId: any){
        this.http.get("https://localhost:7000/api/Course/GetCourseById/" + courseId).subscribe(
          {
            next: (x)=>{ 
             this.courseInfo=x;
              console.log(this.courseInfo)
              //this.toastr.success("Get course Successfully!")
              },
            error: (e)=>{console.log(e);
             // this.toastr.warning("Faild to get course!")
    
            }
          }
        )
      }
 
    
     
      DeleteSection(sectionId:number){
          this.http.delete("https://localhost:7000/api/Section/DeleteSection/" + sectionId).subscribe(
            {
              next: (x)=>{
                console.log(sectionId)
                this.GetSectionsByCourse(this.testcourse)

                this.toastr.success("Deleted Successfully!")
                },
              error: (e)=>{console.log(e);
                this.toastr.warning("Faild to delete the section.")
      
              }
            }
          )
        }


        sectionUpdated: any = {}
        UpdateSection(sectionUpdate: any){
          this.http.put("https://localhost:7000/api/Section/UpdateSection" , sectionUpdate ).subscribe(
            {
              next: (x)=>{
                this.sectionUpdated=x;
               // console.log(this.sectionUpdated)
               this.GetSectionsByCourse(this.testcourse)

                this.toastr.success("Updated Successfully!")
                },
              error: (e)=>{console.log(e);
                this.toastr.warning("Faild to update the section.")
      
              }
            }
          )

        }


testcourse :number =0
        getCourses(courseid:number){

this.testcourse = courseid
        }



studentStd: any={}

CreateStdSection(student: any){
  this.http.post("https://localhost:7000/api/StdSection/CreateStdSection" , student).subscribe(
    {
      next: (x)=>{
        console.log(student)
        this.GetStudentsByCSection(this.teststudent)

        this.toastr.success("Added Successfully!")
        },
      error: (e)=>{console.log(e);
        this.toastr.warning("not Added!")

      }
    });
  }



  
teststudent :number =0
getStudents(id:number){

this.teststudent = id
}






DeleteStudent(studentId:number, sectionId:number){
  this.http.delete("https://localhost:7000/api/StdSection/DeleteStdSection/" + studentId+"/"+ sectionId).subscribe(
    {
      next: (x)=>{
        console.log(studentId)
        console.log(sectionId)

        this.GetStudentsByCSection(this.teststudent)

        this.toastr.success("Deleted Successfully!")
        },
      error: (e)=>{console.log(e);
        this.toastr.warning("Faild to delete the section.")

      }
    }
  )
}




GenerateCertificate(userId: number, sectionId: number): Observable<Uint8Array> {
  return this.http.get(`https://localhost:7000/api/StudentGrade/GetStudentGradeInfo?userId=${userId}&sectionId=${sectionId}`, { responseType: 'arraybuffer' })
    .pipe(
      map((response: any) => new Uint8Array(response)),
      tap(() => {
        this.toastr.success("Certificate generated successfully");
      }),
      catchError(error => {
        console.error("Error in GenerateCertificate:", error);
        this.toastr.warning('Student must have a CV to generate a certificate.');
        return throwError(error);
      })
    );
}


async sendCertificateAndEmail(certificateBytes: Uint8Array, recipientEmail: string): Promise<boolean> {
  try {
    console.log("email", certificateBytes, recipientEmail)
    const formData = new FormData();
    const certificateBlob = new Blob([certificateBytes], { type: 'application/pdf' });
    formData.append('certificateFile', certificateBlob, 'certificate.pdf');
    formData.append('recipientEmail', recipientEmail);

    // Use Fetch API to send the certificate and email to the API
    const response = await fetch('https://localhost:7000/api/Certificate/SendCertificate', {
      method: 'POST',
      body: formData,
    });

    if (response.ok) {
      console.log('Certificate and email sent successfully.');
      return true;
    } else {
      console.error('Failed to send certificate and email:', response.statusText);
      return false;
    }
  } catch (error) {
    console.error('Error sending certificate and email:', error);
    return false;
  }
}

userInfo : any = {}
GetUserInfo(userId:number){
  this.http.get("https://localhost:7000/api/Cv/GetUserInfo/"+userId).subscribe(
    {
      next: (x)=>{this.userInfo = x;
        },
      error: (e)=>{console.log(e);
      }
    }
  )
 
}

SetFlag(sectionId: number, studentId:number){
  this.http.put(`https://localhost:7000/api/StdSection/SetFlag/${sectionId}/${studentId}`, {Headers}).subscribe(
    {
      next: (response) => {
        console.log(response);
      },
      error: (error) => {
        console.error('Error', error);
      },
    }
  )

}



studentGrade: any = {};
 
  async GetStudentGrade(userId: number, sectionId: number) {
    try {
      const result = await this.http.get(`https://localhost:7000/api/StudentGrade/GetStudentGrade?userId=${userId}&sectionId=${sectionId}`).toPromise();
      this.studentGrade = result;
      console.log("Grade is " + JSON.stringify(this.studentGrade));
    } catch (error) {
      console.error("Error in GetStudentGrade:", error);
    }
  }



  UpdatePassword(login:any){
    this.http.put("https://localhost:7000/api/Login/UpdatePassword",login).subscribe(
      {
        next: (response) => {
          console.log(response);
          this.toastr.success("Password Updated Successfully.")
        },
        error: (error) => {
          console.error('Error', error);
          this.toastr.error("Error in Password Updateding.")
        },
      }
    )
  }

  imageName : string = ""
  UploadImage(data: any) {
    this.http.post('https://localhost:7000/api/Users/UploadImage', data).subscribe(
      {
        next: (result: any) => {
          this.imageName = result.imagepath;
          console.log(this.imageName)
        },
        error: (err) => {
          console.log(err);
        },
      }
    );
    console.log(this.imageName);
  }
UpdateImage(imagePath : string, userId:number)
{
  this.http.put("https://localhost:7000/api/Users/UpdateUserImage/"+userId+"/" + this.courseImage, {Headers}).subscribe(
    {
      next: ()=>{console.log("Updated")
      console.log(imagePath)
    this.GetUserInfo(userId)
    this.toastr.success("Updated Successfully")
    
  },
      error: (err) => {console.log(err)
        this.toastr.error("Error")
      }
      }
)
}

//farah


token:any

Login(data:any){
let headerOptions ={
  'Content-Type' : 'application/json',
      'Accept':'application/json'
     
}

let options = {
  headers: new HttpHeaders( headerOptions)
}

  this.http.post('https://localhost:7000/api/Login/UserLogin',data , options).subscribe(

  {
    next:(result)=>{this.token = result ;
      // this.toastr.toastrConfig.timeOut = 0; 
      // this.toastr.toastrConfig.closeButton = true; 
      // this.toastr.toastrConfig.tapToDismiss = false; 
      // this.toastr.toastrConfig.progressBar = false; 
  
      // this.toastr.info('Logged successfully', 'Success', {
      //   closeButton: true,
      //   disableTimeOut: true,
      //   enableHtml: true 
        
      // });
      // this.toastr.success('Logged successfully') ;
      console.log(this.token);
      localStorage.setItem("token",this.token);
      let user:any = jwtDecode(this.token)
      console.log("user", user);
      localStorage.setItem("user", JSON.stringify(user))
if(user.RoleId == "1"){
this.router.navigate(['admin/AdminDashboard'])
}
else if(user.RoleId == "2"){
  this.router.navigate(['instructor/InstructorDashboard'])
}
else if(user.RoleId == "3"){
  this.router.navigate(['learner/StudentDashboard'])
}
else{
  this.toastr.error('Unauthorized Else');
}
    },
    error:(err)=>{
      this.toastr.error('Unauthorized');
        console.error('Error: ', err);
    }
  }
  )
}


logout() {
  localStorage.removeItem('token');
  localStorage.removeItem('user');
  this.router.navigate(['auth/login']);
}

instructorCount : number = 0
studentCount : number = 0
students:any =[{}]
courses:any=[{}];
programss:any=[{}]


GetInstructorCount(){
  return this.http.get("https://localhost:7000/api/Login/GetInstructorCount").subscribe(
    (count: any)=>{
      this.instructorCount = count;
    },
    (error) => {
      console.error('Error fetching instructor count:', error);
    }
  )
}
GetStudentCount(){
  return this.http.get("https://localhost:7000/api/Login/GetStudentCount").subscribe(
    (count: any)=>{
      this.studentCount = count;
    },
    (error) => {
      console.error('Error fetching instructor count:', error);
    }
  )
}

GetStdSectionsInfo(){
 return this.http.get("https://localhost:7000/api/StdSection/GetStdSectionsInfo");
 
}


GetAllCourses1(){
  this.http.get('https://localhost:7000/api/Course/GetAllCourses').subscribe({
    next:(x)=> {this.courses=x},
    error:(x)=> {console.log(x);
    }
  })
}

GetAllPrograms(){
  this.http.get('https://localhost:7000/api/Program/GetAllPrograms').subscribe({
    next:(x)=> {this.programss=x},
    error:(x)=> {console.log(x);
    }
  })
}





imageName1:string=''
async UploadImage1(data:any){
 await this.http.post('https://localhost:7000/api/Course/uploadCourseImage',data).subscribe({
    next:(result:any)=>{this.imageName1=result.imagepath
  this.GetAllCourses()},
        error:(err)=> {this.toastr.error("Error");}
  })
  }


//////////////newwwwwwwwwwwwwwwww////////////////////////
programImage : string = ""
UploadProgramImage(data: any) {
  this.http.post('https://localhost:7000/api/Program/UploadImage', data).subscribe(
    {
      next: (result: any) => {
        this.programImage = result.imagepath;
        console.log(this.imageName)
      },
      error: (err) => {
        console.log(err);
      },
    }
  );
  console.log(this.imageName);
}

CreateProgram(program:any){

  this.http.post("https://localhost:7000/api/Program/CreateProgram" , program).subscribe(
    {
      next: (x)=>{
        

        this.toastr.success("Created Successfully!")
        this.GetAdminPrograms()
        },
        
      error: (e)=>{console.log(e);
        this.toastr.warning("Error")

      }
    }
  )

 } 
 DeleteProgram(programId:number){
  this.http.delete("https://localhost:7000/api/Program/"+ programId).subscribe(
{
  next: (x)=>{
    this.toastr.success("Deleted Successfully!")
        this.GetAdminPrograms()
  },
  error: (e)=>{
    console.log(e)
    this.toastr.error("Error")
  }
}
  )
 }
 UpdateProgram(program:any){
  this.http.put('https://localhost:7000/api/Program/UpdateProgram',program).subscribe({
    next:()=>{this.toastr.success("Created Successfully!")
    this.GetAdminPrograms()},
        error:(err)=> {this.toastr.error("Error");}
  })
  }
  courseImage : string = ""
   UploadCourseImage(data: any) {
     this.http.post('https://localhost:7000/api/Course/UploadImage', data).subscribe(
      {
        next: (result: any) => {
          this.courseImage = result.imagepath;
          console.log(this.imageName)
        },
        error: (err) => {
          console.log(err);
        },
      }
    );
    console.log(this.imageName);
  }


  programId:number =0
   SetProgramId(programid:number){
    this.programId =programid
   }
  DeleteCourse(courseId:number){
    this.http.delete('https://localhost:7000/api/Course/DeleteCourse/'+courseId).subscribe( 
      {
        next:()=>{this.toastr.success("Deleted Successfully!")
        this.GetCoursesByProgram(this.programId)
     // this.GetAllCourses()
    },
        error:(x)=> {this.toastr.error("Error");
      }
  })
  }
  CreateCourse(data:any){
  
  
    this.http.post('https://localhost:7000/api/Course/CreateCourse',data).subscribe({
    next:()=>{this.toastr.success("Created Successfully!")
    this.GetCoursesByProgram(this.programId)},
        error:(err)=> {this.toastr.error("Error");}
    })
    }
    UpdateCourse(data:any){
      data.imagepath=this.imageName
      this.http.put('https://localhost:7000/api/Course/UpdateCourse',data).subscribe({
        next:()=>{this.toastr.success("Updated Successfully!")
        this.GetCoursesByProgram(this.programId)},
            error:(err)=> {this.toastr.error("Error");}
      })
      }


      downloadCertificate(certificate: any): void {
        if (certificate && certificate.certificatecloudinaryurl) {
          // Fetch the PDF content using HttpClient
          this.http.get(certificate.certificatecloudinaryurl, { responseType: 'arraybuffer' })
            .subscribe((data: ArrayBuffer) => {
              // Create a Blob from the PDF content
              const blob = new Blob([data], { type: 'application/pdf' });
      
              // Create a download link and trigger the download
              const link = document.createElement('a');
              link.href = window.URL.createObjectURL(blob);
              link.download = 'certificate.pdf';
              link.target = '_blank';
              link.click();
            }, (error) => {
              console.error('Error fetching PDF content:', error);
            });
        } else {
          console.error('Certificate object or certificate.certificatecloudinaryurl is undefined:', certificate);
          this.toastr.error("Student doesnt have a certificate.")
        }
      }
      


studentCertificate:any = {}
  async GetStudentCertificate(userId:number, sectionId:number){
    await this.http.get("https://localhost:7000/api/StdSection/GetCertificateUrl/"+userId+"/"+sectionId).subscribe({
      next:(x)=> {this.studentCertificate=x,
      this.downloadCertificate(this.studentCertificate)},
      error:(x)=> {console.log(x);
      }
    })
  }
  UpdateProgramImage(imagePath : string, program:number)
{
  console.log(program)
  this.http.put("https://localhost:7000/api/Program/UpdateProgramImage/"+program+"/" + imagePath, {Headers}).subscribe(
    {
      next: ()=>{console.log("Updated")
      console.log(imagePath)
      this.GetAdminPrograms()
    this.toastr.success("Updated Successfully")
    
  },
      error: (err) => {console.log(err)
        this.toastr.error("Error")
      }
      }
)
}
UpdateCourseImage(imagePath : string, course:number)
{
  //console.log(program)
  this.http.put("https://localhost:7000/api/Course/UpdateCourseImage/"+course+"/" + imagePath, {Headers}).subscribe(
    {
      next: ()=>{console.log("Updated")
      console.log(imagePath)
      this.GetCoursesByProgram(this.programId)
    this.toastr.success("Updated Successfully")
    
  },
      error: (err) => {console.log(err)
        this.toastr.error("Error")
      }
      }
)
}

//eslam
sectionCount: number = 0
courseCount: number = 0
GetSectionCount(){
  return this.http.get("https://localhost:7000/api/Section/GetSectionCount").subscribe(
    (scount: any)=>{
      this.sectionCount = scount;
      console.log("Section Count:", this.sectionCount);
    },
    (error) => {
      console.error('Error fetching section count:', error);
    }
  )
}
GetCourseCount(){
  return this.http.get("https://localhost:7000/api/Course/GetCourseCount").subscribe(
    (ccount: any)=>{
      this.courseCount = ccount;
      console.log(this.courseCount)
    },
    (error) => {
      console.error('Error fetching course count:', error);
    }
  )
}
 
//PROGRAM STUDENT COUNT
GetProgramStudentCount(): Observable<any>{
  return this.http.get("https://localhost:7000/api/Program/GetProgramStudentCounts")
 
}
GetAllCoursesWithSectionsAndInstructors(): Observable<any>{
  return this.http.get("https://localhost:7000/api/Course/GetAllCoursesWithSectionsAndInstructors")
 
}
studentAttendance : any = []
GetAttendanceByUserId(id : number){
  this.http.get("https://localhost:7000/api/Attendance/GetAttendanceByUser/" + id).subscribe(
    {
      next: (x)=>{this.studentAttendance = x;
        console.log(this.studentAttendance)
        },
      error: (e)=>{console.log(e);
      }
    }
  )
}
studentInfo : any = {}
GetStdSectionInfoById(sectionId:number){
  this.http.get("https://localhost:7000/api/StdSection/StudentsInfoForSection/"+ sectionId).subscribe(
    {
      next: (x)=>{
        this.studentInfo = x;
        console.log(x)
       },
      error: (e)=>{console.log(e);
      }
    }
  )
}
}