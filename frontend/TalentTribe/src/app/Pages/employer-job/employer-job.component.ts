import { Component, OnInit } from '@angular/core';
import { Job } from '../../models/job.model';
import { AuthService } from '../../service/auth.service';
import { JobService } from '../../service/job.service';
import { CommonModule } from '@angular/common';
import { EmployerProfileService } from '../../service/employer-profile.service';
import { Router, RouterModule } from '@angular/router';
import { SweetAlertService } from '../../service/sweet-alert.service';

@Component({
  selector: 'app-employer-job',
  standalone: true,
  imports: [CommonModule,RouterModule],
  templateUrl: './employer-job.component.html',
  styleUrls: ['./employer-job.component.css']
})
export class EmployerJobComponent implements OnInit {
  jobs: Job[] = [];
  employerProfileId: number | null = null;
  loading = true;
  errorMessage: string | null = null;

  constructor(private jobService: EmployerProfileService, private authService: AuthService,
    private router: Router, // Inject Router for navigation
    private sweetAlertService: SweetAlertService
    ) {}

  ngOnInit(): void {
    // Get the employer profile ID from AuthService
    this.employerProfileId = Number(this.authService.getProfileId());

    if (this.employerProfileId) {
      // Fetch the jobs for the employer
      this.jobService.getJobsByEmployerProfileId(this.employerProfileId).subscribe(
        (jobs: Job[]) => {
          if (jobs && jobs.length > 0) {
            this.jobs = jobs;
          } else {
            this.errorMessage = 'No jobs found for this employer.';
          }
          this.loading = false;
        },
        (error) => {
          this.sweetAlertService.showWarning('No jobs found for this employer.', 'Warning');

          this.errorMessage = 'No jobs found for this employer.Please create new job!';
          
          this.loading = false;
        }
      );
    } else {
      this.errorMessage = 'Employer profile ID is not available.';
      this.loading = false;
    }
  }
  getJobStatusClass(status: string): string {
    switch (status.toLowerCase()) {
      case 'open':
        return 'open';
      case 'closed':
        return 'closed';

      default:
        return ''; // Fallback for unknown statuses
    }
  }



  

  
}
