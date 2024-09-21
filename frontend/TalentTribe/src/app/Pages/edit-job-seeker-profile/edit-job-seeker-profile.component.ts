import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { JobSeekerProfile } from '../../models/job-seeker-profile.model';
import { AuthService } from '../../service/auth.service';
import { JobSeekerProfileService } from '../../service/job-seeker-profile.service';
import { CommonModule } from '@angular/common';
import { SweetAlertService } from '../../service/sweet-alert.service';

@Component({
  selector: 'app-edit-job-seeker-profile',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './edit-job-seeker-profile.component.html',
  styleUrl: './edit-job-seeker-profile.component.css'
})

export class EditJobSeekerProfileComponent implements OnInit {
  jobSeekerProfile: JobSeekerProfile = {
    jobSeekerProfileId: 0,
    fullName: '',
    skills: '',
    experience: '',
    education: '',
    address: '',
    city: '',
    state: '',
    country: '',
    postalCode: '',
    resumeUrl: '',
    profilePictureUrl: ''
  };

  profileId: number | null = null;
  selectedResumeFile: File | null = null;
  selectedProfilePictureFile: File | null = null;

  constructor(
    private profileService: JobSeekerProfileService,
    private authService: AuthService,
    private router: Router
    ,private sweetAlertService: SweetAlertService
  ) {}

  ngOnInit(): void {
    this.profileId = +this.authService.getProfileId()!;
    if (this.profileId) {
      this.loadProfile(this.profileId);
    }
  }

  loadProfile(profileId: number) {
    this.profileService.getProfile(profileId).subscribe(
      (profile: JobSeekerProfile) => {
        if (profile) {
          this.jobSeekerProfile = profile;
        }
      },
      (error) => {
        console.error('Error fetching profile:', error);
        alert('Unable to load profile data.');
      }
    );
  }

  onFileSelected(event: any, fileType: string): void {
    const file: File = event.target.files[0];
    if (fileType === 'resume') {
      this.selectedResumeFile = file;
    } else if (fileType === 'profilePicture') {
      this.selectedProfilePictureFile = file;
    }
  }

  onSubmit(form: any) {
    if (form.valid && this.profileId) {
      const formData = new FormData();

      // Add form fields to FormData
      formData.append('FullName', this.jobSeekerProfile.fullName || '');
      formData.append('Skills', this.jobSeekerProfile.skills || '');
      formData.append('Experience', this.jobSeekerProfile.experience || '');
      formData.append('Education', this.jobSeekerProfile.education || '');
      formData.append('Address', this.jobSeekerProfile.address || '');
      formData.append('City', this.jobSeekerProfile.city || '');
      formData.append('State', this.jobSeekerProfile.state || '');
      formData.append('Country', this.jobSeekerProfile.country || '');
      formData.append('PostalCode', this.jobSeekerProfile.postalCode || '');

      // Add the selected resume file if available
      if (this.selectedResumeFile) {
        formData.append('resume', this.selectedResumeFile, this.selectedResumeFile.name);
      }

      // Add the selected profile picture if available
      if (this.selectedProfilePictureFile) {
        formData.append('profilePicture', this.selectedProfilePictureFile, this.selectedProfilePictureFile.name);
      }

      // Send the form data to the API
      this.profileService.updateProfile(this.profileId, formData).subscribe(
        () => {
          this.sweetAlertService.showSuccess('Profile updated successfully', 'Success');

          this.router.navigate(['/jobseeker-profile']);
        },
        (error) => {
          console.error('Error updating profile:', error);
          alert('An error occurred while updating the profile.');
        }
      );
    }
  }
}