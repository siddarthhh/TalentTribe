import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms'; // Import FormsModule for template-driven forms
import { HttpClientModule } from '@angular/common/http'; // Import HttpClientModule for API calls
import { UserService } from '../../service/user.service'; // Ensure this service is created
import { SweetAlertService } from '../../service/sweet-alert.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  standalone: true,
  imports: [CommonModule, FormsModule, HttpClientModule], // Import necessary modules
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css'],
  providers: [UserService] // Include UserService for API interactions
})
export class RegistrationComponent {
  user = {
    username: '',
    passwordHash: '',
    confirmPassword: '', // For password confirmation
    role: '',
    email: '',
    phoneNumber: ''
  };

  // To toggle show/hide passwords
  showPassword = false;
  showConfirmPassword = false;

  constructor(private userService: UserService,private sweetAlertService: SweetAlertService,    private router: Router) {}

  // Function to submit the registration form
  onSubmit(form: NgForm) {
    if (form.valid && this.user.passwordHash === this.user.confirmPassword && this.user.role) {
      this.userService.registerUser(this.user).subscribe({
        next: (response) => {
          this.sweetAlertService.showSuccess('User registered successfully','Success')

          form.reset();
          this.router.navigate(['/login']); // Redirect to login
        },
        error: (error) => {
          this.sweetAlertService.showError('There was an error registering the user: '+ error.error.message, 'Error');
        }
      });
    }
  }

  // Function to dynamically set role
  setRole(role: string) {
    this.user.role = role;
  }

  // Function to toggle password visibility
  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }

  // Function to toggle confirm password visibility
  toggleConfirmPasswordVisibility() {
    this.showConfirmPassword = !this.showConfirmPassword;
  }
}