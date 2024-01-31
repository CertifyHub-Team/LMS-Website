import { Injectable, ViewChild } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams, HttpResponse } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from "ngx-spinner";
import { Observable, Observer, catchError, map, tap, throwError } from 'rxjs';

import { google } from 'googleapis';
import { AdminService } from './admin.service';

interface UserInfo {
  FIRSTNAME: string;
  USERNAME: string;
}
@Injectable({
  providedIn: 'root'
})
 

export class LearnerService {

  sections : any = []
  user : any = []
  upcomingAssessments: any = []
  AssessmentsForThisWeek: any =[]
 
  constructor(private http : HttpClient, private toastr: ToastrService, public _adminService: AdminService) { }
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

GetCourses(userId:number){
  this.http.get("https://localhost:7000/api/Program/GetStudentProgram/"+userId).subscribe(
    {
      next:(x)=>{this.myCourses = x;
      console.log(this.myCourses);
      },
      error: (err) => {console.log(err)
        this.toastr.error("Error")
      }
    }
  )
}


courseSection: any
GetSectionById(id : number, userId:number){
  this.http.get("https://localhost:7000/api/Section/GetSectionById/"+ id+"/"+userId).subscribe(
    {
      next: (result)=>{this.courseSection = result
      console.log(result);
      },
      error: (err)=>{console.log(err);
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

assessmentSection : any = {}
  GetAssessmentBySection(sectionId : number){
    
    this.http.get("https://localhost:7000/api/Assessment/GetAssessmentBysectionId/"+ sectionId).subscribe(
      {
        next: (result)=>{this.assessmentSection = result
        console.log(result);
        },
        error: (err)=>{console.log(err);
        }
      }
    )
  }





  
 grade : any = {}
GetUserGrade(userId: number, assessmentId: number): Observable<any> {
  return this.http.get<any>(`https://localhost:7000/api/Grade/GetUserGrade?userid=${userId}&assessmentid=${assessmentId}`)
    .pipe(
      tap((result: any) => {
        this.grade = result; 
        console.log(result);
      })
    );
}

  

presentcount : any 
  GetPresentCount(userId : number, sectionId :number){
    
    this.http.get("https://localhost:7000/api/Attendance/GetPresentAttendanceCountForUser/" + userId + "/"+sectionId ).subscribe(
      {
        next: (result)=>{this.presentcount = result
        console.log(result);
        },
        error: (err)=>{console.log(err);
        }
      }
    )
  }


   

absentcount : any 
GetAbsentCount(userId : number, sectionId :number){
  
  this.http.get("https://localhost:7000/api/Attendance/GetAbsentAttendanceCountForUser/" + userId + "/"+sectionId ).subscribe(
    {
      next: (result)=>{this.absentcount = result
      console.log(result);
      },
      error: (err)=>{console.log(err);
      }
    }
  )
}

private apiUrl = 'https://localhost:7000/api';
loggedInUserId: number | null = null;
GetUserCertificates(userId: number): Observable<any> {

  //https://localhost:7100/api/Certificate/GetUserCertificates/1

  return this.http.get(`${this.apiUrl}/Certificate/GetUserCertificates/${userId}`);
}






GetUserSections(id :   number){
   
  this.http.get("https://localhost:7000/api/StdSection/GetUserSection/"+id).subscribe(
    {
      next: (x) =>{this.sections=x},
      error:(err)=>{console.log(err)}
    }
  )
}

GetUser(id :   number){
 
  this.http.get("https://localhost:7000/api/Users/GetUserById/"+id).subscribe(
    {
      next: (x) =>{this.user=x},
      error:(err)=>{console.log(err)}
    }
  )
}

getUpcomingAssessments() {
  return this.http.get('https://localhost:7000/api/Assessment/GetUpcomingAssessments');
}


getWeekStartAndEnd() {
  const currentDate = new Date();
  const first = currentDate.getDate() - currentDate.getDay() + (currentDate.getDay() === 0 ? -6 : 1); // Adjust for Sunday
  const last = first + 6;

  const firstDateOfWeek = new Date(currentDate.setDate(first));
  firstDateOfWeek.setHours(0, 0, 0, 0); // start of the first day

  const lastDateOfWeek = new Date(currentDate.setDate(last));
  lastDateOfWeek.setHours(23, 59, 59, 999); // end of the last day

  console.log('filter assessment')
  console.log(firstDateOfWeek.toISOString())
  console.log(lastDateOfWeek.toISOString())
  return {
    startDate: firstDateOfWeek.toISOString(),
    endDate: lastDateOfWeek.toISOString()
  };
}


getAssessmentsForThisWeek() {
  const { startDate, endDate } = this.getWeekStartAndEnd();

  const params = new HttpParams()
    .set('startDate', startDate)
    .set('endDate', endDate);

  this.http.get('https://localhost:7000/api/Assessment/FilterAssessmentsByDate', { params })
  .subscribe(
    {
      next: (x) =>{this.AssessmentsForThisWeek=x},
      error:(err)=>{console.log(err)}
    }
  )
}


userSolutions: any = [];

GetUserSolution(userId: number, assessmentId: number): Promise<any> {
  return new Promise((resolve, reject) => {
    this.http.get(`https://localhost:7000/api/UserSolution?assessmentId=${assessmentId}&userId=${userId}`).subscribe(
      {
        next: (response) => {
          this.userSolutions = response;
          console.log(response);
          resolve(response);
        },
        error: (error) => {
          console.error('Error', error);
          reject(error);
        },
      }
    );
  });
}


sectionCourse :any = {}

async GetCourseBySectionId(sectionId:number) {
  try {
    const result = await this.http.get("https://localhost:7000/api/Section/GetCourseBySectionId/"+sectionId).toPromise();
    this.sectionCourse = result;
    console.log("Date is " + JSON.stringify(this.sectionCourse));
  } catch (error) {
    console.error("Error in GetCourseBySectionId:", error);
  }
}




  
certificateFile : Blob|null = null


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
  


  async sendCertificateAndEmail(certificateBytes: Uint8Array, recipientEmail: string): Promise<boolean> {
    try {
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
 
 
 
 
 
  GenerateCertificate(userId: number, sectionId: number): Observable<Uint8Array> {
    return this.http.get(`https://localhost:7000/api/StudentGrade/GetStudentGradeInfo?userId=${userId}&sectionId=${sectionId}`, { responseType: 'arraybuffer' })
      .pipe(
        map((response: any) => new Uint8Array(response)),
        catchError(error => {
          console.error("Error in GenerateCertificate:", error);
          return throwError(error);
        })
      );
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




///examm

ExamQuestion : any = []
 
GetExamQuestion(assessmentId: number) {
  this.http.get(`https://localhost:7000/api/Question/ListQuestionsByAssessment/${assessmentId}`).subscribe(
    (response: any) => {
      this.ExamQuestion = response;
      this.ExamQuestion.forEach((question: { questionid: number; }) => {
        this.GetExamQuestionAnswer(question.questionid, question);
      });
    },
    error => {
      console.error('Error', error);
    }
  );
}
 
GetExamQuestionAnswer(questionId: number, question: any) {
  this.http.get(`https://localhost:7000/api/Answer/ListAnswersByQuestion/${questionId}`).subscribe(
    (response: any) => {
      question.answers = response;
    },
    error => {
      console.error('Error', error);
    }
  );
}

testcourse :number =0
testItem : number = 0
getSection(courseid:number, itemId:number){
  this.testcourse = courseid
  this.testItem = itemId
}

ExamAnswer(answers:any){
  this.http.post('https://localhost:7000/api/UserSolution/AddUserExamSolution', answers).subscribe(
    (response: any) => {
     console.log(answers)
     this.toastr.success('your answers submitted successfully.')
    },
    error => {
      console.error('Error', error);
      this.toastr.error('error in submitting.')
    }
  );
}

userScore : number|null =  null
GetUserScore(assessmentId: number, userId: any) {
  this.http.get(`https://localhost:7000/api/UserSolution/GetUserScore/${assessmentId}/${userId}`).subscribe(
    (response: any) => {
      this.userScore = response;
    },
    error => {
      console.error('Error', error);
    }
  );
}


sectionNotifications : any = []
GetNotificationsBySection(sectionId : number){
  this.http.get("https://localhost:7000/api/Notification/GetNotificationsBySection/"+sectionId).subscribe(
    (response: any) => {
      this.sectionNotifications = response;
    },
    error => {
      console.error('Error', error);
    }
  );
}

///newwww
studentsCv : any = []
GetStudentsCv(){
  this.http.get("https://localhost:7000/api/Users/GetStudentsCvs").subscribe(
    (response: any) => {
      this.studentsCv = response;
    },
    error => {
      console.error('Error', error);
    }
  );
}
uploadFile(formData: FormData) {
  const url = 'https://localhost:7000/api/UserSolution';
 
  const attemptDate = formData.get('Attemptdate') as string | null;
  console.log(attemptDate);
 
  const jsonFormData: { [key: string]: string | null } = {
    Userid: formData.get('Userid') as string,
    Assessmentid: formData.get('Assessmentid') as string,
    attemptDate: attemptDate ? new Date(attemptDate).toISOString() : null,
    Questionid: formData.get('Questionid') as string,
    Answerid: formData.get('Answerid') as string,
  };
 
  console.log('jsonFormData:', jsonFormData);
 
  const finalFormData = new FormData();
  for (const key in jsonFormData) {
    if (jsonFormData.hasOwnProperty(key) && jsonFormData[key] !== null) {
      finalFormData.append(key, jsonFormData[key] as string);
    }
  }
 
  const file = formData.get('file') as File;
  if (file) {
    finalFormData.append('file', file, file.name);
  }
 
  console.log('finalFormData:', finalFormData);
 
  this.http.post(url, finalFormData).subscribe({
    next: (response) => {
      console.log('Data saved successfully', response);
      this.toastr.success('File uploaded successfully');
    },
    error: (error) => {
      console.error('Error saving data', error);
      this.toastr.error('Error uploading file');
    },
  });
}
 
 
UpdateSubmission(formData: FormData) {
  const url = 'https://localhost:7000/api/UserSolution';
 
  const attemptDate = formData.get('Attemptdate') as string | null;
  console.log(attemptDate);
 
  const jsonFormData: { [key: string]: string | null } = {
    solutionid: formData.get('Solutionid') as string,
    userid: formData.get('Userid') as string,
    usersolutiontext: formData.get('Usersolutiontext') as string,
    assessmentid: formData.get('Assessmentid') as string,
    attemptdate:formData.get('Attemptdate') as string,
    questionid: formData.get('Questionid') as string,
    answerid: formData.get('Answerid') as string,
  };
 
  console.log('jsonFormData:', jsonFormData);
 
  const finalFormData = new FormData();
  for (const key in jsonFormData) {
    if (jsonFormData.hasOwnProperty(key) && jsonFormData[key] !== null) {
      finalFormData.append(key, jsonFormData[key] as string);
    }
  }
 
  const file = formData.get('file') as File;
  if (file) {
    finalFormData.append('file', file, file.name);
  }
 
  console.log('finalFormData:', finalFormData);
 
  this.http.put(url, formData).subscribe({
    next: (response) => {
      console.log('Data saved successfully', response);
      this.toastr.success('File uploaded successfully');
    },
    error: (error) => {
      console.error('Error saving data', error);
      this.toastr.error('Error uploading file');
    },
  });
}
 
DeleteSubmistion(id : number)
{
  this.http.delete("https://localhost:7000/api/UserSolution/" + id).subscribe(
    {
      next: (x)=>{
        console.log(id)
 
        this.toastr.success("Deleted Successfully!")
        },
      error: (e)=>{console.log(e);
        this.toastr.warning("Faild to delete the submistion.")
 
      }
    }
  )
}

}


