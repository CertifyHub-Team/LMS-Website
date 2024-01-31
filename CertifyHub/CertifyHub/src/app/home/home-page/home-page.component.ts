import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LearnerService } from 'src/app/learner.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent {
constructor(public _learnerService:LearnerService, private router:Router){}
ngOnInit(){
  this._learnerService.GetStudentsCv()
}
Cv(userid:number){
  this.router.navigate(['learner/StudentCv',userid])
}
}
