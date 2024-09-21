import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../service/auth.service';
import { Application } from '../../models/application.model';
import { ApplicationService } from '../../service/application.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatDialog } from '@angular/material/dialog';
import { SuccessDialogComponent } from '../success-dialog/success-dialog.component';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { SweetAlertService } from '../../service/sweet-alert.service';
@Component({
  selector: 'app-application',
  standalone: true,
  imports: [FormsModule,CommonModule,MatSnackBarModule],
  templateUrl: './application.component.html',
  styleUrl: './application.component.css'
})
export class ApplicationComponent implements OnInit {
  jobId: number | null = null;
  jobSeekerProfileId: string | null = null;
  coverLetter: string = '';

  constructor(
    private route: ActivatedRoute,
    private authService: AuthService,
    private applicationService: ApplicationService,
    private router: Router
    ,private sweetAlertService: SweetAlertService
  ) { }

  ngOnInit(): void {
    // Fetch JobId from URL params
    this.route.paramMap.subscribe(params => {
      this.jobId = +params.get('jobId')!;
    });

    // Get the JobSeekerProfileId from the decoded JWT token using AuthService
    this.jobSeekerProfileId = this.authService.getProfileId();

    // Redirect if not a job seeker
    if (!this.authService.hasRole('JobSeeker')) {
      this.router.navigate(['/']); // Navigate to home if not a JobSeeker
    }
  }

  submitApplication(): void {
    if (!this.jobId || !this.jobSeekerProfileId) {
      alert('JobId or JobSeekerProfileId missing');
      return;
    }

    const newApplication: Application = {
      jobId: this.jobId,
      jobSeekerProfileId: parseInt(this.jobSeekerProfileId, 10),
      coverLetter: this.coverLetter,
      status: 'Pending', // Default status
      resumeUrl: '', // Optional: You can pass if the user has a resume URL
      applicationDate: new Date(),
      submittedDate: new Date()
    };

    // Post application to the server
    this.applicationService.createApplication(newApplication).subscribe(
      response => {
        this.sweetAlertService.showSuccess('Application submitted successfully!','Success')
      this.router.navigate(['/job-list']); // Navigate to home if not a JobSeeker

      },
      error => {
        console.error('Error submitting application:', error);

        if (error.status === 400 && error.error?.message === "You have already applied for this job.") {

          this.sweetAlertService.showError('You have already applied for this job.', 'Error');
      this.router.navigate(['/job-list']); // Navigate to home if not a JobSeeker


        } else {
          this.sweetAlertService.showError('An error occurred while submitting your application.', 'Error');
        }
      }
    );
  }
}