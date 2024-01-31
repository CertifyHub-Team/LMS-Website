import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AdminService } from 'src/app/admin.service';
import { LearnerService } from 'src/app/learner.service';

@Component({
  selector: 'app-student-program',
  templateUrl: './student-program.component.html',
  styleUrls: ['./student-program.component.css']
})
export class StudentProgramComponent  {
  constructor(public _learnerService : LearnerService, private router : Router, private cdr: ChangeDetectorRef,public _adminService : AdminService){}
  userId = 0
  ngOnInit()
  {
    const userString: string | null = localStorage.getItem('user');

    if (userString) {
      const user = JSON.parse(userString);
      this.userId = user.UserId;
    }
    this._learnerService.GetCourses(this.userId)
    console.log(this._learnerService.myCourses[0].courses[0].imagepath)
  }

  GoToSection(id: number, courseId : number)
{
  this.router.navigate(['learner/CourseSection', id, courseId]).then(() => {
    this.cdr.detectChanges();
});
}
}
