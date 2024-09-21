import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CredentialService } from '../../service/credential.service';
import { AuthService } from '../../service/auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-add-credential',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './add-credential.component.html',
  styleUrls: ['./add-credential.component.css']
})
export class AddCredentialComponent {
  credentialName: string = '';
  issuedBy: string = '';
  issueDate: string = '';
  credentialType: string = '';
  selectedFile: File | null = null;
  
  // Used to disable future dates
  maxDate: string = new Date().toISOString().split('T')[0]; 

  constructor(
    private credentialService: CredentialService,
    private authService: AuthService,
    private router: Router
  ) {}

  onFileChange(event: any) {
    if (event.target.files.length > 0) {
      this.selectedFile = event.target.files[0];
    }
  }

  onSubmit() {
    if (!this.selectedFile) {
      alert('Please select a PDF file.');
      return;
    }

    // Add more validations as needed
    if (!this.credentialName || !this.issuedBy || !this.issueDate || !this.credentialType) {
      alert('Please fill in all required fields.');
      return;
    }

    this.credentialService.uploadCredential(
      this.credentialName,
      this.issuedBy,
      this.issueDate,
      this.credentialType,
      this.selectedFile
    ).subscribe(
      response => {
        console.log('Credential added successfully', response);
        this.router.navigate(['/credentials-dashboard']);
      },
      error => {
        console.error('Error adding credential', error);
      }
    );
  }
}
