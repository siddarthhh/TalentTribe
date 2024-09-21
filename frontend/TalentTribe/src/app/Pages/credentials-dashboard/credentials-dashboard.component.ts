import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../service/auth.service';
import { CredentialService } from '../../service/credential.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SweetAlertService } from '../../service/sweet-alert.service';

@Component({
  selector: 'app-credentials-dashboard',
  standalone: true,
  imports: [CommonModule,RouterModule],
  templateUrl: './credentials-dashboard.component.html',
  styleUrl: './credentials-dashboard.component.css'
})
export class CredentialsDashboardComponent implements OnInit {
  credentials: any[] = [];
  profileId: number | null = null;

  constructor(private credentialService: CredentialService, private authService: AuthService,private sweetAlertService: SweetAlertService) {}

  ngOnInit(): void {
    // Fetch the profile ID from the AuthService
    this.profileId = Number(this.authService.getProfileId());

    if (this.profileId) {
      // Fetch the credentials for the logged-in job seeker
      this.credentialService.getJobSeekerCredentials(this.profileId).subscribe(
        (response) => {
          this.credentials = response;
        },
        (error) => {
          if (error.status === 404) {
            this.sweetAlertService.showWarning( 'No credentials found for this job seeker. Please add your credentials','Warning');
          } else {
            this.sweetAlertService.showWarning('Error', 'An unexpected error occurred. Please try again later.');
          }
        }
      );
    }
  }

  // Download credential PDF
  downloadCredential(credentialId: number): void {
    this.credentialService.downloadCredential(credentialId).subscribe((response) => {
      const blob = new Blob([response], { type: 'application/pdf' });
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download = `credential_${credentialId}.pdf`; // Set the download file name
      a.click();
    });
  }

  // View credential PDF in a new tab
  viewCredential(credentialId: number): void {
    this.credentialService.downloadCredential(credentialId).subscribe((response) => {
      const blob = new Blob([response], { type: 'application/pdf' });
      const url = window.URL.createObjectURL(blob);
      window.open(url);
    });
  }
  // Delete a credential by ID
  deleteCredential(credentialId: number): void {
    if (confirm('Are you sure you want to delete this credential?')) {
      this.credentialService.deleteCredential(credentialId).subscribe(
        (response) => {
        this.sweetAlertService.showSuccess('Credential deleted successfully','Success')

          
          // Refresh the list of credentials
          this.credentials = this.credentials.filter(c => c.credentialId !== credentialId);
        },
        (error) => {
          alert('Error deleting credential'+ error);
        }
      );
    }
  }

}