import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import html2canvas from 'html2canvas';
import * as jspdf from 'jspdf';
import 'jspdf-autotable';
import { ToastPackage, ToastrModule, ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
interface UserGradeResponse {
  studentgrade: number;
}
@Injectable({
  providedIn: 'root'
})
export class InstructorService {

  constructor(private http : HttpClient, private toastr: ToastrService) { }
  userInfo : any = {}
  userCv : any = {}
  unDocumented : boolean = true
  myCourses : any = {}


  
  GetUserInfo(userId: number){
    console.log(userId)
    this.http.get("https://localhost:7000/api/Cv/GetUserInfo/"+userId).subscribe(
      {
        next: (x)=>{this.userInfo = x;
         },
        error: (e)=>{console.log(e);
        }
      }
    )
    
  }
  
  GetUserCvInfo(userId: number){
    this.http.get("https://localhost:7000/api/Cv/GetUserCv/"+userId, { observe: 'response' }).subscribe({
      next: (response) => {
        if (response && response.status !== undefined) {
          const responseStatus = response.status;
  
          this.userCv = response.body; 
  
          if (responseStatus === 204) {
            console.log('Undocumented');
            console.log('_learnerService.unDocumented:', this.unDocumented);
            this.unDocumented = true;

          } else {
            console.log('documented');
            this.unDocumented = false;
          }
          console.log('_learnerService.unDocumented:', this.unDocumented);
        } else {
          console.log('Invalid response format');
          console.log('_learnerService.unDocumented:', this.unDocumented);
        }
      },
      error: (err) => {
        console.log(err);
        this.toastr.error("Error");
      }
    });
  }
  
  
  
  CreateCv(data: any, userId: number) {
    this.http.post("https://localhost:7000/api/Cv", data).subscribe({
      next: () => {
        console.log("Created");
        this.toastr.success("Created Successfully");
        this.GetUserCvInfo(userId);
      },
      error: (err) => {
        console.error('Error:', err);
  
        if (err instanceof HttpErrorResponse) {
          if (err.status === 400) {
            console.log('Validation Errors:', err.error.errors);
            // Handle validation errors here
            this.toastr.error("Validation Errors. Please check the form.");
          } else {
            this.toastr.error("Server Error. Please try again later.");
          }
        } else {
          this.toastr.error("An unexpected error occurred.");
        }
      },
    });
  }
  

  UpdateCv(data: any, userId:number)
  {
    debugger
    this.http.put("https://localhost:7000/api/Cv" , data).subscribe(
      {
        next: ()=>{console.log("Updated")
      // this.GetAllCourses()
      this.toastr.success("Updated Successfully")
      // window.location.reload()
      this.GetUserCvInfo(userId)
    },
        error: (err) => {console.log(err)
          this.toastr.error("Error")
        }
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
  this.http.put("https://localhost:7000/api/Users/UpdateUserImage/"+userId+"/" + imagePath, {Headers}).subscribe(
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

instructorSections : any = []
GetInstructorSections(userId: number){
  console.log(userId)
  this.http.get("https://localhost:7000/api/Section/GetInstructorSections/"+userId).subscribe(
    {
      next: (x)=>{this.instructorSections = x;
        console.log(x)
       },
      error: (e)=>{console.log(e);
      }
    }
  )
  
}

courseId =0
sectionInf : any = {}
sectionInfo(sectionId:number){
  console.log('Fetching section info for sectionId:', sectionId);
 
  this.http.get("https://localhost:7000/api/Section/GetSectionInfo/"+sectionId).subscribe(
    {
      next: (x)=>{this.sectionInf = x;
        console.log(x)
        this.courseId = this.sectionInf.courseid;
        this.GetMaterialsByCourse(this.courseId);
 
       },
      error: (e)=>{console.log(e);
      }
    }
  )
}

CreateNotification(notification:any){
  this.http.post("https://localhost:7000/api/Notification/CreateNotification", notification).subscribe(
    {
      next: (x)=>{this.toastr.success("Created Successfully");
       },
      error: (e)=>{console.log(e);
        this.toastr.error("Error");
      }
    }
  )
}

//new

sectionAssessments : any = []
GetSectionAssessments(sectionId:number){
  this.http.get("https://localhost:7000/api/Assessment/GetAssessmentBysectionId/"+sectionId).subscribe(
    {
      next: (x)=>{this.sectionAssessments = x;
        console.log(x)
       },
      error: (e)=>{console.log(e);
      }
    }
  )
}

CreateAssessment(assessment:any, sectionId: number){
  this.http.post("https://localhost:7000/api/Assessment", assessment).subscribe(
    {
      next: (x)=>{this.toastr.success("Created Successfully");
      this.GetSectionAssessments(sectionId)
       },
      error: (e)=>{console.log(e);
        this.toastr.error("Error");
      }
    }
  )
}

assessmentUpdated: any = {}
UpdateSAssessment(assessment: any, sectionId: number){
  this.http.put("https://localhost:7000/api/Assessment" , assessment ).subscribe(
    {
      next: (x)=>{
        this.assessmentUpdated=x;
        // console.log(this.sectionUpdated)
        this.GetSectionAssessments(sectionId)

        this.toastr.success("Updated Successfully!")
        },
        error: (e)=>{console.log(e);
        this.toastr.warning("Faild to update the section.")
      
        }
      }
    )

  }

  DeleteAssessment(assessmentId:number, sectionId:number){
    this.http.delete("https://localhost:7000/api/Assessment/" + assessmentId).subscribe(
      {
        next: (x)=>{
          console.log(sectionId)
          this.GetSectionAssessments(sectionId)

          this.toastr.success("Deleted Successfully!")
          },
        error: (e)=>{console.log(e);
          this.toastr.warning("Faild to delete the section.")

        }
      }
    )
  } 

studentsSolution:any = []
  GetStudentsSolutionBySection(sectionId:number, assessmentId:number){
    this.http.get("https://localhost:7000/api/StdSection/GetStdSectionsBySection/"+sectionId+"/"+assessmentId).subscribe(
      {
        next: (x)=>{this.studentsSolution = x;
          console.log(x)
         },
        error: (e)=>{console.log(e);
        }
      }
    )
  }
  AddStudentsGrades(studentInfo : any[], sectionId:number, assessmentId:number){
    this.http.post("https://localhost:7000/api/Grade", studentInfo).subscribe(
      {
        next: (x)=>{this.toastr.success("Created Successfully");
        this.GetStudentsSolutionBySection(sectionId, assessmentId)
         },
        error: (e)=>{console.log(e);
          this.toastr.error("Error");
        }
      }
    )
  }

  UpdateGrade(studentInfo:any, sectionId:number, assessmentId:number){
    console.log(studentInfo)
    this.http.put("https://localhost:7000/api/Grade" , studentInfo ).subscribe(
      {
        next: (x)=>{
          this.assessmentUpdated=x;
          this.GetStudentsSolutionBySection(sectionId, assessmentId)
          this.toastr.success("Updated Successfully!")
          },
          error: (e)=>{console.log(e);
          this.toastr.warning("Faild to update the section.")
        
          }
        }
      )
  
    }
   
    userGrade: number = 0;


GetUserGrade(assessmentId: number, userId: number) {
  this.http.get<number>(`https://localhost:7000/api/Grade/GetUserGrade?userid=${userId}&assessmentid=${assessmentId}`).subscribe(
    {
      next: (grade) => {
        this.userGrade = grade;
        console.log("grade",grade)
      },
      error: (error) => {
        console.log('Error fetching user grade:', error);
      }
    }
  );
}
assessmentQuestions:any = []
GetQuestionsAndAnswers(assessmentId:number){
  this.http.get("https://localhost:7000/api/Answer/ListQuestionsAndAnswers/"+assessmentId).subscribe(
    {
      next: (x)=>{this.assessmentQuestions = x;
        console.log(x)
       },
      error: (e)=>{console.log(e);
      }
    }
  )
}
  CreateQuestion(question:any, assessmantid: number){
    this.http.post("https://localhost:7000/api/Question", question).subscribe(
      {
        next: (x)=>{this.toastr.success("Created Successfully");
        this.GetQuestionsAndAnswers(assessmantid)
         },
        error: (e)=>{console.log(e);
          this.toastr.error("Error");
        }
      }
    )
  }

  UpdateQuestion(question:any, assessmantid:number){
    console.log(question)
    this.http.put("https://localhost:7000/api/Question/UpdateQuestion" , question ).subscribe(
      {
        next: (x)=>{
  
          this.toastr.success("Updated Successfully!")
          this.GetQuestionsAndAnswers(assessmantid)
          },
          error: (e)=>{console.log(e);
          this.toastr.warning("Faild to update the section.")
        
          }
        }
      )
  
    }
    DeleteQuestion(questionId:number,assessmentId:number){
      this.http.delete("https://localhost:7000/api/Question/" + questionId).subscribe(
        {
          next: (x)=>{
            console.log(questionId)
            this.GetQuestionsAndAnswers(assessmentId)
  
            this.toastr.success("Deleted Successfully!")
            },
          error: (e)=>{console.log(e);
            this.toastr.warning("Faild to delete the section.")
  
          }
        }
      )
    }
    CreateAnswer(answer:any, questionid: number){
      this.http.post("https://localhost:7000/api/Answer/CreateAnswer", answer).subscribe(
        {
          next: (x)=>{this.toastr.success("Created Successfully");
          this.GetQuestionsAndAnswers(questionid)
           },
          error: (e)=>{console.log(e);
            this.toastr.error("Error");
          }
        }
      )
    }
    UpdateAnswer(answer:any, assessmantid:number){
      console.log("Serviceee",answer)
      answer.iscorrect = String(answer.iscorrect)
      this.http.put("https://localhost:7000/api/Answer/UpdateAnswer" , answer ).subscribe(
        {
          next: (x)=>{
    
            this.toastr.success("Updated Successfully!")
            this.GetQuestionsAndAnswers(assessmantid)
            },
            error: (e)=>{console.log(e);
            this.toastr.warning("Faild to update the section.")
          
            }
          }
        )
    
      }
    DeleteAnswer(answerId:number,assessmentId:number){
      this.http.delete("https://localhost:7000/api/Answer/DeleteAnswer/" + answerId).subscribe(
        {
          next: (x)=>{
            console.log(answerId)
            this.GetQuestionsAndAnswers(assessmentId)
  
            this.toastr.success("Deleted Successfully!")
            },
          error: (e)=>{console.log(e);
            this.toastr.warning("Faild to delete the section.")
  
          }
        }
      )
    }
    studentsGrades:any = []
    GetStudentsGrades(assessmentId:number){
  this.http.get("https://localhost:7000/api/StdSection/ListStudentsGrades/"+assessmentId).subscribe(
    {
      next: (x)=>{this.studentsGrades = x;
        console.log(x)
       },
      error: (e)=>{console.log(e);
      }
    }
  )
}



//shatha

AddMaterial(formData: FormData) {
  const url = 'https://localhost:7000/api/Material';
 
  const courseid = formData.get('courseid') as string | null;
  console.log(courseid);
 
  const finalFormData = new FormData();
  formData.forEach((value, key) => {
    finalFormData.append(key, value);
  });
 
  const file = formData.get('materialpath') as File;
  if (file) {
    finalFormData.append('materialpath', file, file.name);
  }
 
  console.log('finalFormData:', finalFormData);
  this.http.post(url, finalFormData).subscribe({
    next: (response) => {
      console.log('Data saved successfully', response);
   
      if (typeof response === 'string') {
        // Handle non-JSON response (assuming it's a success message)
        this.toastr.success(response);
      } else {
        // Handle JSON response
        this.toastr.success('File uploaded successfully');
   
        const numericCourseId = courseid ? +courseid : null;
        if (numericCourseId !== null) {
          this.GetMaterialsByCourse(numericCourseId);
        } else {
          console.log('courseid is null');
        }
      }
    },
   
    error: (error) => {
      console.error('Error saving data', error);
   
      if (error instanceof HttpErrorResponse) {
        console.error('Server error:', error.status, error.statusText);
       
        // Check if the response is valid JSON
        try {
          const responseBody = JSON.parse(error.error);
         
          // If it's a valid JSON response, handle it as a success message
          this.toastr.success(responseBody.error);
        } catch (jsonError) {
          // If parsing fails or it's not a JSON response, handle it as a standard error
          this.toastr.error('Error uploading file');
        }
      } else {
        // Handle non-HTTP errors
        this.toastr.error('Error uploading file');
      }
    },
   
   
  });
}
 
 
MaterialUpdated: any = {}
 
UpdateMaterial(formData: FormData) {
  const url = 'https://localhost:7000/api/Material';
  const courseid = formData.get('courseid') as string | null;
  console.log(courseid);
 
  this.http.put(url, formData).subscribe({
      next: (response) => {
          console.log('Updatated successfully', response);
          const successMessage = typeof response === 'string' ? response : 'Updatated successfully';
          this.toastr.success(successMessage);
          const numericCourseId = courseid ? +courseid : null;
          if (numericCourseId !== null) {
            this.GetMaterialsByCourse(numericCourseId);
          } else {
            console.log('courseid is null');
          }
      },
      error: (error) => {
          console.error('Error updating data', error);
          this.handleErrorResponse(error);
      },
  });
}
 
 
private handleErrorResponse(error: any) {
  if (error instanceof HttpErrorResponse) {
    console.error('Server error:', error.status, error.statusText);
 
    try {
      const responseBody = JSON.parse(error.error);
      this.toastr.error(responseBody.error);
    } catch (jsonError) {
      this.toastr.error('Error updating file');
    }
  } else {
    this.toastr.error('Error updating file');
  }
}
 
 
DeleteMaterial(id : number , courseid: number)
{
  this.http.delete("https://localhost:7000/api/Material/" + id).subscribe(
    {
      next: (x)=>{
        console.log(id)
 
        this.toastr.success("Deleted Successfully!")
        this.GetMaterialsByCourse(courseid);
        },
      error: (e)=>{console.log(e);
        this.toastr.warning("Faild to delete the section.")
 
      }
    }
  )
}
courseMaterial : any = {}
GetMaterialsByCourse(courseId : number){
  this.http.get("https://localhost:7000/api/Material/GetMaterialsByCourse/"+ courseId).subscribe(
    {
      next: (result)=>{this.courseMaterial = result
      console.log(result);
      },
      error: (err)=>{console.log(err);
      }
    }
  )
}

//




//Eslam
attendanceStudentInfo : any={}
updateAttendanceStudentInfo:any={}
 
GetInfo(sectionId: number): Observable<any> {
  return this.http.get("https://localhost:7000/api/StdSection/GetStdSectionsInfoBySectionId/" + sectionId);
}
 
 
 
 
 
 
studentAttendance: any={}
 
 
insertAttendance(attendance: any[]): Observable<any> {  
  console.log('Sending attendance data:', attendance);
  return this.http.post("https://localhost:7000/api/Attendance",  attendance );
}
 
 
 
 
 
UpdateAttendance(attendance: any[]): Observable<any> {  
  console.log('Update attendance data:', attendance);
  return this.http.put("https://localhost:7000/api/Attendance",  attendance );
}
 
attendanceArray: any = []
GetAttendanceBySection(sectionId:number){
  this.http.get("https://localhost:7000/api/Attendance/GetAttendanceBySection/" +sectionId ).subscribe(
    {
      next: (result)=>{    
         this.attendanceArray=result
         console.log(result)
      },
      error: (err)=>{console.log(err);
      }
    }
  )
}
attendanceOfLearner: any[] = [];
 
GetAttendanceByUserAndSectionId(userId: number, sectionId: number): Observable<any> {
  return this.http.get("https://localhost:7000/api/Attendance/GetAttendanceByUserAndSection/" + userId + "/" + sectionId);
}
 
 
 
 
 
 
instructorCourses: any = []
 
 
GetCoursesByInstructorId(userId: number): Observable<any> {
  return this.http.get("https://localhost:7000/api/Course/GetCoursesByInstructorId/" + userId);
}
 
studentCount : number = 0
 
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
 
 
  courseCount : number=0
GetInstructorCourseCount(userId: number){
  return this.http.get("https://localhost:7000/api/Course/GetInstructorCourseCount/"+ userId).subscribe(
    (count: any)=>{
      this.courseCount = count;
    },
    (error) => {
      console.error('Error fetching instructor course count:', error);
    }
  )
 
}
 
 
sectionCount : number=0
GetInstructorSectionCount(userId: number){
  return this.http.get("https://localhost:7000/api/Section/GetInstructorSectionCount/"+ userId).subscribe(
    (count: any)=>{
      this.sectionCount = count;
    },
    (error) => {
      console.error('Error fetching instructor course count:', error);
    }
  )
 
}
 
 
 
 
 
  topStudents: any[] = [];
 
  GetTopStudents(userId:number): Observable<any>{
    return this.http.get("https://localhost:7000/api/Attendance/GetTopStudentsByAttendanceForEachSection/" + userId);
 
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
          const attendanceData = this.studentInfo.map((attendance: { firstname: any; lastname: any; dateOfAttendance: any; status: any; }) => [
            attendance.firstname,
            attendance.lastname,
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
 
 
  formatDate(dateString: string): string {
    // Use the Angular date pipe with a custom format
    return new Date(dateString).toLocaleDateString('en-US', {
      month: 'short',
      day: 'numeric',
      year: 'numeric',
    });
  }
   







}
