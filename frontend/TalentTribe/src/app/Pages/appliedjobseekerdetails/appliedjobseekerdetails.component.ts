import { Component, OnInit } from '@angular/core';
import { JobSeekerProfile } from '../../models/job-seeker-profile.model';
import { AuthService } from '../../service/auth.service';
import { JobSeekerProfileService } from '../../service/job-seeker-profile.service';
import { ActivatedRoute } from '@angular/router';
import { CredentialService } from '../../service/credential.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Application, ApplicationUpdatePayload } from '../../models/application.model';
import { ApplicationService } from '../../service/application.service';
import { SweetAlertService } from '../../service/sweet-alert.service';

@Component({
  selector: 'app-appliedjobseekerdetails',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './appliedjobseekerdetails.component.html',
  styleUrl: './appliedjobseekerdetails.component.css'
})

export class AppliedjobseekerdetailsComponent implements OnInit {
  applicationId!: number;
  application: Application | null = null;
  jobSeekerProfile: JobSeekerProfile | null = null;
  jobSeekerProfileId!: string; // Store jobSeekerProfileId from URL
  credentials: any[] = [];
  profileId: number | null = null;
  updatedStatus: string = '';
  updatedFeedback: string = '';

  constructor(
    private route: ActivatedRoute, // Inject ActivatedRoute
    private jobSeekerProfileService: JobSeekerProfileService,
    private credentialService: CredentialService,
    private applicationService: ApplicationService
    ,private sweetAlertService: SweetAlertService
  ) { }

  ngOnInit(): void {

    // Get the applicationId and jobSeekerProfileId from the URL
    this.applicationId = +this.route.snapshot.paramMap.get('ApplicationId')!;
    this.jobSeekerProfileId = this.route.snapshot.paramMap.get('jobSeekerProfileId')!;

    // Fetch the application details using applicationId
    this.applicationService.getApplicationById(this.applicationId).subscribe(application => {
      this.application = application;
      this.updatedStatus = application.status || '';
      this.updatedFeedback = application.feedback || '';
    });
    // Retrieve the jobSeekerProfileId from the URL
    this.jobSeekerProfileId = this.route.snapshot.paramMap.get('jobSeekerProfileId')!;
    this.profileId = +this.route.snapshot.paramMap.get('jobSeekerProfileId')!;

    // Call the service to get the job seeker profile
    this.jobSeekerProfileService.getJobSeekerProfile(this.jobSeekerProfileId).subscribe(profile => {
      this.jobSeekerProfile = profile;
    });
    if (this.profileId) {
      // Fetch the credentials for the logged-in job seeker
      this.credentialService.getJobSeekerCredentials(this.profileId).subscribe(
        (response) => {
          this.credentials = response;
        },
        (error) => {
          console.error('Error fetching credentials', error);
        }
      );
    }
  }

  // Get the profile picture URL
  getProfilePictureUrl(): string {
    return this.jobSeekerProfile?.jobSeekerProfileId
      ? this.jobSeekerProfileService.getProfilePictureUrl(this.jobSeekerProfile.jobSeekerProfileId)
      : 'assets/default-profile-picture.jpg'; // Fallback to default image
  }

  // Download resume as PDF
  downloadResume(): void {
    if (this.jobSeekerProfile?.jobSeekerProfileId) {
      this.jobSeekerProfileService.downloadResume(this.jobSeekerProfile.jobSeekerProfileId)
        .subscribe((response) => {
          const blob = new Blob([response], { type: 'application/pdf' });
          const url = window.URL.createObjectURL(blob);
          const a = document.createElement('a');
          a.href = url;
          a.download = `resume_.pdf`; // Set the download file name
          a.click();
        }, (error) => {
          console.error('Error downloading resume:', error);
          // Handle error appropriately, e.g., show error message to the user
        });
    }
  }

  // View resume PDF in a new tab
  viewResume(): void {
    if (this.jobSeekerProfile?.jobSeekerProfileId) {
      this.jobSeekerProfileService.downloadResume(this.jobSeekerProfile.jobSeekerProfileId)
        .subscribe((response) => {
          const blob = new Blob([response], { type: 'application/pdf' });
          const url = window.URL.createObjectURL(blob);
          window.open(url);
        }, (error) => {
          console.error('Error viewing resume:', error);
          // Handle error appropriately, e.g., show error message to the user
        });
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
  saveUpdates(): void {
    if (this.application) {
      // Create the update payload with only the fields that need to be updated
      const updatedApplication: ApplicationUpdatePayload = {
        status: this.updatedStatus,
        feedback: this.updatedFeedback,
      
       
      };
  
      this.applicationService.updateApplication(this.applicationId, updatedApplication).subscribe(
        () => {
          this.sweetAlertService.showSuccess('Updated successfully!','Success')

      },
      (error) => {
        // Display error alert
        window.alert('Error updating application: ' + error.message);
      }
      );
    }
  }
}

