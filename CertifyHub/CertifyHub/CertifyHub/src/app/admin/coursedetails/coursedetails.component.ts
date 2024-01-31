import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { AdminService } from 'src/app/admin.service';

@Component({
  selector: 'app-coursedetails',
  templateUrl: './coursedetails.component.html',
  styleUrls: ['./coursedetails.component.css']
})
export class CoursedetailsComponent {
  constructor(public _adminService :AdminService,public dialog:MatDialog,private router: Router)
  {
   
  }
  ngOnInit(){
    this._adminService.GetAllCourses1();
    this._adminService.GetAllPrograms();
  }
}
