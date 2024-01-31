import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { InstructorService } from 'src/app/instructor.service';

@Component({
  selector: 'app-instructor-sections',
  templateUrl: './instructor-sections.component.html',
  styleUrls: ['./instructor-sections.component.css']
})
export class InstructorSectionsComponent {
constructor(public _instructorService : InstructorService, private route: Router){}
userId = 0
ngOnInit(){
  const userString: string | null = localStorage.getItem('user');

  if (userString) {
    const user = JSON.parse(userString);
    this.userId = user.UserId;
  }

  this._instructorService.GetInstructorSections(this.userId)
  this._instructorService.instructorSections.forEach((section:any) => {
    const courseName = section?.course?.COURSENAME;
    console.log(courseName);
  });
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
  // Check if sectionName is defined and not an empty string
  if (sectionName && sectionName.trim() !== '') {
    // Generate a default image URL using the first letter and the second part of the section name
    const firstLetter = sectionName.charAt(0).toUpperCase();
    const secondPart = sectionName.split(' ').slice(1).join(''); // Get the second part of the name
    const defaultImageUrl = `https://via.placeholder.com/80/959acc/ffffff?text=${firstLetter}${secondPart}`;

    return defaultImageUrl;
  } else {
    // If sectionName is undefined or an empty string, return a default placeholder URL
    return 'https://via.placeholder.com/80/959acc/ffffff?text=Default';
  }
}


ViewSection(sectionId: number)
{
this.route.navigate(['instructor/InstructorSection', sectionId]);
}

}
