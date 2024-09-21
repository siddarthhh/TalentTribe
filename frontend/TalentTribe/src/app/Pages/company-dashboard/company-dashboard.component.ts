import { Component } from '@angular/core';
import { AuthService } from '../../service/auth.service';
import { CompanyService } from '../../service/company.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { SweetAlertService } from '../../service/sweet-alert.service';

@Component({
  selector: 'app-company-dashboard',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './company-dashboard.component.html',
  styleUrl: './company-dashboard.component.css'
})
export class CompanyDashboardComponent {
  company: any = {};
  loading = true;
  error: string | null = null;

  constructor(private authService: AuthService, private companyService: CompanyService ,private sweetAlertService: SweetAlertService,
    private router: Router) {}

  ngOnInit(): void {
    const profileId = this.authService.getProfileId();
    
    if (profileId) {
      // Typecasting profileId to integer
      const profileIdAsNumber = Number(profileId);

      this.companyService.getCompany(profileIdAsNumber).subscribe(
        data => {
          this.company = data;
          this.loading = false;
        },
        err => {
          this.sweetAlertService.showError("Please update your company details ", 'Error');

          this.loading = false;
        }
      );
    } else {
      this.error = 'No profile ID found.';
      this.loading = false;
    }
  }

  saveCompanyDetails(): void {
    const profileId = this.authService.getProfileId();
    
    if (profileId) {
      // Typecasting profileId to integer
      const profileIdAsNumber = Number(profileId);

      this.companyService.upsertCompany(profileIdAsNumber, this.company).subscribe(
        data => {
          this.company = data;
          this.sweetAlertService.showSuccess('Company updated successfully','Success')
          this.router.navigate(['/employer-dashboard']);
        },
        err => {
          this.sweetAlertService.showError("Error saving company details.", 'Error');

        }
      );
    }
  }
}