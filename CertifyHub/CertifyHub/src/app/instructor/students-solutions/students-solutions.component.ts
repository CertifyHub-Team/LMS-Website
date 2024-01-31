import { Component, ViewChild } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { InstructorService } from 'src/app/instructor.service';
class StudentGrade {
  userid: number|null;
  assessmantid: number|null;
  studentgrade: number|null; 

  constructor(userid: number, assessmentid: number, studentgrade: number) {
    this.userid = userid;
    this.assessmantid = assessmentid;
    this.studentgrade = studentgrade;
  }
}

@Component({
  selector: 'app-students-solutions',
  templateUrl: './students-solutions.component.html',
  styleUrls: ['./students-solutions.component.css']
})
export class StudentsSolutionsComponent {
@ViewChild('updateGrade') UpdateGradeDialog : any
constructor(public _instructorService:InstructorService, private router: ActivatedRoute , private dialog:MatDialog, private toastr: ToastrService){}

  userId = 0
  sectionId = 0
  assessmentId = 0
  ngOnInit() {
    this.router.paramMap.subscribe(params => {

      this.sectionId = Number(params.get('sectionId'));
      this.assessmentId = Number(params.get('assessmentId'));
      //new
      this._instructorService.GetStudentsSolutionBySection(this.sectionId, this.assessmentId)
      this._instructorService.GetSectionAssessments(this.sectionId)
  
    });
}

studentsGrades: StudentGrade[] = [];

section: StudentGrade = {
  userid: 0,
  assessmantid: this.assessmentId,
  studentgrade: 0 
};
 
onGradeInputChange(section: any)  {
  const existingGradeIndex = this.studentsGrades.findIndex(grade =>
    grade.userid === section.userid
  );

  // If the grade exists, update it; otherwise, add a new grade
  if (existingGradeIndex !== -1) {
    this.studentsGrades[existingGradeIndex].studentgrade = section.studentgrade
  } else {
    const newGrade = new StudentGrade(section.userid, this.assessmentId, section.studentgrade);
    this.studentsGrades.push(newGrade);
  }

  // Clear the input field after saving the grade
  section.studentgrade = null;
}






// saveAllGrades() {
//   // Call the service method to save all grades
//   this._instructorService.AddStudentsGrades(this.studentsGrades);

//   // Optional: Clear the array if needed
//   this.studentsGrades = [];
// }

saveAllGrades() {
  const assessmentScore = this.getAssessmentScoreById(this.assessmentId)
  if (assessmentScore != null) {
    const allGradesValid = this.studentsGrades.every((student:any) => student.studentgrade <= assessmentScore);

    if (allGradesValid) {
      this._instructorService.AddStudentsGrades(this.studentsGrades, this.sectionId, this.assessmentId);

      this.studentsGrades = [];
    } else {
      this.toastr.error('Not all student grades are valid for the assessment score.');
    }
  } else {
    console.error('Assessment score not found.');
  }
}



gradeFormControl  = new FormControl(0, [Validators.required]);

newGrade = new StudentGrade(0, 0, 0);

userGrade : number = 0

async OpenUpdateDialog(userid: number) {
  try {
    await this._instructorService.GetUserGrade(this.assessmentId, userid)
    //this.gradeFormControl = 

      await console.log('Received grade:', this._instructorService.userGrade);

      // Set other properties as needed
      this.newGrade.userid = userid;
      this.newGrade.assessmantid = this.assessmentId;

      // Open the dialog
      this.dialog.open(this.UpdateGradeDialog, {
        width: '280px',
        height: '180px'
      });
   
      console.error('Error: Received null grade.');
      // Handle the case where the grade is null, if needed
    
  } catch (error) {
    console.error('Error in OpenUpdateDialog:', error);
    // Handle error if needed
  }
}


UpdateGrade()
{
 
  this.newGrade.studentgrade = this.gradeFormControl.value
  console.log(this.newGrade)
  const assessmentScore = this.getAssessmentScoreById(this.assessmentId)
  if(this.newGrade.studentgrade != null && assessmentScore ) {
    if(this.newGrade.studentgrade <= assessmentScore)
    this._instructorService.UpdateGrade(this.newGrade, this.sectionId, this.assessmentId)
  else{
this.toastr.error("Student Grade Must be less or equale assessment score!")
  }
  }
 
}

async GetUserGrade(userId: number) {
  try {
    const userGradeResponse = await this._instructorService.GetUserGrade(this.assessmentId, userId);
    return true
    
  } catch (error) {
    console.error(error);
    return false
  }
}

getAssessmentScoreById(assessmentId: number): number | undefined {
  const foundAssessment = this._instructorService.sectionAssessments.find((assessment:any) => assessment.assessmentid === this.assessmentId);

  return foundAssessment ? foundAssessment.assessmentscore : undefined;
}
userSol = ''
getLocalFilePath(): string {
  return `assets/uploads/${this.userSol}`;
}
}
