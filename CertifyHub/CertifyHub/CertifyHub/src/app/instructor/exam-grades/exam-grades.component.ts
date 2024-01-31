import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { InstructorService } from 'src/app/instructor.service';

@Component({
  selector: 'app-exam-grades',
  templateUrl: './exam-grades.component.html',
  styleUrls: ['./exam-grades.component.css']
})
export class ExamGradesComponent {
  constructor(public _instructorService:InstructorService, private router: ActivatedRoute ){}

  userId = 0
  sectionId = 0
  assessmentId = 0
  ngOnInit() {
    this.router.paramMap.subscribe(params => {

      this.sectionId = Number(params.get('sectionId'));
      this.assessmentId = Number(params.get('assessmentId'));
      //new
      this._instructorService.GetStudentsSolutionBySection(this.sectionId, this.assessmentId)
      this._instructorService.GetStudentsGrades(this.assessmentId)
      console.log(this._instructorService.studentsGrades)
    });
}


}
