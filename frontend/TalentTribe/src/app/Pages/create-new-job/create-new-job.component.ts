import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { JobService } from '../../service/job.service';  // Adjust the path as needed
import { CommonModule } from '@angular/common';
import { AuthService } from '../../service/auth.service';
import { SweetAlertService } from '../../service/sweet-alert.service';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-create-new-job',
  standalone: true,
  imports: [CommonModule,FormsModule],  // Include Angular modules if needed
  templateUrl: './create-new-job.component.html',
  styleUrls: ['./create-new-job.component.css']
})
export class CreateNewJobComponent implements OnInit {
  today: string | undefined; // New property for today's date
  job = {
    employerProfileId: 0,
    jobTitle: '',
    jobDescription: '',
    requiredSkills: '',
    employmentType: '',
    salaryRange: '',
    location: '',
    jobStatus: '',
    applicationDeadline: '',
    experienceLevel: '',
    companyName: '' // Prefill with company name
  };

  salaryCurrency: string = '$'; // Default to USD
  minSalary: number | null = null;
  maxSalary: number | null = null;

  constructor(
    private router: Router,
    private jobService: JobService,
    private authService: AuthService,
    private sweetAlertService: SweetAlertService,
    
    

  ) {}

  ngOnInit() {
    const options: Intl.DateTimeFormatOptions = {
      timeZone: 'Asia/Kolkata',
      year: 'numeric',
      month: '2-digit',
      day: '2-digit'
    };
    
    const today = new Date().toLocaleDateString('en-IN', options);
    
    // Format it to YYYY-MM-DD
    const [day, month, year] = today.split('/');
    this.today = `${year}-${month}-${day}`; // Set today's date
    // Fetch employerProfileId from authService/local storage
    const employerProfileId = this.authService.getProfileId();
    if (employerProfileId) {
      this.job.employerProfileId = +employerProfileId; // Convert to number
      
      // Fetch the company by employerProfileId and prefill the company name
      this.jobService.getCompanyByEmployerProfileId(this.job.employerProfileId).subscribe(
        (company) => {
          this.job.companyName = company.companyName;
        },
        (error) => {
          alert('Error fetching company details:'+ error);
        }
      );
    }
  }

  // Method to format salary
  formatSalaryRange() {
    if (this.minSalary && this.maxSalary) {
      this.job.salaryRange = `${this.salaryCurrency}${this.formatNumber(this.minSalary)} - ${this.salaryCurrency}${this.formatNumber(this.maxSalary)}`;
    } else {
      this.job.salaryRange = '';
    }
  }

  // Utility method to format the number with commas
  formatNumber(num: number): string {
    return num.toLocaleString(); // Automatically adds commas in appropriate places
  }

  // Ensure formatSalaryRange is called before submitting the form
  onSubmit(form: NgForm) {
    if (form.valid) {
      this.formatSalaryRange(); // Ensure salary range is formatted before submitting
      this.jobService.createJob(this.job).subscribe(response => {
        // Handle successful job creation
        this.sweetAlertService.showSuccess('Job created successfully:','Success')
        this.router.navigate(['employer-job']); 
      }, error => {
        // Handle error
        this.sweetAlertService.showError("Failed to create job.", 'Error');

      });
    }
  }
}
