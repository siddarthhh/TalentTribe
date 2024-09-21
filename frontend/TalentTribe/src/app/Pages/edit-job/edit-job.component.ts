import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Job } from '../../models/job.model';
import { JobService } from '../../service/job.service';
import { CommonModule } from '@angular/common';
import { SweetAlertService } from '../../service/sweet-alert.service';

@Component({
  selector: 'app-edit-job',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './edit-job.component.html',
  styleUrl: './edit-job.component.css'
})
export class EditJobComponent implements OnInit {
  job: Job | null = null;
  jobId!: number;
  errorMessage: string = '';
  loading: boolean = true;
  minDate: string|undefined;


  constructor(
    private route: ActivatedRoute,
    private jobService: JobService,
    private router: Router
    ,private sweetAlertService: SweetAlertService
  ) {}

  ngOnInit(): void {
    this.jobId = +this.route.snapshot.paramMap.get('id')!; // Fetch job ID from URL
    if (this.jobId) {
      this.fetchJobDetails(this.jobId);
    }
    const today = new Date();
    this.minDate = today.toISOString().split('T')[0];
  }

  // Fetch job details by jobId
  fetchJobDetails(jobId: number): void {
    this.jobService.getJobById(jobId).subscribe({
      next: (job: Job) => {
        this.job = job;
        this.loading = false;
      },
      error: (err) => {
        this.errorMessage = 'Error fetching job details: ' + err.message;
        this.loading = false;
      }
    });
  }

  // Update job
  updateJob(form: NgForm): void {
    if (form.valid && this.job) {
      this.jobService.updateJob(this.jobId, this.job).subscribe({
        next: (updatedJob) => {
          this.sweetAlertService.showSuccess('Job updated successfully:','Success')
          this.router.navigate(['/employer-job']); // Redirect after update
        },
        error: (err) => {
          this.errorMessage = 'Error updating job: ' + err.message;
        }
      });
    }
  }
}
