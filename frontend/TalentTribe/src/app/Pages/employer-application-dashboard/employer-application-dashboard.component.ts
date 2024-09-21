import { Component, OnInit } from '@angular/core';
import { Application, EmpApplication } from '../../models/application.model';
import { ApplicationService } from '../../service/application.service';
import { AuthService } from '../../service/auth.service';
import { EmployerProfileService } from '../../service/employer-profile.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { SweetAlertService } from '../../service/sweet-alert.service';

@Component({
  selector: 'app-employer-application-dashboard',
  standalone: true,
  imports: [FormsModule,CommonModule,RouterModule],
  templateUrl: './employer-application-dashboard.component.html',
  styleUrl: './employer-application-dashboard.component.css'
})export class EmployerApplicationDashboardComponent implements OnInit {
  applications: EmpApplication[] = [];
  loading = true;
  errorMessage = '';
  constructor(
    private applicationService: EmployerProfileService,
    private authService: AuthService,
    private router: Router// Inject Router,
    ,private sweetAlertService: SweetAlertService
  ) { }
  ngOnInit(): void {
    const employerProfileId = this.authService.getProfileId();

    if (employerProfileId) {
      this.loadApplications(parseInt(employerProfileId, 10));
    } else {
      this.errorMessage = 'Employer not logged in';
      this.loading = false;
    }
  }

  loadApplications(employerProfileId: number): void {
    this.applicationService.getApplicationsByEmployerProfileId(employerProfileId)
      .subscribe({
        next: (applications) => {
          this.applications = applications;
          this.loading = false;
        },
        error: (err) => {
         this.sweetAlertService.showWarning('You have zero aplications', 'Sorry');

          this.errorMessage = 'You have zero aplications ' ;
          this.loading = false;
        }
      });
  }
  getStatusClass(status: string): string {
    switch (status.toLowerCase()) {
      case 'approved':
        return 'approved';
      case 'pending':
        return 'pending';
      case 'rejected':
        return 'rejected';
      default:
        return ''; // Fallback in case of unknown status
    }
  }

 // Method to navigate to the interview details page
 viewInterviewDetails(applicationId: number): void {
  this.router.navigate([`/interview-details/${applicationId}`]);
}
  // Method to navigate to the details page
  viewDetails(applicationId: number, jobSeekerProfileId: number): void {
    this.router.navigate([`/appliedjobseekerdetails/${applicationId}/${jobSeekerProfileId}`]);
  }

}