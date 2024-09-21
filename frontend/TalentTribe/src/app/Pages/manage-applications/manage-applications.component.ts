import { Component, OnInit } from '@angular/core';
import { ApplicationService } from '../../service/application.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-manage-applications',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './manage-applications.component.html',
  styleUrl: './manage-applications.component.css'
})
export class ManageApplicationsComponent implements OnInit {

  applications: any[] = [];

  constructor(private applicationService: ApplicationService) { }

  ngOnInit(): void {
    this.loadApplications();
  }

  // Load applications from API
  loadApplications(): void {
    this.applicationService.getApplications().subscribe(data => {
      this.applications = data;
    });
  }

  // Delete an application
  deleteApplication(id: number): void {
    if (confirm('Are you sure you want to delete this application?')) {
      this.applicationService.deleteApplication(id).subscribe(() => {
        this.loadApplications(); // Reload the list after deletion
      });
    }
  }
}