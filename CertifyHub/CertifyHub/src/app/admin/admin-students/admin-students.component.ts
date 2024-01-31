import { ChangeDetectorRef, Component, NgZone, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import jsPDF from 'jspdf';
import { NgxSpinner, NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { AdminService } from 'src/app/admin.service';

@Component({
  selector: 'app-admin-students',
  templateUrl: './admin-students.component.html',
  styleUrls: ['./admin-students.component.css'],
})
export class AdminStudentsComponent {
  sectionId : number = 0
  sectionStudents: any = [];
studentId : number=0

  @ViewChild('addStudent') AddStudentDialog : any
  @ViewChild('deleteStudent') DeleteStudentDialog : any


  constructor(public _adminService : AdminService, private router : ActivatedRoute, private toastr: ToastrService, private dialog: MatDialog, private spinner: NgxSpinnerService, private ngZone: NgZone, ){
  }
  userId = 0
  ngOnInit() {
    this.router.paramMap.subscribe(params => {

      this.sectionId = Number(params.get('id'));
      this._adminService.GetStudentsByCSection(this.sectionId);
      this._adminService.GetStdSectionInfoById(this.sectionId);
    });
    const userString: string | null = localStorage.getItem('user');

    if (userString) {
      const user = JSON.parse(userString);
      this.userId = user.UserId;
    }

    this._adminService.getStudents(this.sectionId)

  
  this._adminService.GetAllStudents()
  }
  getSectionImagePath(section: any): string {
    // Check if image path is available, otherwise generate a default image
    return section.imagepath || this.getDefaultImagePath(section.firstname+ " " +section.lastname);
  }
  
  getAltText(section: any): string {
    // If image is available, use section name as alt text; otherwise, use a generic alt text
    return section.imagepath ? section.firstname+ " " +section.lastname : 'Section Image';
  }
  
  getDefaultImagePath(sectionName: string): string {
    // Extract first letters of firstname and lastname
    const [firstLetterFirstName, firstLetterLastName] = sectionName.split(' ')
      .map(namePart => namePart.charAt(0).toUpperCase());
  
    // Generate a default image URL using the first letters of firstname and lastname
    const defaultImageUrl = `https://via.placeholder.com/150/959acc/ffffff?text=${firstLetterFirstName}${firstLetterLastName}`;
  
    return defaultImageUrl;
  }
  
  GenerateCertificate(userId : number, sectionId: number){

    this._adminService.GenerateCertificate(userId, sectionId)
  }


  onGetSectionReport() {
    // Assuming you have the section ID, replace 1 with the actual section ID
    this._adminService.GetStudentsByCSection(this.sectionId);
    this._adminService.GetStdSectionInfoById(this.sectionId);
    this._adminService.generateSectionPDF();
  }
 


        CreateStudentForm : FormGroup = new FormGroup(
          {
            sectionid : new FormControl(''),
            
            studentid: new FormControl('', [Validators.required]),
           
          });

          
        DeleteStudentForm : FormGroup = new FormGroup(
          {
            sectionid : new FormControl(''),
            
            studentid: new FormControl('', [Validators.required]),
           
          });


          // OpenAddDialog()
          // {
          // this.dialog.open(this.AddStudentDialog , {
          //   width: '390px',
          //   height: '260px',
          //   enterAnimationDuration: 1000,
          //   exitAnimationDuration: 1000
           
          // })
          // }
      
      
      
          OpenAddDialog() {
            // Set the sectionid in the form before opening the dialog
            this.CreateStudentForm.get('sectionid')?.setValue(this.sectionId);
          
            this.dialog.open(this.AddStudentDialog, {
              width: '390px',
              height: '260px',
              enterAnimationDuration: 1000,
              exitAnimationDuration: 1000,
            });
          }
          
          CloseAddDialog(){
        this.dialog.closeAll( )
      
      }

     students:  any=[] 
      async AddStudent(){

        console.log(this.CreateStudentForm.value)


       await this._adminService.CreateStdSection(this.CreateStudentForm.value);

        await this.CloseAddDialog()
      }




      
OpenConfirmDeleteDialog(id : number, sectionid:number)
{
  // this.DeleteStudentForm.get('studentid')?.setValue(this.studentId);
  // this.DeleteStudentForm.get('sectionid')?.setValue(this.sectionId);
  console.log(id)
  console.log(sectionid)

var dialog = this.dialog.open(this.DeleteStudentDialog)
dialog.afterClosed().subscribe(
 
  (result)=>{
    if(result == 'yes')
    this.DeleteStudent(id, sectionid);}
    )
  }
 



CloseDeleteDialog(){
this.dialog.closeAll( )

}


  
  
 
async DeleteStudent(studentId: number, sectionId:number){

  await this._adminService.DeleteStudent(studentId, sectionId);
    
  console.log(studentId)


  await this.CloseDeleteDialog()

}





async CheckCoursePeriod(userid: number, sectionId: number) {
  try {
    await this.ngZone.run(async () => {
      await this._adminService.GetCourseBySectionId(sectionId);
      await this._adminService.GetUserInfo(userid);
      await this._adminService.GetStudentGrade(userid, sectionId);
      if (this._adminService.sectionCourse && this._adminService.sectionCourse.enddate) {
        const currentDate = new Date();
        const endDate = new Date(this._adminService.sectionCourse.enddate);
        const email = this._adminService.userInfo.username;
        const flag = this._adminService.studentGrade.flag;

        this.spinner.show();

        if (currentDate >= endDate && !flag) {
          this._adminService.GenerateCertificate(userid, sectionId).subscribe(
            (certificateBytes: Uint8Array) => {
              // Send certificate and email
              this._adminService.sendCertificateAndEmail(certificateBytes, email).then(
                (success:any) => {
                  if (success) {
                    this._adminService.SetFlag(sectionId, userid)
                    console.log('Proceed with further actions...');
                  } else {
                    // Handle failure
                    console.error('Failed to send certificate and email.');
                  }
                }
              )
            },
            (error) => {
              console.error("Error while generating certificate:", error);
            }
          );
          setTimeout(() => {
            this.spinner.hide();
          }, 5000);
        }
        else if(flag) {
          this.toastr.warning('Certificate is already generated.');
        }
        else {
          this.toastr.warning('Generate Certificate is not available until the course period is completed.');
          setTimeout(() => {
            this.spinner.hide();
          }, 500);
        }
      } else {
        console.error('Unable to check course period.');
      }
    });
  } catch (error) {
    console.error('Error while fetching course data:', error);
    // Handle the error as needed
    this.toastr.error('An error occurred while checking the course period. Please try again.');
    this.spinner.hide();
  }
}


//new


GetCertificate(userId:number){
  this._adminService.GetStudentCertificate(userId, this.sectionId)
}
}
