import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../service/auth.service';
import { NgIf, NgTemplateOutlet } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [NgIf, NgTemplateOutlet, RouterLink],
  templateUrl: './navbar.component.html',
})
export class NavbarComponent {
  constructor(private authService: AuthService, private router: Router) {}

  isAuthenticated(): boolean {
    return this.authService.isAuthenticated();
  }

  isJobSeeker(): boolean {
    return this.authService.hasRole('JobSeeker');
  }

  isEmployer(): boolean {
    return this.authService.hasRole('Employer');
  }

  isAdmin(): boolean {
    return this.authService.hasRole('admin');
  }

  getUserRole(): string {
    return this.authService.getUserRole();
  }

  getUsername(): string | null {
    return this.authService.getUsername();
  }

  goToLogin(): void {
    this.router.navigate(['/login']);
  }

  goToRegister(): void {
    this.router.navigate(['/register']);
  }

  logout(): void {
    this.authService.logout();
  }
}
