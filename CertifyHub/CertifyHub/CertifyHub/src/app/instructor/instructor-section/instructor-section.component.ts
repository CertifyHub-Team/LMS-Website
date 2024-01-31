import { DatePipe } from '@angular/common';
import { HttpHeaders } from '@angular/common/http';
import { ThisReceiver } from '@angular/compiler';
import { Component, ViewChild } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { InstructorService } from 'src/app/instructor.service';

@Component({
  selector: 'app-instructor-section',
  templateUrl: './instructor-section.component.html',
  styleUrls: ['./instructor-section.component.css']
})
export class InstructorSectionComponent {
  sectionId : number = 0
  private headers = new HttpHeaders({
    'Content-Type': 'application/json',
  });
  @ViewChild('createNotification') CreateNotificationDialog : any
  @ViewChild('createAssignment') CreateAssignmentDialog : any
  @ViewChild('updateAssignment') UpdateAssignmentDialog : any
  @ViewChild('deleteAssignment') DeleteAssignmentDialog : any
  @ViewChild('AddMaterial') AddMaterialDialog : any
  @ViewChild('updateMaterial') UpdateMaterialDialog : any
  @ViewChild('deleteMaterial') DeleteMaterialDialog : any
  @ViewChild('details') AttendanceDetailDialog:any
  constructor(public _instructorService : InstructorService, private router : ActivatedRoute, private toastr: ToastrService, private dialog: MatDialog, private route : Router ){
    this.endDate.setDate(this.today.getDate() + 7);
  }
  userId = 0
  async ngOnInit() {
    this.router.paramMap.subscribe(async params => {
      this.sectionId = Number(params.get('id'));
      console.log(this.sectionId);
     
      // Call sectionInfo and wait for it to complete
      await this._instructorService.sectionInfo(this.sectionId);
 
      // Now, course ID should be properly set
      console.log(this._instructorService.courseId);
 
      // Call other methods that depend on the course ID
      this._instructorService.GetSectionAssessments(this.sectionId);
      console.log(this._instructorService.sectionInf);
      // this._instructorService.GetStudentsByCSection(this.sectionId);
      this._instructorService.GetStdSectionInfoById(this.sectionId);
    });
    this._instructorService.GetInfo(this.sectionId).subscribe(
      (studentsInfo: any) => {
        this._instructorService.studentInfo = studentsInfo;
        console.log('Student Information:', studentsInfo);
 
        // Extract student IDs
        const studentIds = studentsInfo.map((student: any) => student.userid);
        console.log('Student IDs:', studentIds);
 
        // Now you can use studentIds array as needed
      },
      (error) => {
        console.log('Error fetching student information:', error);
      }
    );
 
 
    this._instructorService.GetAttendanceBySection(this.sectionId)
 
  }
 

today = new Date();
endDate = new Date();


CreateNotificationForm: FormGroup = new FormGroup({
  sectionid: new FormControl(this.sectionId, [Validators.required]),
  notificationtext: new FormControl('', [Validators.required]),
  notificationenddate: new FormControl(this.today, [Validators.required]),
  notificationstartdate: new FormControl(this.endDate, [Validators.required])
});





OpenCreateDialog()
{
    this.dialog.open(this.CreateNotificationDialog , {
      width: '450px',
      height: '240px',
      enterAnimationDuration: 1000,
      exitAnimationDuration: 1000
     
    })
}

CloseCreateDialog(){
  this.dialog.closeAll( )

}



async CreateNotification() {
  console.log(this.CreateNotificationForm.value)
  await this._instructorService.CreateNotification(this.CreateNotificationForm.value);
  await this.CloseCreateDialog()
}



CreateAssignmentForm: FormGroup = new FormGroup({
  title: new FormControl('', [Validators.required]),
  description: new FormControl('', [Validators.required]),
  assessmenttype: new FormControl('' , [Validators.required]),
  startdate: new FormControl('', [Validators.required]),
  enddate: new FormControl('', [Validators.required]),
  starttime: new FormControl(''),
  endtime: new FormControl(''),
  assessmentscore: new FormControl('', [Validators.required]),
  attachfile: new FormControl(''),
  sectionid: new FormControl('', [Validators.required]),  
});

assessmentType : string = ''
OpenCreateAssignmentDialog(assessmemtTypee:string)
{
 
  this.assessmentType = assessmemtTypee
    this.dialog.open(this.CreateAssignmentDialog , {
      width: '440px',
      height: '580px',
      enterAnimationDuration: 1000,
      exitAnimationDuration: 1000
     
    })
    this.CreateAssignmentForm.get('startdate')?.valueChanges.subscribe(
      (startDate) => {
        this.CreateAssignmentForm.get('starttime')?.setValue(startDate);
      }
    );
  
    this.CreateAssignmentForm.get('enddate')?.valueChanges.subscribe(
      (endDate) => {
        this.CreateAssignmentForm.get('endtime')?.setValue(endDate);
      }
    );

    this.CreateAssignmentForm.get('assessmenttype')?.setValue(this.assessmentType);

    console.log("Assess", this.CreateAssignmentForm.value)
}

CloseCreateAssignmentDialog(){
  this.dialog.closeAll( )

}

selectedFileName: string | null = null;

UploadFile(event: Event) {
  const element = event.currentTarget as HTMLInputElement;
  const file = element.files?.[0];

  if (file) {
    this.selectedFileName = file.name;
    // Update the form control with the actual file object
    this.CreateAssignmentForm.patchValue({
      attachfile: file
    });
    // Call this method if you are using Angular 8+
    this.CreateAssignmentForm.get('attachfile')?.updateValueAndValidity();
  }
}


async CreateAssignment() {
  const formData = new FormData();
  const fileControl = this.CreateAssignmentForm.get('attachfile');
  const file = fileControl?.value;

  // Append the file to the formData
  if (file && file instanceof File) {
    formData.append('attachfile', file, file.name);
  }

  // Iterate over form controls (excluding the file input)
  Object.keys(this.CreateAssignmentForm.controls).forEach(key => {
    if (key !== 'attachfile') {
      const control = this.CreateAssignmentForm.get(key);
      formData.append(key, control?.value);
    }
  });

  await this._instructorService.CreateAssessment(formData, this.sectionId);
  await this.CloseCreateDialog();
}


UpdateAssignmentForm: FormGroup = new FormGroup({
  assessmentid : new FormControl('', [Validators.required]),
  title: new FormControl('', [Validators.required]),
  description: new FormControl('', [Validators.required]),
  assessmenttype: new FormControl(this.assessmentType, [Validators.required]),
  startdate: new FormControl('', [Validators.required]),
  enddate: new FormControl('', [Validators.required]),
  starttime: new FormControl(''),
  endtime: new FormControl(''),
  assessmentscore: new FormControl('', [Validators.required]),
  attachfile: new FormControl(''),
  sectionid: new FormControl('', [Validators.required]),  
});


async UpdateAssignment(){

  await this._instructorService.UpdateSAssessment(this.UpdateAssignmentForm.value, this.sectionId);
  // console.log(section)
  await this.CloseCreateAssignmentDialog()

//this._adminService.GetSectionsByCourse(this.courseId);


}

updateAssessmentType : string = ''
OpenUpdateDialog(c : any)
  {
    
    console.log(c);
   this.updateAssessmentType = c.assessmenttype
    this.UpdateAssignmentForm.patchValue(c)
this.dialog.open(this.UpdateAssignmentDialog ,{
  width: '450px',
  height: '580px',
  enterAnimationDuration: 1000,
  exitAnimationDuration: 1000
    
})
  }



CloseUpdateDialog(){
this.dialog.closeAll( )

}

OpenConfirmDeleteDialog(id : any)
{
var dialog = this.dialog.open(this.DeleteAssignmentDialog)
dialog.afterClosed().subscribe(
 
  (result)=>{
    if(result == 'yes')
    this.DeleteAssignment(id);}
    )
  }
 



CloseDeleteDialog(){
this.dialog.closeAll( )

}


async DeleteAssignment(assessmentId: number){

   await this._instructorService.DeleteAssessment(assessmentId, this.sectionId);



   await this.CloseDeleteDialog()

 //this._adminService.GetSectionsByCourse(this.courseId);
 //await  window.location.reload();

}

GoToStudentsSolutions(assessmentId:number){
  this.route.navigate(['instructor/StudentsSubmissions', this.sectionId, assessmentId]).then(() => {
   
  });
}



getAssessmentsScore(): number | undefined {
  const totalScore = this._instructorService.sectionAssessments
    .reduce((sum: number | undefined, assessment: any) => {
      if (assessment.assessmentscore !== undefined) {
        return (sum || 0) + assessment.assessmentscore;
      } else {
        // Handle the case where assessmentscore is undefined
        console.error(`Assessment score is undefined for assessment with ID ${assessment.assessmentid}`);
        return sum;
      }
    }, 0);

  return totalScore !== undefined ? totalScore : undefined;
}


ExamDetails(assessmentId:number){
  this.route.navigate(['instructor/ExamDetails', this.sectionId, assessmentId]).then(() => {
   
  });
}
StudentsGrades(assessmentId:number){
  this.route.navigate(['instructor/StudentsGrades', this.sectionId, assessmentId]).then(() => {
   
  });
}

userSol = ''
getLocalFilePath(): string {
  return `assets/uploads/${this.userSol}`;
}

//shatha
selectedMaterialFileName: string | null = null;
 
UploadMaterialFile(event: Event) {
  const element = event.currentTarget as HTMLInputElement;
  const file = element.files?.[0];
 
  if (file) {
    this.selectedMaterialFileName = file.name;
    // Update the form control with the actual file object
    this.AddMaterialForm.patchValue({
      materialpath: file
    });
    // Call this method if you are using Angular 8+
    this.AddMaterialForm.get('materialpath')?.updateValueAndValidity();
  }
}
 
 
editMaterialFile(event: Event) {
  const element = event.currentTarget as HTMLInputElement;
  const file = element.files?.[0];
 
  if (file) {
    this.selectedMaterialFileName = file.name;
    // Update the form control with the actual file object
    this.updateMaterialForm.patchValue({
      materialpath: file
    });
    // Call this method if you are using Angular 8+
    this.updateMaterialForm.get('materialpath')?.updateValueAndValidity();
  }
}
 
 
async AddFormMaterial() {
  // Ensure sectionInfo has completed and course ID is set
  await this._instructorService.sectionInfo(this.sectionId);
 
  // Log the courseId before making the HTTP request
  console.log('Course ID before HTTP request:', this._instructorService.courseId);
 
  const formData = new FormData();
  const fileControl = this.AddMaterialForm.get('materialpath');
  const file = fileControl?.value;
 
  // Append the file to the formData
  if (file && file instanceof File) {
    formData.append('materialpath', file, file.name);
  }
 
  // Append other form values directly to formData
  formData.append('materialname', this.AddMaterialForm.get('materialname')?.value);
  formData.append('courseid', this._instructorService.courseId.toString());
  formData.append('videourl', this.AddMaterialForm.get('videourl')?.value);
 
  console.log('Material Data:', formData);
 
  await this._instructorService.AddMaterial(formData);
  await this.CloseCreateDialog();
}
 
 
 
AddMaterialForm: FormGroup = new FormGroup({
  materialname: new FormControl('', [Validators.required]),
  materialpath: new FormControl('', [Validators.required]),
  courseid: new FormControl(this._instructorService.courseId),
  videourl: new FormControl(''),
 
});
 
async OpenAddMaterialDialog()
{
    await this.dialog.open(this.AddMaterialDialog , {
      width: '450px',
      height: '450px',
      enterAnimationDuration: 1000,
      exitAnimationDuration: 1000
     
    })
}
 
CloseAddMaterialDialog(){
  this.dialog.closeAll( )
 
}
 
updateMaterialForm: FormGroup = new FormGroup({
  materialid:new FormControl('',[Validators.required]),
  materialname: new FormControl('',[Validators.required]),
  materialpath: new FormControl('',[Validators.required]),
  courseid: new FormControl('',[Validators.required]),
  videourl: new FormControl('',[Validators.required]),
 
});
 
 
OpenUpdateMaterialDialog(c: any) {
  console.log(c);
 
  this.selectedMaterialFileName = c.materialpath ? c.materialpath.name : null;
 
  this.updateMaterialForm.patchValue({
    materialid: c.materialid,
    materialname: c.materialname,
    materialpath: c.materialpath,
    courseid: c.courseid,
    videourl: c.videourl,
  });
 
  this.dialog.open(this.UpdateMaterialDialog, {
    width: '450px',
    height: '580px',
    enterAnimationDuration: 1000,
    exitAnimationDuration: 1000,
  });
}
 
 
 
 
CloseUpdateMaterialDialog(){
this.dialog.closeAll( )
 
}
 
async UpdateMaterial() {
  console.log('UpdateMaterial called');
 
  const formData = new FormData();
  formData.append('materialid', this.updateMaterialForm.value.materialid);
  formData.append('materialname', this.updateMaterialForm.value.materialname);
  formData.append('courseid', this.updateMaterialForm.value.courseid);
  formData.append('videourl', this.updateMaterialForm.value.videourl);
 
  // Append file if it's available
  if (this.selectedMaterialFileName) {
      formData.append('materialpath', this.updateMaterialForm.value.materialpath);
  }
 
  try {
      await this._instructorService.UpdateMaterial(formData);
      await this.CloseUpdateMaterialDialog();
  } catch (error) {
      console.error('Error in UpdateMaterial:', error);
  }
}
 
 
OpenConfirmDeleteMaterialDialog(id : number , courseid: number)
{
var dialog = this.dialog.open(this.DeleteMaterialDialog)
dialog.afterClosed().subscribe(
 
  (result)=>{
    if(result == 'yes')
    this.DeleteMaterial(id,courseid);
  else
  this.DeleteMaterialDialog();
  }
    )
  }
 
 
 
 
CloseDeleteMaterialDialog(){
this.dialog.closeAll( )
 
}
 
 
async DeleteMaterial(id: number , courseid : number){
 
   await this._instructorService.DeleteMaterial(id , courseid);
 console.log(id)
 
 
   await this.CloseDeleteDialog()
 
 
}
 


//
 
getStudentImagePath(student: any): string {
  return student.imagepath || this.getDefaultImagePath(student.firstname, student.lastname);
}
 
getAltText(student: any): string {
  return student.imagepath ? `${student.firstname} ${student.lastname}` : 'Student Image';
}
getDefaultImagePath(firstname: string, lastname: string): string {
  if (!firstname || !lastname) {
    return 'https://via.placeholder.com/80/959acc/ffffff?text=NA';
  }
 
  const firstLetterFirstName = firstname.charAt(0).toUpperCase();
  const firstLetterLastName = lastname.charAt(0).toUpperCase();
  const defaultImageUrl = `https://via.placeholder.com/80/959acc/ffffff?text=${firstLetterFirstName}${firstLetterLastName}`;
 
  return defaultImageUrl;
}
 
 
 
showAttendanceTable = false;
 
toggleTables() {
  this.showAttendanceTable = !this.showAttendanceTable;
}
 
 
submitAttendance() {
  let dateOfAttendance = new Date().toISOString();
 
  const attendanceDataArray = this._instructorService.studentInfo.map((student: any) => {
    const attendanceStatus = student.attendanceStatus ? 'Absent' : 'Present';
    return {
      sectionid: this.sectionId,
      dateofattendance: dateOfAttendance,
      userid: student.userid,
      status: attendanceStatus
    };
  });
 
  // Send the request with the array of attendance data
  this._instructorService.insertAttendance(attendanceDataArray).subscribe(
    async (response: any) => {
      console.log('Success Response:', response);
      this.toastr.success('Success recording attendance.');
 
      // Update the attendance data after successful insertion
      await this._instructorService.GetAttendanceBySection(this.sectionId);
     
      // Open the dialog with the updated attendance information
      this.showAttendanceTable= false;
    },
    (error) => {
      console.error('Error:', error);
      this.toastr.error('Error recording attendance for students.');
    }
  );
 
  this.showAttendanceTable= false;
 
}
 
 
 
async UpdateAttendance() {
  const dateOfAttendance = new Date().toISOString();
 
  await this._instructorService.GetAttendanceBySection(this.sectionId);
    setTimeout(() => {
    console.log('Existing Attendance:', this._instructorService.attendanceArray);
 
    const updatedAttendanceArray = this._instructorService.attendanceArray.map((existingAttendance: any) => {
    const student = this._instructorService.studentInfo.find((s: any) => s.userid === existingAttendance.userid);
 
    if (student) {
      console.log('Updating Student:', student);
        existingAttendance.status = student.attendanceStatus ? 'Absent' : 'Present';
    }    
 
    return existingAttendance;
    });
 
      console.log('Updated Attendance:', updatedAttendanceArray);
 
      // Send the updated attendance data to the server
      this._instructorService.UpdateAttendance(updatedAttendanceArray).subscribe(
        (response: any) => {
          console.log('Success Response:', response);
          this.toastr.success("Success updating attendance.");
        },
        (error) => {
          console.error('Error:', error);
          this.toastr.error('Error updating attendance for students.');
        });
 
      this.showUpdateAttendanceTable = false;
    }, 1000);
  }
 
  showUpdateAttendanceTable = false;
 
  toggleUpdateTables() {
    this.showUpdateAttendanceTable = !this.showUpdateAttendanceTable;
  }
 
  OpenDetailDialog(userid: number) {
    console.log('Opening detail dialog for student ID:', userid);
 
    if (userid && this.sectionId) {
      const sectionId = this.sectionId;
 
      // Fetch attendance details for the selected student
      this._instructorService.GetAttendanceByUserAndSectionId(userid, sectionId).subscribe(
        (result) => {
          this._instructorService.attendanceOfLearner = result;
          console.log('Fetched attendance details:', this._instructorService.attendanceOfLearner);
 
          // Open the dialog with the fetched attendance details
          const dialogRef = this.dialog.open(this.AttendanceDetailDialog, {
            width: '350px',
            height: 'auto',
            enterAnimationDuration: 1000,
            exitAnimationDuration: 1000,
            data: { attendanceDetails: this._instructorService.attendanceOfLearner } // Pass data to the dialog
          });
 
          // You can subscribe to dialog events if needed
          dialogRef.afterClosed().subscribe((result) => {
            console.log('The dialog was closed', result);
          });
        },
        (error:any) => {
          console.log('Error fetching attendance details:', error);
        }
      );
    } else {
      console.error('Invalid studentId or sectionId. Unable to fetch attendance details.');
    }
  }
 
 
  CloseDetailDialog(){
    this.dialog.closeAll( )
 
  }
 
 
 
  onGetSectionReport() {
    // Assuming you have the section ID, replace 1 with the actual section ID
    this._instructorService.GetStudentsByCSection(this.sectionId);
    this._instructorService.GetStdSectionInfoById(this.sectionId);
    this._instructorService.generateSectionPDF();
  }
 

}