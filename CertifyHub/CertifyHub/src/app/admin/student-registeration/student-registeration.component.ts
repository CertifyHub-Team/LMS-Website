import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AdminService } from 'src/app/admin.service';

@Component({
  selector: 'app-student-registeration',
  templateUrl: './student-registeration.component.html',
  styleUrls: ['./student-registeration.component.css']
})
export class StudentRegisterationComponent {
  constructor(public _adminService : AdminService, private toastr: ToastrService){
  }
  ngOnInit() {


  }





  CreateUserForm : FormGroup = new FormGroup(
    {
      firstname : new FormControl('', [Validators.required]),
      lastname : new FormControl('', [Validators.required]),
      dateofbirth: new FormControl('' , [Validators.required]),
      imagepath : new FormControl('', ),
      address : new FormControl('', [Validators.required]),
      phonenumber: new FormControl('' , [Validators.required]),
     
    }
  )



  
  CreateUserLoginForm : FormGroup = new FormGroup(
    {
      username : new FormControl('', [Validators.required]),
      password : new FormControl('123'),
      roleid: new FormControl('' , [Validators.required]),
      userid : new FormControl('', [Validators.required]),
   
    }
  )

  
CreateUser(){

  this._adminService.CreateUser(this.CreateUserForm.value);

  this.CreateUserForm.reset();

}

  
CreateUserLogin(){

  this._adminService.CreateLogin(this.CreateUserLoginForm.value);
}

// async submited() {
//   try {
//     const createUserResponse = await this._adminService.CreateUser(this.CreateUserForm.value);
//     console.log("CreateUser response:", createUserResponse);

//     if (this._adminService.createUsers) {
//       const randomPassword = this.generateRandomPassword(8); // You can adjust the length as needed
//       await this.CreateUserLoginForm.patchValue({ userid: createUserResponse });
//       await this.CreateUserLoginForm.patchValue({ password: randomPassword });
//       await this.CreateUserLogin();
//     }
 
//     // Other logic or checks can be added here if needed
 
//     await this.finalRegistration(); // Call the last step separately
//   } catch (error) {
//     console.error(error);
//     this.toastr.warning("Something went wrong during user creation, login, or registration!");
//   }
 
//   this.CreateUserForm.reset();
//   this.CreateUserLoginForm.reset();
// }
async submited() {
  try {
    const createUserResponse = await this._adminService.CreateUser(this.CreateUserForm.value).toPromise();
    console.log(this.CreateUserForm.value)
    console.log(createUserResponse)
    if (createUserResponse) {
      const randomPassword = this.generateRandomPassword(8);
      await this.CreateUserLoginForm.patchValue({ userid: createUserResponse });
      await this.CreateUserLoginForm.patchValue({ password: randomPassword });
      
      await this.CreateUserLogin();
    }
 
    // Other logic or checks can be added here if needed
 
    await this.finalRegistration(); // Call the last step separately
  } catch (error) {
    console.error(error);
    this.toastr.warning("Something went wrong during user creation, login, or registration!");
  }
 
  this.CreateUserForm.reset();
  this.CreateUserLoginForm.reset();
}

async finalRegistration() {
  try {
    await this._adminService.Registration(this.CreateUserLoginForm.value);
  } catch (error) {
    console.error(error);
    this.toastr.warning("Registration failed!");
  }
}



generateRandomPassword(length: number): string {
  const charset = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
  let password = "";
  for (let i = 0; i < length; i++) {
    const randomIndex = Math.floor(Math.random() * charset.length);
    password += charset[randomIndex];
  }
  return password;
}
}
