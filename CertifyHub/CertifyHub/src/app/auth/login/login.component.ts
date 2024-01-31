import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AdminService } from 'src/app/admin.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  constructor( public _adminService : AdminService ){}
  loginForm : FormGroup = new FormGroup({
    username : new FormControl('',[Validators.required]),
    password : new FormControl('',[Validators.required])
  })
  ngOnInit(){}
  Login(){
    console.log(this.loginForm.value);
  
  this._adminService.Login(this.loginForm.value)
  }
}
