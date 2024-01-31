import { Component, OnInit, ElementRef, ViewChild, AfterViewInit } from '@angular/core';
import { LearnerService } from 'src/app/learner.service';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { HttpClient } from '@angular/common/http';
import { AdminService } from 'src/app/admin.service';
 
@Component({
  selector: 'app-certificate',
  templateUrl: './certificate.component.html',
  styleUrls: ['./certificate.component.css']
})
export class CertificateComponent implements OnInit {
  certificates: any[] = [];
  pdfUrl: SafeResourceUrl | undefined;
 
 
 
  constructor(public _learnerService: LearnerService,  private sanitizer: DomSanitizer,    private http: HttpClient, public _adminService : AdminService) {  }
  userId = 0
  
  ngOnInit(): void {
    const userString: string | null = localStorage.getItem('user');

    if (userString) {
      const user = JSON.parse(userString);
      this.userId = user.UserId;
    }
 
    this._learnerService.GetUserCertificates(this.userId).subscribe(
      (data: any[]) => {
        console.log('API Response:', data);
        this.certificates = data;
      },
      (error) => {
        console.error('Error fetching certificates:', error);
      }
    );}
 
 
 
 
    getSafeUrl(url: string): SafeResourceUrl {
      return this.sanitizer.bypassSecurityTrustResourceUrl(url);
    }
 
 
    // displayCertificatePdf(certificate: any): void {
    //   console.log('Displaying certificate:', certificate);
   
    //   if (certificate && certificate.certificatecloudinaryurl) {
    //     const cloudinaryUrlParts = certificate.certificatecloudinaryurl.split('/');
    //     const cloudName = cloudinaryUrlParts[3];
    //     const version = cloudinaryUrlParts[6];
    //     const publicId = cloudinaryUrlParts[7].split('.')[0];
   
    //     const directPdfUrl = `https://res.cloudinary.com/${cloudName}/image/upload/${version}/${publicId}.pdf`;
   
    //     this.pdfUrl = this.sanitizer.bypassSecurityTrustResourceUrl(directPdfUrl);
    //   } else {
    //     console.error('Certificate object or certificate.certificatecloudinaryurl is undefined:', certificate);
    //   }
    // }
 
   
    downloadCertificate(certificate: any): void {
      if (certificate && certificate.certificatecloudinaryurl) {
        // Fetch the PDF content using HttpClient
        this.http.get(certificate.certificatecloudinaryurl, { responseType: 'arraybuffer' })
          .subscribe((data: ArrayBuffer) => {
            // Create a Blob from the PDF content
            const blob = new Blob([data], { type: 'application/pdf' });
 
            // Create a download link and trigger the download
            const link = document.createElement('a');
            link.href = window.URL.createObjectURL(blob);
            link.download = 'certificate.pdf';
            link.target = '_blank';
            link.click();
          }, (error) => {
            console.error('Error fetching PDF content:', error);
          });
      } else {
        console.error('Certificate object or certificate.certificatecloudinaryurl is undefined:', certificate);
      }
    }
 
  }
 