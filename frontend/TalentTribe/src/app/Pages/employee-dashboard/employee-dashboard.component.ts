import { Component, OnInit } from '@angular/core';
import { EmployerProfile } from '../../models/employer-profile.model';
import { EmployerProfileService } from '../../service/employer-profile.service';
import { AuthService } from '../../service/auth.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { SweetAlertService } from '../../service/sweet-alert.service';

@Component({
  selector: 'app-employee-dashboard',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './employee-dashboard.component.html',
  styleUrl: './employee-dashboard.component.css'
})
export class EmployeeDashboardComponent implements OnInit {

  navigateToEditProfile(): void {
    this.router.navigate(['/edit-employer-profile']);
  }

  employerProfile: EmployerProfile | null = null;
  loading = true;
  error = '';

  constructor(
    private router: Router,
    private employerProfileService: EmployerProfileService,
    private authService: AuthService
    ,private sweetAlertService: SweetAlertService
  ) {}

  ngOnInit(): void {
    const profileId = this.authService.getProfileId();
    if (profileId) {
      this.loadEmployerProfile(profileId);
    } else {
      this.error = 'No Employer Profile ID found.';
      this.loading = false;
    }
  }
  needsProfileUpdate(profile: EmployerProfile): boolean {
    // Add conditions as per your requirement
    return !profile.fullName || !profile.positionTitle || !profile.department || !profile.workEmail || !profile.workPhone;
  }
  
  // Show profile update warning
  showProfileUpdateWarning(): void {
    this.sweetAlertService.showWarning('Please update your profile', 'Warning');
  }
  loadEmployerProfile(profileId: string): void {
    this.employerProfileService.getEmployerProfileById(profileId).subscribe({
      next: (profile) => {
        this.employerProfile = profile;
        this.loading = false;
        // Inside your profile loading logic
if (this.needsProfileUpdate(profile)) {
  this.showProfileUpdateWarning();
}
      },
      error: (err) => {
        this.error = 'Error loading employer profile';
        console.error(err);
        this.loading = false;
      }
    });
  }
}