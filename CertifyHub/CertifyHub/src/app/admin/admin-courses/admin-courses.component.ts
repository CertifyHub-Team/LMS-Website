import { ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { AdminService } from 'src/app/admin.service';
declare var $: any;
@Component({
  selector: 'app-admin-courses',
  templateUrl: './admin-courses.component.html',
  styleUrls: ['./admin-courses.component.css']
})
export class AdminCoursesComponent implements OnInit{
  programId : number = 0
  @ViewChild('createCourse') CreateCourseDialog : any
  @ViewChild('deleteCourse') DeleteCourseDialog : any
  @ViewChild('updateCourse') UpdateCourseDialog : any
  @ViewChild('modalRef') modalRef!: ElementRef;
  @ViewChild('updateModalRef') updateModalRef!: ElementRef;
  @ViewChild('updateForm') updateForm!: NgForm;
  constructor(public _adminService: AdminService, private route: ActivatedRoute , private router : Router, private dialog:MatDialog, private cdr: ChangeDetectorRef) {}
 

  ngOnInit() {

    this.route.paramMap.subscribe(params => {

      this.programId = Number(params.get('id'));
      this._adminService.GetCoursesByProgram(this.programId);

    });
    this._adminService.SetProgramId(this.programId)

}

GoToSection(sectionId: number)
    {
      this.router.navigate(['admin/AdminSections', sectionId]);
    

  }







  CreateCourseForm : FormGroup = new FormGroup(
    {
      coursename : new FormControl('', [Validators.required]),
      programid : new FormControl( this.programId, [Validators.required]),
      prerequisite : new FormControl(''),
      startdate : new FormControl('', [Validators.required]),
      enddate : new FormControl('', [Validators.required]),
      numberofsections: new FormControl(1 ),
      imagepath: new FormControl(''),
     
    }
  )
  OpenCreateDialog()
    {
    this.dialog.open(this.CreateCourseDialog , {
      width: '520px',
      height: '500px',
      enterAnimationDuration: 1000,
      exitAnimationDuration: 1000
     
    })
    }

CloseCreateDialog(){
  this.dialog.closeAll( )

}
async UploadImage(event: any, id:number)
{
  console.log(id)
  let file = event.target.files[0]
  let form = new FormData()
  form.append('file' , file)
   await this._adminService.UploadCourseImage(form)
}
 CreateCourse() {
console.log(this.CreateCourseForm.value)
this.CreateCourseForm.get('imagepath')?.setValue("CertifyHub1.png")
this.CreateCourseForm.get('programid')?.setValue(this.programId)
   this._adminService.CreateCourse(this.CreateCourseForm.value);
   this.CloseCreateDialog()

}

OpenConfirmDeleteDialog(id : number)
{
var dialog = this.dialog.open(this.DeleteCourseDialog)
dialog.afterClosed().subscribe(
 
  (result)=>{
    if(result == 'yes')
    this.DeleteCourse(id);}
    )
  }
 



CloseDeleteDialog(){
this.dialog.closeAll( )

}


async DeleteCourse(courseId: number){

   await this._adminService.DeleteCourse(courseId);
 console.log(courseId)
   await this.CloseDeleteDialog()

}

UpdateCourseForm : FormGroup = new FormGroup(
  {
    courseid : new FormControl('', [Validators.required]),
    coursename : new FormControl('', [Validators.required]),
    programid : new FormControl( this.programId, [Validators.required]),
    prerequisite : new FormControl(''),
    startdate : new FormControl('', [Validators.required]),
    enddate : new FormControl('', [Validators.required]),
    numberofsections: new FormControl(1 ),
    imagepath: new FormControl(''),
   
  }
)

 UpdateCourse(){
  this.UpdateCourseForm.get('imagepath')?.setValue(this._adminService.courseImage)
  this.CreateCourseForm.get('programid')?.setValue(this.programId)
  console.log(this.UpdateCourseForm.value)
   this._adminService.UpdateCourse(this.UpdateCourseForm.value);
   console.log(this.UpdateCourseForm.value)
   this.CloseDeleteDialog()

//this._adminService.GetSectionsByCourse(this.courseId);


}


OpenUpdateDialog(c : any)
  {
    
    console.log(c);
   
    this.UpdateCourseForm.patchValue(c)
this.dialog.open(this.UpdateCourseDialog ,{
  width: '520px',
      height: '520px',
  enterAnimationDuration: 1000,
  exitAnimationDuration: 1000
    
})
  }
  imageFormControl  = new FormControl('', Validators.required);
   UpdateImage(courseId: number) {
    console.log("id", courseId);
    console.log("IMggg", this._adminService.courseImage);
    
     this._adminService.UpdateCourseImage(this._adminService.courseImage, courseId);

    $('#modal' + courseId).modal('hide');

}


}
