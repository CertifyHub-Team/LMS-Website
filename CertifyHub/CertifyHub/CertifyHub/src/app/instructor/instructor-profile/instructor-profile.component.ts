import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { InstructorService } from 'src/app/instructor.service';

@Component({
  selector: 'app-instructor-profile',
  templateUrl: './instructor-profile.component.html',
  styleUrls: ['./instructor-profile.component.css']
})
export class InstructorProfileComponent {
  @ViewChild('UpdateDialog') UpdatePhoneDialog: any
  @ViewChild('modalRef') modalRef!: ElementRef;
  @ViewChild('fullInterests') AboutMeDialog: any
  @ViewChild('UpdateDialog') UpdatePasswordDialog: any
  constructor(public _instructorService : InstructorService, public dialog: MatDialog, private router: Router){}
  userId = 0
   ngOnInit()
  {
    const userString: string | null = localStorage.getItem('user');

    if (userString) {
      const user = JSON.parse(userString);
      const userId = user.UserId;
  
      if(userId){
        
         this._instructorService.GetUserInfo(userId)
         this._instructorService.GetUserCvInfo(userId)
        this.userId = userId
        console.log(this.userId, userId)
      }
      
    }
  
   }

   

  isMyCvTabActive(): boolean {
    return !this._instructorService.unDocumented && this._instructorService.userCv != null;
  }

  isCreateTabActive(): boolean {
    return this._instructorService.unDocumented;
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
      cvid : new FormControl(this._instructorService.userCv.cvid),
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
    this._instructorService.CreateCv(this.CreateCvForm.value, this.userId);
  }

  // CreateCv()
  // {
  //   console.log(this.CreateCvForm.value)
  //   this._instructorService.CreateCv(this.CreateCvForm.value, this.userId)

  // }

  GetCvInfo(){
    this._instructorService.GetUserCvInfo(this.userId)
  }

  UpdateCvInfo(){
    this.UpdateCvForm.patchValue({
      cvid: this._instructorService.userCv.cvid,
      education: this._instructorService.userCv.education,
      gpa: this._instructorService.userCv.gpa,
      experience: this._instructorService.userCv.experience,
      major: this._instructorService.userCv.major,
      projects: this._instructorService.userCv.projects,
      skills: this._instructorService.userCv.skills,
      languages: this._instructorService.userCv.languages,
      interests: this._instructorService.userCv.interests,
      githublink: this._instructorService.userCv.githublink,
      linkedintlink: this._instructorService.userCv.linkedintlink,
      userid: this._instructorService.userCv.userid
    });
    console.log(this.UpdateCvForm)
    this._instructorService.UpdateCv(this.UpdateCvForm.value, this.userId)
    
    
    
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
  //   this._instructorService.UpdatePhoneNumber(this.phoneFormControl.value??'')
  // }

  UploadImage(event: any)
  {
    let file = event.target.files[0]
    let form = new FormData()
    form.append('file' , file)
    this._instructorService.UploadImage(form)
  }
  UpdateImage(imagePath:string)
  {
    
    this._instructorService.UpdateImage(imagePath,this.userId)
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
      this._instructorService.UpdatePassword(this.loginObject)
    }

Cv(){
  this.router.navigate(['learner/StudentCv/'+ this.userId])
}

}
