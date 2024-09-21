import { Component, OnInit } from '@angular/core';
import { JobSeekerProfileService } from '../../service/job-seeker-profile.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-manage-jobseeker',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './manage-jobseeker.component.html',
  styleUrl: './manage-jobseeker.component.css'
})
export class ManageJobseekerComponent implements OnInit {

  jobSeekers: any[] = [CommonModule,FormsModule];

  constructor(private jobSeekerService: JobSeekerProfileService) { }

  ngOnInit(): void {
    this.loadJobSeekers();
  }

  // Load job seekers from API
  loadJobSeekers(): void {
    this.jobSeekerService.getJobSeekers().subscribe(data => {
      this.jobSeekers = data;
    });
  }

  // Delete a job seeker
  deleteJobSeeker(id: number): void {
    if (confirm('Are you sure you want to delete this job seeker?')) {
      this.jobSeekerService.deleteJobSeeker(id).subscribe(() => {
        this.loadJobSeekers(); // Reload the list after deletion
      });
    }
  }
}