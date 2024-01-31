import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { LearnerService } from 'src/app/learner.service';
import { MatDialog } from '@angular/material/dialog';
import { AdminService } from 'src/app/admin.service';
import { Router } from '@angular/router';



@Component({
  selector: 'app-student-profile',
  templateUrl: './student-profile.component.html',
  styleUrls: ['./student-profile.component.css']
})
export class StudentProfileComponent{
  @ViewChild('UpdateDialog') UpdatePhoneDialog: any
  @ViewChild('modalRef') modalRef!: ElementRef;
  @ViewChild('fullInterests') AboutMeDialog: any
  @ViewChild('UpdateDialog') UpdatePasswordDialog: any
  constructor(public _learnerService : LearnerService, public dialog: MatDialog, private router: Router){}
  userId = 0
   ngOnInit()
  {
    const userString: string | null = localStorage.getItem('user');

    if (userString) {
      const user = JSON.parse(userString);
      const userId = user.UserId;
  
      if(userId){
        
         this._learnerService.GetUserInfo(userId)
         this._learnerService.GetUserCvInfo(userId)
        this.userId = userId
        console.log(this.userId, userId)
      }
      
    }
  
   }

   

  isMyCvTabActive(): boolean {
    return !this._learnerService.unDocumented && this._learnerService.userCv != null;
  }

  isCreateTabActive(): boolean {
    return this._learnerService.unDocumented;
  }

  CreateCvForm : FormGroup = new FormGroup(
    {
      education : new FormControl( [Validators.required]),
      gpa : new FormControl( [Validators.required]),
      experience: new FormControl( [Validators.required]),
      major : new FormControl( [Validators.required]),
      projects : new FormControl( [Validators.required]),
      skills: new FormControl( [Validators.required]),
      languages : new FormControl( [Validators.required]),
      interests: new FormControl( [Validators.required]),
      githublink: new FormControl( [Validators.required]),
      linkedintlink : new FormControl( [Validators.required]),
      certificates :new FormControl( [Validators.required]),
      rating : new FormControl( [Validators.required]),
      userid : new FormControl()
    }
  )
  
  UpdateCvForm : FormGroup = new FormGroup(
    {
      cvid : new FormControl(this._learnerService.userCv.cvid),
      education : new FormControl('', [Validators.required]),
      gpa : new FormControl( [Validators.required]),
      experience: new FormControl('' , [Validators.required]),
      major : new FormControl('', [Validators.required]),
      projects : new FormControl('', [Validators.required]),
      skills: new FormControl('' , [Validators.required]),
      languages : new FormControl('', [Validators.required]),
      interests: new FormControl('' , [Validators.required]),
      gitHubLink: new FormControl('' , [Validators.required]),
      linkedintlink : new FormControl('', [Validators.required]),
      userid : new FormControl(),
    }
  )

  phoneFormControl  = new FormControl('', [Validators.required, Validators.minLength(10)]);
  imageFormControl  = new FormControl('', Validators.required);

  CreateCv() {
    // Set the userid value before submitting the form
    this.CreateCvForm.get('userid')?.setValue(this.userId);
   
    console.log(this.CreateCvForm.value);
    this._learnerService.CreateCv(this.CreateCvForm.value, this.userId);
  }

  // CreateCv()
  // {
  //   console.log(this.CreateCvForm.value)
  //   this._learnerService.CreateCv(this.CreateCvForm.value, this.userId)

  // }

  GetCvInfo(){
    this._learnerService.GetUserCvInfo(this.userId)
  }

  UpdateCvInfo(){
    this.UpdateCvForm.patchValue({
      cvid: this._learnerService.userCv.cvid,
      education: this._learnerService.userCv.education,
      gpa: this._learnerService.userCv.gpa,
      experience: this._learnerService.userCv.experience,
      major: this._learnerService.userCv.major,
      projects: this._learnerService.userCv.projects,
      skills: this._learnerService.userCv.skills,
      languages: this._learnerService.userCv.languages,
      interests: this._learnerService.userCv.interests,
      githublink: this._learnerService.userCv.githublink,
      linkedintlink: this._learnerService.userCv.linkedintlink,
      userid: this._learnerService.userCv.userid
    });
    console.log(this.UpdateCvForm)
    this._learnerService.UpdateCv(this.UpdateCvForm.value, this.userId)
    
    
    
  }


  OpenUpdateDialog(c: string) {
    console.log('Opening dialog with value:', c);
    this.phoneFormControl.setValue(c);
    this.dialog.open(this.UpdatePhoneDialog, {
      width: '215px',
      height: '125px',
    });
  }

  OpenAboutMeDialog(c: any) {
    this.dialog.open(this.AboutMeDialog, {
      width: '700px',
      height: '360px',
    });
  }
  CloseAboutMeDialog() {
    this.dialog.closeAll();
  }
  // UpdatePhoneNumber()
  // {
  //   console.log('phone')
  //   this._learnerService.UpdatePhoneNumber(this.phoneFormControl.value??'')
  // }

  UploadImage(event: any)
  {
    let file = event.target.files[0]
    let form = new FormData()
    form.append('file' , file)
    this._learnerService.UploadImage(form)
  }
  UpdateImage(imagePath:string)
  {
    
    this._learnerService.UpdateImage(imagePath,this.userId)
    this.modalRef.nativeElement.click();
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


  passwordFormControl  = new FormControl('', [Validators.required]);

  loginObject: any = {
    password: '',
    userid: this.userId
  };
  
  OpenUpdatePasswordDialog() {
    console.log('Opening dialog with value:');
  
    this.dialog.open(this.UpdatePasswordDialog, {
      width: '280px',
      height: '180px'
    });
  }
  
  
  
    UpdatePassword()
    {
      console.log('password')
      this.loginObject.password = this.passwordFormControl.value;
      this._learnerService.UpdatePassword(this.loginObject)
    }

Cv(){
  this.router.navigate(['learner/StudentCv/'+ this.userId])
}

}
