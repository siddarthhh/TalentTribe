import { Component, OnInit } from '@angular/core';
import { JobApplication } from '../../models/application.model';
import { Router } from '@angular/router';
import { ApplicationService } from '../../service/application.service';
import { AuthService } from '../../service/auth.service';
import { CommonModule } from '@angular/common';
import { JobSeekerProfileService } from '../../service/job-seeker-profile.service';

@Component({
  selector: 'app-job-seeker-application',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './job-seeker-application.component.html',
  styleUrl: './job-seeker-application.component.css'
})
export class JobSeekerApplicationComponent implements OnInit {
  applications: JobApplication[] = [];

  constructor(
    private applicationService: JobSeekerProfileService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.loadApplications();
  }

  loadApplications(): void {
    const jobSeekerProfileId = this.authService.getProfileId();
    if (!jobSeekerProfileId) {
      console.error('Job Seeker Profile ID is missing');
      return;
    }

    this.applicationService
      .getApplicationsByJobSeekerProfileId(+jobSeekerProfileId) // Convert to number
      .subscribe(
        (data: JobApplication[]) => {
          this.applications = data;
        },
        (error) => {
          console.error('Error fetching applications:', error);
        }
      );
  }
}
