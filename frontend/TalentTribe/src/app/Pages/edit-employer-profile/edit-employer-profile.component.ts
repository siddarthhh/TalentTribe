import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { AuthService } from '../../service/auth.service';
import { EmployerProfileService } from '../../service/employer-profile.service';
import { CommonModule } from '@angular/common';
import { SweetAlertService } from '../../service/sweet-alert.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-edit-employer-profile',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './edit-employer-profile.component.html',
  styleUrl: './edit-employer-profile.component.css'
})
export class EditEmployerProfileComponent implements OnInit {
  employerProfile: any = {}; // Holds the employer profile data
  profileId: string | null = null; // EmployerProfileId

  constructor(
    private employerProfileService: EmployerProfileService,
    private authService: AuthService
    ,private sweetAlertService: SweetAlertService,
    private router: Router

  ) {}

  ngOnInit(): void {
    this.profileId = this.authService.getProfileId(); // Fetch ProfileId from JWT token
    if (this.profileId) {
      this.getEmployerProfile(); // Fetch employer profile if ProfileId exists
    }
  }

  // Fetch the employer profile by ID
  getEmployerProfile(): void {
    this.employerProfileService.getProfileById(this.profileId!).subscribe(
      (profile) => {
        this.employerProfile = profile; // Bind the fetched profile to the form model
      },
      (error) => {
        alert('Error fetching employer profile:'+ error);
      }
    );
  }

  // Handle form submission
  onSubmit(form: NgForm): void {
    if (form.valid) {
      this.employerProfileService.updateProfile(this.profileId!, this.employerProfile).subscribe(
        (response) => {
          
        this.sweetAlertService.showSuccess('Profile updated successfully','Success')
        this.router.navigate(['/employer-dashboard']);


        },
        (error) => {
          alert('Error updating profile:'+ error);
        }
      );
    }
  }
}