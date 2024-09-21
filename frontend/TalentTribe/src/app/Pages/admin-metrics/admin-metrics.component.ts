import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../service/admin.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-admin-metrics',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './admin-metrics.component.html',
  styleUrl: './admin-metrics.component.css'
})
export class AdminMetricsComponent implements OnInit {
  companiesWithMostJobs: any[] = [];
  employersWithMostJobs: any[] = [];
  constructor(private adminService: AdminService) { }

  ngOnInit(): void {
    this.loadJobMetrics();
    this.loadEmployerMetrics();
  }

  loadJobMetrics(): void {
    this.adminService.getCompaniesWithMostJobs().subscribe((data: any[]) => {
      this.companiesWithMostJobs = data;
    });
  }


  loadEmployerMetrics(): void {
    this.adminService.getEmployersWithMostJobs().subscribe((data: any[]) => {
      this.employersWithMostJobs = data;
    });
  }
}