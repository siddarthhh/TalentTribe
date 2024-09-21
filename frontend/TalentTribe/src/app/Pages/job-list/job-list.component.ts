import { Component, OnInit } from '@angular/core';
import { Job } from '../../models/job.model';
import { JobService } from '../../service/job.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Company } from '../../models/company.model';

@Component({
  selector: 'app-job-list',
  standalone: true,
  imports: [CommonModule, RouterModule,FormsModule],
  templateUrl: './job-list.component.html',
  styleUrl: './job-list.component.css'
})
export class JobListComponent implements OnInit {

  jobs: Job[] = [];
  filteredJobs: Job[] = [];
  company: Company | null = null;
  errorMessage: string = '';  // For displaying validation error message

  filters = {
    jobTitle: '',
    experienceLevel: '',
    companyName: '',  // Fixed typo
    minSalary: 0,
    maxSalary: Infinity,
    location: '',
    employerProfileId: null,
    employmentType: '',
    jobStatus: ''
  };
  

  constructor(private jobService: JobService) { }

  ngOnInit(): void {
    this.loadJobs();
  }

  loadJobs(): void {
    this.jobService.getJobs().subscribe(
      (data: Job[]) => {
        this.jobs = data;
        this.filteredJobs = data; // Initialize with all jobs
      },
      (error) => {
        console.error('Error fetching job data', error);
      }
    );
  }
  applyFilters(): void {
    // Validate salary range first
    if (this.filters.minSalary > this.filters.maxSalary) {
      this.errorMessage = 'Maximum salary cannot be less than minimum salary.';
      return;
    }
  
    // Clear any previous error messages if validation passes
    this.errorMessage = '';
  
    // Filter jobs based on the provided filters
    this.filteredJobs = this.jobs.filter(job => {
      const salary = this.parseSalaryRange(job.salaryRange);
      const minSalary = this.filters.minSalary || 0;
      const maxSalary = this.filters.maxSalary || Infinity;
  
      return (
        (!this.filters.jobTitle || job.jobTitle.toLowerCase().includes(this.filters.jobTitle.toLowerCase())) &&
        (!this.filters.experienceLevel || job.experienceLevel === this.filters.experienceLevel) &&
        (!this.filters.companyName || job.companyName.toLowerCase().includes(this.filters.companyName.toLowerCase())) &&  // New filter logic for company name
        (!this.filters.location || job.location.toLowerCase().includes(this.filters.location.toLowerCase())) &&
        (!this.filters.employerProfileId || job.employerProfileId === this.filters.employerProfileId) &&
        (!this.filters.employmentType || job.employmentType === this.filters.employmentType) &&
        (!this.filters.jobStatus || job.jobStatus === this.filters.jobStatus) &&
        (salary.min >= minSalary && salary.max <= maxSalary)
      );
    });
  }
  
  parseSalaryRange(range: string): { min: number; max: number } {
    const cleanRange = range.replace(/[$,\s]/g, '');  // Remove $, commas, and spaces
    const [minStr, maxStr] = cleanRange.split('-').map(str => str.trim());

    const min = Number(minStr);
    const max = Number(maxStr);

    if (isNaN(min) || isNaN(max)) {
      console.error('Invalid salary range:', range);
      return { min: 0, max: 0 };
    }

    return { min, max };
  }
}