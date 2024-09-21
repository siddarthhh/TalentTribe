import { Component, OnInit } from '@angular/core';
import { EmployerProfileService } from '../../service/employer-profile.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-manage-employer',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './manage-employer.component.html',
  styleUrl: './manage-employer.component.css'
})
export class ManageEmployerComponent implements OnInit {

  employers: any[] = [];

  constructor(private employerService: EmployerProfileService) { }

  ngOnInit(): void {
    this.loadEmployers();
  }

  // Load employers from API
  loadEmployers(): void {
    this.employerService.getEmployers().subscribe(data => {
      this.employers = data;
    });
  }

  // Delete an employer
  deleteEmployer(id: number): void {
    if (confirm('Are you sure you want to delete this employer?')) {
      this.employerService.deleteEmployer(id).subscribe(() => {
        this.loadEmployers(); // Reload the list after deletion
      });
    }
  }
}
