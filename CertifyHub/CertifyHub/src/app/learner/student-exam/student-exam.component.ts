import { Component, OnDestroy } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription, timer } from 'rxjs';
import { AdminService } from 'src/app/admin.service';
import { LearnerService } from 'src/app/learner.service';
class UserSolution {
  userid: number;
  assessmentid: number;
  attemptdate: Date;
  questionid: number | null;
  answerid: number | null;

  constructor(userid: number, assessmentid: number, attemptdate: Date, questionid: number | null, answerid: number | null) {
    this.userid = userid;
    this.assessmentid = assessmentid;
    this.attemptdate = attemptdate;
    this.questionid = questionid;
    this.answerid = answerid;
  }
}
@Component({
  selector: 'app-student-exam',
  templateUrl: './student-exam.component.html',
  styleUrls: ['./student-exam.component.css']
})
export class StudentExamComponent implements OnDestroy {
  assessmentId: number = 0;
  title:string =''
  hours: number = 1;
  minutes: number = 0;
  seconds: number = 0;
  timerSubscription!: Subscription;
  constructor(public _learnerService: LearnerService, private toastr: ToastrService, private route: ActivatedRoute, private router:Router, public _adminService : AdminService) {}
  userId = 0
  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.assessmentId = Number(params.get('assessmentid'));
      this.title = String(params.get('examTitle'));
      this._learnerService.GetExamQuestion(this.assessmentId);

      this.timerSubscription = timer(0, 1000).subscribe(() => {
        let totalSeconds = this.hours * 3600 + this.minutes * 60 + this.seconds;

        if (totalSeconds > 0) {
          totalSeconds--;
          this.hours = Math.floor(totalSeconds / 3600);
          this.minutes = Math.floor((totalSeconds % 3600) / 60);
          this.seconds = totalSeconds % 60;
        } else {
          this.timerSubscription.unsubscribe();
          this.SaveAnswers();  // Move SaveAnswers() here
        }
      });
    });

    const userString: string | null = localStorage.getItem('user');

    if (userString) {
      const user = JSON.parse(userString);
      this.userId = user.UserId;
    }
  }
    
 
ngOnDestroy() {
  if (this.timerSubscription) {
    this.timerSubscription.unsubscribe();
    
  }
}

getQuestionAnswer(id : any)
{
  this._learnerService.GetExamQuestion(id)
}
 
 
AddUserSolution : FormGroup = new FormGroup(
  {
    Assessmentid : new FormControl('', [Validators.required]),
    Userid: new FormControl('' , [Validators.required]),
    Usersolutiontext : new FormControl('' ),
    Attemptdate : new FormControl('', [Validators.required]),
    Questionid: new FormControl(''),
    Answerid: new FormControl('', [Validators.required]),
  }
)
 

// userSolution:any = {

//   userid : 1,
//   assessmentid : this.assessmentId,
//   attemptdate: new Date(),
//   questionid:'',
//   answerid:''
// }




examSolution: UserSolution[] = [];

answer: any = {}

AddSolution(event: any, questionId: number) {
  // Check if the questionid already exists in the examSolution array
  const index = this.examSolution.findIndex(solution => solution.questionid === questionId);

  // If the questionid exists, remove all objects with the same questionid except the last one
  if (index !== -1) {
    this.examSolution = this.examSolution.filter(solution => solution.questionid !== questionId);
  }

  // Create a new UserSolution instance
  const userSolution = new UserSolution(this.userId, this.assessmentId, new Date(), questionId, event.target.value);

  // Add the userSolution to the examSolution array
  this.examSolution.push(userSolution);

  console.log(this.examSolution);
}

SaveAnswers() {
  // Check if the examSolution array is empty
  if (this.examSolution.length === 0) {
    // If examSolution is empty, add a default data point
    const defaultSolution = new UserSolution(this.userId, this.assessmentId, new Date(), null, null);
    this.examSolution.push(defaultSolution);
  } else {
    // Iterate through each user solution in the examSolution array
    this.examSolution.forEach(userSolution => {
      // Check if the user has chosen an answer (event.target.value is not null)
      if (userSolution.answerid !== null) {
        // Find the corresponding question in the examQuestion array
        const question = this._learnerService.ExamQuestion.find((q: UserSolution) => q.questionid === userSolution.questionid);


        // Update the user's answer in the examQuestion array
        if (question) {
          question.userAnswer = userSolution.answerid;
        }
      }
    });
  }

  // Send the updated examQuestion array to the server
  this._learnerService.ExamAnswer(this.examSolution);

  // Navigate to the desired route
  this.router.navigate([`learner/CourseSection/${this._learnerService.testcourse}/${this._learnerService.testItem}`]).then(() => {});
}


}
