import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { forkJoin, tap } from 'rxjs';
import { InstructorService } from 'src/app/instructor.service';

@Component({
  selector: 'app-exam-details',
  templateUrl: './exam-details.component.html',
  styleUrls: ['./exam-details.component.css']
})
export class ExamDetailsComponent implements OnInit{
  @ViewChild('createQuestion') CreateQuestionDialog : any
  @ViewChild('updateQuestion') UpdateQuestionDialog : any
  @ViewChild('deleteQuestion') DeleteQuestionDialog : any
  @ViewChild('createAnswer') CreateAnswerDialog : any
  @ViewChild('updateAnswer') UpdateAnswerDialog : any
  @ViewChild('deleteAnswer') DeleteAnswerDialog : any
  constructor(public _instructorService:InstructorService, private router: ActivatedRoute , private dialog:MatDialog, private toastr: ToastrService){}

  userId = 0
  sectionId = 0
  assessmentId = 0
  ngOnInit() {
    this.router.paramMap.subscribe(params => {

      this.sectionId = Number(params.get('sectionId'));
      this.assessmentId = Number(params.get('assessmentId'));
      //new
      this._instructorService.GetSectionAssessments(this.sectionId)
  this.GetAssessmentDetails()
  this._instructorService.GetQuestionsAndAnswers(this.assessmentId)
console.log(this._instructorService.assessmentQuestions)
console.log("result", this._instructorService.assessmentQuestions[1].questions[0].answers === null && this._instructorService.assessmentQuestions[1].questions[0].answers.length > 0 ? 1 : 0);


  
    });

}







assessment : any = {}
GetAssessmentDetails() {
  const assessmentIdToFind = this.assessmentId;
  this.assessment = this._instructorService.sectionAssessments.find(
    (assessment: any) => assessment.assessmentid === assessmentIdToFind
  );

  if (!this.assessment) {
    // Handle the case where the assessment with the specified ID is not found
    console.error(`Assessment with ID ${assessmentIdToFind} not found`);
    // You might want to show a message or navigate to an error page
  }
}


CreateQuestionForm: FormGroup = new FormGroup({
  assessmentid: new FormControl(this.assessmentId, [Validators.required]),
  marks: new FormControl('', [Validators.required]),
  questiontext: new FormControl('', [Validators.required])
});





OpenCreateDialog()
{
    this.dialog.open(this.CreateQuestionDialog , {
      width: '500px',
      height: '380px',
      enterAnimationDuration: 1000,
      exitAnimationDuration: 1000
     
    })
}

CloseCreateDialog(){
  this.dialog.closeAll( )

}

async CreateQuestion() {
  console.log(this.CreateQuestionForm.value)
  await this._instructorService.CreateQuestion(this.CreateQuestionForm.value, this.assessmentId);
  await this.CloseCreateDialog()
  this._instructorService.GetQuestionsAndAnswers(this.assessmentId)
}



UpdateQuestionForm: FormGroup = new FormGroup({
  questionid:new FormControl('', [Validators.required]),
  assessmentid: new FormControl(this.assessmentId, [Validators.required]),
  marks: new FormControl('', [Validators.required]),
  questiontext: new FormControl('', [Validators.required])
});


async UpdateQuestiont(){

  await this._instructorService.UpdateQuestion(this.UpdateQuestionForm.value, this.assessmentId);
  console.log(this.UpdateQuestionForm)
  await this.CloseCreateDialog()

//this._adminService.GetSectionsByCourse(this.courseId);


}

updateAssessmentType : string = ''
OpenUpdateDialog(c : any)
  {
    
    console.log("Update",c);
   this.updateAssessmentType = c.assessmenttype
    // this.UpdateQuestionForm.patchValue(c)
    this.UpdateQuestionForm.controls['assessmentid'].setValue(this.assessmentId);
    this.UpdateQuestionForm.controls['questionid'].setValue(c.questionid);
    this.UpdateQuestionForm.controls['marks'].setValue(c.marks);
    this.UpdateQuestionForm.controls['questiontext'].setValue(c.questiontext);
    console.log("Updatedddddd",this.UpdateQuestionForm.value);
this.dialog.open(this.UpdateQuestionDialog ,{
  width: '500px',
  height: '380px',
  enterAnimationDuration: 1000,
  exitAnimationDuration: 1000
    
})
  }



CloseUpdateDialog(){
this.dialog.closeAll( )

}



OpenConfirmDeleteDialog(id : any)
{
var dialog = this.dialog.open(this.DeleteQuestionDialog)
dialog.afterClosed().subscribe(
 
  (result)=>{
    if(result == 'yes')
    this.DeleteQuestion(id);}
    )
  }
 



CloseDeleteDialog(){
this.dialog.closeAll( )

}


async DeleteQuestion(questionId: number){

   await this._instructorService.DeleteQuestion(questionId, this.assessmentId);



   await this.CloseDeleteDialog()


}


CreateAnswerForm: FormGroup = new FormGroup({
  questionid: new FormControl(this.assessmentId, [Validators.required]),
  iscorrect: new FormControl(0, [Validators.required]),
  answertext: new FormControl('', [Validators.required])
});




OpenCreateAnswerDialog(questionId: number) {
  this.CreateAnswerForm.controls['questionid'].setValue(questionId);

  // Open the dialog
  this.dialog.open(this.CreateAnswerDialog , {
    width: '500px',
    height: '280px',
    enterAnimationDuration: 1000,
    exitAnimationDuration: 1000
  });
}


async CreateAnswer() {
  console.log(this.CreateAnswerForm.value)
  if(this.CreateAnswerForm.value.iscorrect === true){
    this.CreateAnswerForm.controls['iscorrect'].setValue("true");
  }
  else{
    this.CreateAnswerForm.controls['iscorrect'].setValue("false");
  }
  console.log(this.CreateAnswerForm.value)
 await this._instructorService.CreateAnswer(this.CreateAnswerForm.value, this.assessmentId);
  await this.CloseCreateDialog()
  this._instructorService.GetQuestionsAndAnswers(this.assessmentId)
}







OpenConfirmDeleteAnswerDialog(id : any)
{
var dialog = this.dialog.open(this.DeleteAnswerDialog)
dialog.afterClosed().subscribe(
 
  (result)=>{
    if(result == 'yes')
    this.DeleteAnswer(id);}
    )
  }
 




async DeleteAnswer(answerId: number){

   await this._instructorService.DeleteAnswer(answerId, this.assessmentId);



   await this.CloseDeleteDialog()


}







UpdateAnswerForm: FormGroup = new FormGroup({
  questionid: new FormControl(this.assessmentId, [Validators.required]),
  answerid: new FormControl(this.assessmentId, [Validators.required]),
  iscorrect: new FormControl("", [Validators.required]),
  answertext: new FormControl('', [Validators.required])
});

async UpdateAnswer(){

  await this._instructorService.UpdateAnswer(this.UpdateAnswerForm.value, this.assessmentId);
  console.log(this.UpdateAnswerForm)
  await this.CloseCreateDialog()

//this._adminService.GetSectionsByCourse(this.courseId);


}


async OpenUpdateAnswerDialog(c : any, questionid:number)
  {
    
    console.log("Update",c);

    await this.UpdateAnswerForm.controls['answerid'].setValue(c.answerid);
    await this.UpdateAnswerForm.controls['questionid'].setValue(questionid);
    await this.UpdateAnswerForm.controls['answertext'].setValue(c.answertext);
    if(this.CreateAnswerForm.value.iscorrect === true){
      await this.UpdateAnswerForm.controls['iscorrect'].setValue("true");
    }
    else{
      await this.UpdateAnswerForm.controls['iscorrect'].setValue("false");
    }
    console.log("Updatedddddd",this.UpdateAnswerForm.value);
this.dialog.open(this.UpdateAnswerDialog ,{
  width: '500px',
  height: '280px',
  enterAnimationDuration: 1000,
  exitAnimationDuration: 1000
    
})
  }



}
