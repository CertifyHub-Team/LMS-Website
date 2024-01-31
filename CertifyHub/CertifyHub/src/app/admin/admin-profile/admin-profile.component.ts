import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { AdminService } from 'src/app/admin.service';

@Component({
  selector: 'app-admin-profile',
  templateUrl: './admin-profile.component.html',
  styleUrls: ['./admin-profile.component.css']
})
export class AdminProfileComponent {

@ViewChild('UpdateDialog') UpdatePasswordDialog: any
@ViewChild('modalRef') modalRef!: ElementRef;

constructor(public _adminService: AdminService, private dialog:MatDialog){}
userId = 0
async ngOnInit() {
  const userString: string | null = localStorage.getItem('user');

  if (userString) {
    const user = JSON.parse(userString);
    const userId = user.UserId;

    if(userId){
      this.userId = userId
      await this._adminService.GetUserInfo(userId);
    }
    
  }
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
imageFormControl  = new FormControl('', Validators.required);

loginObject: any = {
  password: '',
  userid: this.userId
};

OpenUpdateDialog() {
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
    this._adminService.UpdatePassword(this.loginObject)
  }

UploadImage(event: any)
  {
    let file = event.target.files[0]
    let form = new FormData()
    form.append('file' , file)
    this._adminService.UploadImage(form)
  }
  UpdateImage(imagePath:string)
  {
    
    this._adminService.UpdateImage(imagePath, this.userId)
    this.modalRef.nativeElement.click();
  }

}