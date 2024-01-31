
import { Component, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { AdminService } from 'src/app/admin.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css']
})
export class CourseComponent {
  constructor(public _adminService :AdminService,public dialog:MatDialog,private router: Router)
  {
   
  }
  CreateCourseForm : FormGroup=new FormGroup({
    coursename :new FormControl('',[Validators.required]),
    programid:new FormControl('',[Validators.required]),
    startdate:new FormControl('',[Validators.required]),
    enddate:new FormControl('',[Validators.required]),
    imagepath:new FormControl,
    numberofsections:new FormControl('',[Validators.required]),
    prerequisite:new FormControl('',[Validators.required])

  })
  UpdateCourseForm : FormGroup=new FormGroup({
    courseid:new FormControl(),
    coursename :new FormControl('',[Validators.required]),
    programid:new FormControl('',[Validators.required]),
    startdate:new FormControl('',[Validators.required]),
    enddate:new FormControl('',[Validators.required]),
    imagepath:new FormControl('',[Validators.required]),
    numberofsections:new FormControl('',[Validators.required]),
    prerequisite:new FormControl('',[Validators.required]),
    program:new FormControl(),
    certificates:new FormControl(),
    materials:new FormControl(),
    sections:new FormControl()

  })
  

  @ViewChild('CreateDialog')CreateCourseDialog:any
  OpenCreateDialog(){
    this.dialog.open(this.CreateCourseDialog,{
      width:'400px',
      height:'600px',
      enterAnimationDuration:1000,
      exitAnimationDuration:1000
    })

  }
  @ViewChild('UpdateDialog')UpdateCourseDialog:any
  OpenUpdateDialog(course:any){
  this.UpdateCourseForm.setValue(course)
    this.dialog.open(this.UpdateCourseDialog,{
      width:'400px',
      height:'600px',
      enterAnimationDuration:1000,
      exitAnimationDuration:1000
    })
  }

  @ViewChild('ConfirmationDialog')ConfirmDeleteDialog:any
  OpenConfirmDialog(courseId:number){
    var dialog=this.dialog.open(this.ConfirmDeleteDialog)
    dialog.afterClosed().subscribe(
      (result)=>{
        if(result!=undefined)
        if(result=='yes')
        this.DeleteCourse(courseId);
      }
    )
  }
  ngOnInit(){
    this._adminService.GetAllCourses1();
    this._adminService.GetAllPrograms();
  }

  DeleteCourse(courseId:number){
    this._adminService.DeleteCourse(courseId);
  }

  CreateCourse(){
    console.log(this.CreateCourseForm.value)
    this._adminService.CreateCourse(this.CreateCourseForm.value);
  }
  UpdateCourse(){
    this._adminService.UpdateCourse(this.UpdateCourseForm.value);
  }
  UploadImages(event:any){
  let file=event.target.files[0]
  let form=new FormData()
  form.append('file',file)
  this._adminService.UploadImage1(form)
  }
}
