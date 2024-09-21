import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AuthService } from '../../service/auth.service';  // Adjust the path as needed
import { CommonModule } from '@angular/common';
import { login } from '../../service/login';
import { Router } from '@angular/router';
import { SweetAlertService } from '../../service/sweet-alert.service';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [HttpClientModule,CommonModule,FormsModule],  // Import necessary modules
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  user :login= {
    Username: '',
    PasswordHash: ''
  };

  constructor(private authService: AuthService, private router: Router ,private sweetAlertService: SweetAlertService) { }

  onSubmit(form: NgForm) {
    if (this.user.Username && this.user.PasswordHash) {
      this.authService.login(this.user).subscribe(
        data => {
         const token = data?.token;
          if (token) {
            localStorage.setItem('token', token); // Store the token directly
            const role = this.authService.getUserRole()
            if(role==='Employer'){
              this.sweetAlertService.showSuccess('Welcome! Employer login successful!','Success')

              this.router.navigate(['/employer-dashboard']); 
            }
            else if (role === 'JobSeeker'){
              this.sweetAlertService.showSuccess('Welcome! JobSeeker login successful!','Success')

              this.router.navigate(['/job-list']); 
            }
            else if (role === 'admin'){
              this.sweetAlertService.showSuccess('Welcome! Admin login successful!','Success')
              this.router.navigate(['/admin-metrics']); 
            }
          } else {
            this.sweetAlertService.showError("Failed to retrieve token.", 'Error');
          }        
        },
        error => {
            this.sweetAlertService.showError("Invalid login details or server error", 'Error');

        }
      );
    } else {
      this.sweetAlertService.showError("Please enter username and password first", 'Error');
      
    }
    
  }

  
}
