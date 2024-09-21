import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../service/auth.service';
import { JobSeekerProfileService } from '../../service/job-seeker-profile.service';
import { CommonModule } from '@angular/common';
import { JobSeekerProfile } from '../../models/job-seeker-profile.model';
import { SweetAlertService } from '../../service/sweet-alert.service';

@Component({
  selector: 'app-job-seeker-profile',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './job-seeker-profile.component.html',
  styleUrls: ['./job-seeker-profile.component.css']
})
export class JobSeekerProfileComponent implements OnInit {
  jobSeekerProfile: JobSeekerProfile | null = null;

  constructor(
    private authService: AuthService,
    private jobSeekerProfileService: JobSeekerProfileService,
    private sweetAlertService: SweetAlertService
  ) {}

  ngOnInit(): void {
    const profileId = this.authService.getProfileId();
    if (profileId) {
      this.jobSeekerProfileService.getJobSeekerProfile(profileId).subscribe((profile) => {
        this.jobSeekerProfile = profile;
      });
    }
  }

  // Check if the profile has all the necessary details
  isProfileComplete(): boolean {
    return this.jobSeekerProfile?.fullName &&
      this.jobSeekerProfile?.skills &&
      this.jobSeekerProfile?.experience &&
      this.jobSeekerProfile?.education &&
      this.jobSeekerProfile?.address &&
      this.jobSeekerProfile?.country
      ? true
      : false;
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
          this.sweetAlertService.showError('Error downloading resume:', 'Error');

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
          alert('Error viewing resume:');
        });
    }
  }
}
