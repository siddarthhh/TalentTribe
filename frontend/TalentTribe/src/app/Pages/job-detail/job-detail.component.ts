import { Component, OnInit } from '@angular/core';
import { Job } from '../../models/job.model';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { JobService } from '../../service/job.service';
import { Company } from '../../models/company.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-job-detail',
  standalone: true,
  imports: [CommonModule,RouterModule],
  templateUrl: './job-detail.component.html',
  styleUrl: './job-detail.component.css'
})
export class JobDetailComponent implements OnInit {
  job: Job | null = null;
  company: Company | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private jobService: JobService
  ) {}

  ngOnInit(): void {
    const jobId = this.route.snapshot.paramMap.get('jobId');
    if (jobId) {
      this.loadJobDetails(parseInt(jobId, 10));
    }
  }

  loadJobDetails(jobId: number): void {
    this.jobService.getJobById(jobId).subscribe((job) => {
      this.job = job;
      if (job.employerProfileId) {
        this.jobService.getCompanyByEmployerProfileId(job.employerProfileId).subscribe((company) => {
          this.company = company;
        });
      }
    });
  }

  // This method is triggered when the Apply button is clicked
  applyForJob(): void {
    if (this.job) {
      this.router.navigate(['/application', this.job.jobId]);
    }
  }
}