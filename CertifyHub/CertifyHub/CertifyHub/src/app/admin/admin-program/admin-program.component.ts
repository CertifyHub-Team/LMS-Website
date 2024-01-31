import { ChangeDetectorRef, Component, ElementRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { AdminService } from 'src/app/admin.service';
declare var $: any;
@Component({
  selector: 'app-admin-program',
  templateUrl: './admin-program.component.html',
  styleUrls: ['./admin-program.component.css']
})
export class AdminProgramComponent {
  @ViewChild('createProgram') CreateProgramDialog : any
  @ViewChild('deleteProgram') DeleteProgramDialog : any
  @ViewChild('updateProgram') UpdateProgramDialog : any
  @ViewChild('modalRef') modalRef!: ElementRef;
  @ViewChild('updateModalRef') updateModalRef!: ElementRef;
  @ViewChild('updateForm') updateForm!: NgForm;
  constructor(public _adminService : AdminService, private router : Router, private dialog: MatDialog, private cdr: ChangeDetectorRef){
  }
  ngOnInit() {
    this._adminService.GetAdminPrograms()
  
  
  }
  GetCourses(id : number){
    this.router.navigate(['admin/AdminCourses', id]);
  }
  CreateProgramForm : FormGroup = new FormGroup(
    {
      trackname : new FormControl('', [Validators.required]),
      description : new FormControl('', [Validators.required]),
      programperiod: new FormControl('' , [Validators.required]),
      imagepath: new FormControl(''),
     
    }
  )
  OpenCreateDialog()
    {
    this.dialog.open(this.CreateProgramDialog , {
      width: '520px',
      height: '460px',
      enterAnimationDuration: 1000,
      exitAnimationDuration: 1000
     
    })
    }

CloseCreateDialog(){
  this.dialog.closeAll( )

}
   UploadImage(event: any, id:number)
  {
    console.log(id)
    let file = event.target.files[0]
    let form = new FormData()
    form.append('file' , file)
     this._adminService.UploadProgramImage(form)
  }
 CreateProgram() {
console.log(this.CreateProgramForm.value)
this.CreateProgramForm.get('imagepath')?.setValue("CetifyHub1.png")
   this._adminService.CreateProgram(this.CreateProgramForm.value);
   this.CloseCreateDialog()

}

OpenConfirmDeleteDialog(id : number)
{
var dialog = this.dialog.open(this.DeleteProgramDialog)
dialog.afterClosed().subscribe(
 
  (result)=>{
    if(result == 'yes')
    this.DeleteProgram(id);}
    )
  }
 

  imageFormControl  = new FormControl('', Validators.required);
   UpdateImage(programId: number) {
    console.log("id", programId);
    console.log("IMggg", this._adminService.programImage);
    
     this._adminService.UpdateProgramImage(this._adminService.programImage, programId);
    this.cdr.detectChanges();
    $('#modal' + programId).modal('hide');

}




CloseDeleteDialog(){
this.dialog.closeAll( )

}


async DeleteProgram(programId: number){

   await this._adminService.DeleteProgram(programId);
 console.log(programId)
   await this.CloseDeleteDialog()

}

UpdateProgramForm : FormGroup = new FormGroup(
  {
    trackname : new FormControl('', [Validators.required]),
    description : new FormControl('', [Validators.required]),
    programperiod: new FormControl('' , [Validators.required]),
    imagepath: new FormControl(''),
    programid: new FormControl('')
   
  }
)

 UpdateProgram(){
  this.UpdateProgramForm.get('imagepath')?.setValue(this._adminService.programImage)
   this._adminService.UpdateProgram(this.UpdateProgramForm.value);

   this.CloseDeleteDialog()

//this._adminService.GetSectionsByCourse(this.courseId);


}


OpenUpdateDialog(c : any)
  {
    
    console.log(c);
   
    this.UpdateProgramForm.patchValue(c)
this.dialog.open(this.UpdateProgramDialog ,{
  width: '520px',
      height: '460px',
  enterAnimationDuration: 1000,
  exitAnimationDuration: 1000
    
})
  }


}
