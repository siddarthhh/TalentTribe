import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Interview } from '../../models/interview.model';
import { InterviewService } from '../../service/interview.service';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { SweetAlertService } from '../../service/sweet-alert.service';

@Component({
  selector: 'app-interview-details',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './interview-details.component.html',
  styleUrl: './interview-details.component.css'
})


export class InterviewDetailsComponent implements OnInit {
  interview: Interview | null = null;
  applicationId!: number; // Store the applicationId
  errorMessage: string = '';
  loading: boolean = true;
  isEditing: boolean = false; // Track if in edit mode

  interviewDateTime: string = ''; // For datetime-local input
  minDateTime: string| undefined; // For the min attribute in datetime-local

  editedInterview: Interview = {
    interviewId: 0,
    applicationId: 0,
    interviewDate: '',
    interviewType: '',
    interviewLink: '',
    interviewLocation: '',
    interviewFeedback: ''
  };

  constructor(
    private route: ActivatedRoute,
    private interviewService: InterviewService,
    private router: Router // Inject Router
    ,private sweetAlertService: SweetAlertService
  ) {}

  ngOnInit(): void {
    this.applicationId = +this.route.snapshot.paramMap.get('ApplicationId')!; // Convert the ID to number

    if (this.applicationId) {
      this.fetchInterviewDetails(this.applicationId);
    } else {
      this.errorMessage = 'No application ID provided';
      this.loading = false;
    }

    // Set minimum datetime to current date and time
    this.minDateTime = new Date().toISOString().slice(0, 16); // YYYY-MM-DDTHH:MM
  }

  // Fetch interview details using the application ID
  fetchInterviewDetails(applicationId: number): void {
    this.interviewService.getInterviewByApplicationId(applicationId).subscribe({
      next: (interview) => {
        this.interview = interview;
        this.loading = false;

        if (interview.interviewDate) {
          // Convert Date object to YYYY-MM-DDTHH:MM format for datetime-local input
          this.interviewDateTime = interview.interviewDate;
        }

        this.editedInterview = { ...interview };
      },
      error: (err) => {
        if (err.status === 404) {
          this.interview = null; // No interview exists, show the form to create one
          this.loading = false;
        } else {
          this.errorMessage = 'Error loading interview details: ' + err.message;
          this.loading = false;
        }
      }
    });
  }

  // Method to toggle edit mode
  toggleEdit(): void {
    this.isEditing = !this.isEditing;
  }

  // Method to create a new interview
  createInterview(form: NgForm): void {
    if (form.valid) {
      this.editedInterview.applicationId = this.applicationId; // Set applicationId
      this.editedInterview.interviewDate = this.interviewDateTime; // Set the combined date and time
      this.interviewService.createInterview(this.editedInterview).subscribe({
        next: (createdInterview) => {
          this.sweetAlertService.showSuccess('Interview created Successfully','Success')
          this.isEditing = false; // Exit edit mode
          this.fetchInterviewDetails(this.applicationId);
          this.navigateToInterviewDetails(createdInterview.applicationId); 

          // Navigate to interview details
        },
        error: (err) => {
          this.errorMessage = 'Error creating interview: ' + err.message;
        }
      });
    }
  }


  // Method to update the interview
  updateInterview(form: NgForm): void {
    if (form.valid) {
      this.editedInterview.interviewDate = this.interviewDateTime; // Set the combined date and time
      this.interviewService.updateInterview(this.editedInterview).subscribe({
        next: (updatedInterview) => {
          this.isEditing = false; // Exit edit mode
          this.sweetAlertService.showSuccess('Interview edited Successfully','Success')

          this.fetchInterviewDetails(this.applicationId);
          this.navigateToInterviewDetails(updatedInterview.applicationId); 
          
          // Navigate to interview details
        },
        error: (err) => {
          this.errorMessage = 'Error updating interview: ' + err.message;
        }
      });
    }
  }

  // Method to navigate to interview details page
  private navigateToInterviewDetails(applicationId: number): void {
    this.router.navigate([`/interview-details/${applicationId}`]); // Reload the details view
  }

  
}
