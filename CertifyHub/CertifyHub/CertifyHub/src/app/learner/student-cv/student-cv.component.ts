import { Component } from '@angular/core';
import { LearnerService } from 'src/app/learner.service';
import jsPDF from 'jspdf';
import html2canvas from 'html2canvas';
import { AdminService } from 'src/app/admin.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-student-cv',
  templateUrl: './student-cv.component.html',
  styleUrls: ['./student-cv.component.css']
})
export class StudentCvComponent {
  constructor(public _learnerService : LearnerService,   private router : ActivatedRoute,){}
  userId = 0
  ngOnInit()
  {

    this.router.paramMap.subscribe(params => {

      this.userId = Number(params.get('id'));
    });


  this._learnerService.GetUserInfo(this.userId)
  this._learnerService.GetUserCvInfo(this.userId)
   }

   downloadPdf() {
    const data = document.getElementById('cv-container');
    if (data) { 
      html2canvas(data).then(canvas => {
        const imgData = canvas.toDataURL('image/png');
        const pdf = new jsPDF({
          orientation: 'portrait',
          unit: 'px',
          format: 'a4'
        });
   
        const imgProps = pdf.getImageProperties(imgData);
        const pdfWidth = pdf.internal.pageSize.getWidth();
        const pdfHeight = (imgProps.height * pdfWidth) / imgProps.width;
   
        pdf.addImage(imgData, 'PNG', 0, 0, pdfWidth, pdfHeight);
        pdf.save('cv.pdf');
      });
    } else {
      console.error('Element #cv-container not found!');
    }
  }
  

}

