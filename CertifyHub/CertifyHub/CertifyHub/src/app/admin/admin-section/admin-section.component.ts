import { ChangeDetectorRef, Component, NgZone, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { AdminService } from 'src/app/admin.service';


@Component({
  selector: 'app-admin-section',
  templateUrl: './admin-section.component.html',
  styleUrls: ['./admin-section.component.css']
})


export class AdminSectionComponent {
  courseId : number = 0;
  courses: any[] = [];
instructors: any[]=[];



//sectionId: number=0;
  @ViewChild('createSection') CreateSectionDialog : any
  @ViewChild('deleteSection') DeleteSectionDialog : any
  
  @ViewChild('updateSection') UpdateSectionDialog : any
  sectionDeleted: any;
  lectureTimeOptions: string[] = [];
constructor( public _adminService : AdminService, private router : ActivatedRoute, private route : Router, private dialog: MatDialog , private cdr: ChangeDetectorRef, private zone: NgZone){

}

ngOnInit() {

  this.router.paramMap.subscribe(params => {

    this.courseId = Number(params.get('id'));
    this._adminService.GetSectionsByCourse(this.courseId);
    this._adminService.getCourses(this.courseId)

    this._adminService.GetCourseById(this.courseId);
     // this._adminService.GetCourseBySectionId(this.sectionId);




    //all courses 
    this._adminService.GetAllCourses().subscribe(
      (data: any) => {
        console.log('Courses:', data);
        this.courses = data || [];
      },
      (error: any) => {
        console.error('Error fetching courses:', error);
      }
    );

    this._adminService.GetAllInstructors().subscribe(
      (data: any) => {
        console.log('Instructors:', data);
        this.instructors = data || [];
      },
      (error: any) => {
        console.error('Error fetching instructors:', error);
      }
    );
    // this._adminService.GetAllInstructors(this.instructors);
  });

}




ngAfterViewInit() {

  this.ngOnInit();

}


getSectionImagePath(section: any): string {
  // Check if image path is available, otherwise generate a default image
  return section.imagepath || this.getDefaultImagePath(section.sectionname);
}

getAltText(section: any): string {
  // If image is available, use section name as alt text; otherwise, use a generic alt text
  return section.imagepath ? section.sectionname : 'Section Image';
}

getDefaultImagePath(sectionName: string): string {
  // Generate a default image URL using the first letter and the second part of the section name
  const firstLetter = sectionName.charAt(0).toUpperCase();
  const secondPart = sectionName.split(' ').slice(1).join(''); // Get the second part of the name
  const defaultImageUrl = `https://via.placeholder.com/150/959acc/ffffff?text=${firstLetter}${secondPart}`;

  return defaultImageUrl;
}
GetStudents(sectionId: number)
    {
      this.route.navigate(['admin/Students', sectionId]);
    }





    CreateSectionForm : FormGroup = new FormGroup(
      {
        sectionname : new FormControl('', [Validators.required]),
        timelecture : new FormControl('', [Validators.required]),
        meetinglink: new FormControl('' , [Validators.required]),
        courseid : new FormControl('' ),
        instructorid : new FormControl('', [Validators.required]),
        imagepath: new FormControl(''),
        lecturedays: new FormControl('', [Validators.required]),
       
      }
    )
   


    
    UpdateSectionForm : FormGroup = new FormGroup(
      {
        sectionid: new FormControl('', [Validators.required]),
        sectionname : new FormControl('', [Validators.required]),
        timelecture : new FormControl('', [Validators.required]),
        meetinglink: new FormControl('' , [Validators.required]),
        courseid : new FormControl('' ),
        instructorid : new FormControl('', [Validators.required]),
        imagepath: new FormControl(''),
        lecturedays: new FormControl('', [Validators.required]),
       
      }
    )


    OpenCreateDialog()
    {
    this.dialog.open(this.CreateSectionDialog , {
      width: '500px',
      height: '565px',
      enterAnimationDuration: 1000,
      exitAnimationDuration: 1000
     
    })
    }

CloseCreateDialog(){
  this.dialog.closeAll( )

}
   
sections: any=[]
// CreateSection(){

//   this._adminService.CreateSection(this.sections)
// }


async CreateSection() {
  if (this.CreateSectionForm.controls['courseid'].value) {
    this.sections.courseid = this.CreateSectionForm.controls['courseid'].value;
  }

  if (this.CreateSectionForm.controls['instructorid'].value) {
    this.sections.instructorid = this.CreateSectionForm.controls['instructorid'].value;
  }


  await this._adminService.CreateSection(this.CreateSectionForm.value);
  await this.CloseCreateDialog()
  //this._adminService.GetSectionsByCourse(this.courseId);
 //this._adminService.GetSectionsByCourse(this.courseId);
  //await window.location.reload();




}

OpenConfirmDeleteDialog(id : number)
{
var dialog = this.dialog.open(this.DeleteSectionDialog)
dialog.afterClosed().subscribe(
 
  (result)=>{
    if(result == 'yes')
    this.DeleteSection(id);}
    )
  }
 



CloseDeleteDialog(){
this.dialog.closeAll( )

}


async DeleteSection(sectionId: number){

   await this._adminService.DeleteSection(sectionId);
 console.log(sectionId)


   await this.CloseDeleteDialog()

 //this._adminService.GetSectionsByCourse(this.courseId);
 //await  window.location.reload();

}






async UpdateSection(){

  await this._adminService.UpdateSection(this.UpdateSectionForm.value);
  // console.log(section)
  await this.CloseDeleteDialog()

//this._adminService.GetSectionsByCourse(this.courseId);


}


OpenUpdateDialog(c : any)
  {
    
    console.log(c);
   
    this.UpdateSectionForm.patchValue(c)
this.dialog.open(this.UpdateSectionDialog ,{
  width: '500px',
  height: '565px',
  enterAnimationDuration: 1000,
  exitAnimationDuration: 1000
    
})
  }


  weekdays: string[] = ['Sun - Tue - Thu', 'Mon - Wed'];
  times: string[] = [
    '8:00AM-9:30AM',
    '9:30AM-11:00AM',
    '11:00AM-12:30PM',
    '12:30PM-2:00PM',
    '8:00AM-9:00AM',
    '9:00AM-10:00AM',
    '10:00AM-11:00AM',
    '11:00AM-12:00PM',
    '12:00PM-1:00PM',
    '1:00PM-2:00PM',
  ];
  

  // OpenDeleteDialog()
// {
// this.dialog.open(this.DeleteSectionDialog , {
//   width: '300px',
//   height: '300px',
//   enterAnimationDuration: 1000,
//   exitAnimationDuration: 1000
 
// })
// }
CloseUpdateDialog(){
this.dialog.closeAll( )

}








}