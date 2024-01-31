import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AdminService } from 'src/app/admin.service';

@Component({
  selector: 'app-admin-allstudents',
  templateUrl: './admin-allstudents.component.html',
  styleUrls: ['./admin-allstudents.component.css']
})
export class AdminAllstudentsComponent {
  constructor(public _adminService : AdminService, private router : ActivatedRoute){
  }
  
  ngOnInit() {
    
    this._adminService.GetAllStudents().subscribe(
      (data: any) => {
        this._adminService.allStudents = data;
        this.Search();
      },
      (error: any) => {
        console.log(error);
      }
    );

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
  


  async onGetStudentReport(userId:number) {
    await this._adminService.GetGradeByUserId(userId);
    console.log(this._adminService.studentGrades)
    await this._adminService.GetAttendanceByUserId(userId);
    await this._adminService.generateStudentPDF();
  }
 


  searchText = ''
 
  Search()
  {
    this._adminService.filteredStudents = this._adminService.allStudents.filter((x:any)=> x.firstname.toUpperCase().includes(this.searchText.toUpperCase())|| x.lastname.toUpperCase().includes(this.searchText.toUpperCase()));
  }
 
  Search2(event: any)
  {
    this._adminService.filteredStudents = this._adminService.allStudents.filter((x:any)=> x.firstname.toUpperCase().includes(event.target.value.toUpperCase()) || x.lastname.toUpperCase().includes(event.target.value.toUpperCase()));
 
  }
 
 
  handleSearchInput(event: Event): void {
    // Extract the value from the input event
    this.searchText = (event.target as HTMLInputElement).value;
  }
}
