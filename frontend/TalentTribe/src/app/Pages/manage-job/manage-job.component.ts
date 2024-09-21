import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../service/admin.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-manage-job',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './manage-job.component.html',
  styleUrl: './manage-job.component.css'
})
export class ManageJobComponent implements OnInit {

  jobs: any[] = [];

  constructor(private jobService: AdminService) { }

  ngOnInit(): void {
    this.loadJobs();
  }

  // Load jobs from the API
  loadJobs(): void {
    this.jobService.getJobs().subscribe(data => {
      this.jobs = data;
    });
  }

  // Delete a job
  deleteJob(id: number): void {
    if (confirm('Are you sure you want to delete this job?')) {
      this.jobService.deleteJob(id).subscribe(() => {
        this.loadJobs(); // Reload the list after deletion
      });
    }
  }
}