import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { AuthInterceptor } from './auth.interceptor';
import { AuthService } from './service/auth.service'; 
import { CommonModule } from '@angular/common';
import { NavbarComponent } from "./Pages/navbar/navbar.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, NavbarComponent],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true, // Ensures that multiple interceptors can be used
    },
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']  // Fixed the property name from `styleUrl` to `styleUrls`
})
export class AppComponent {
  title = 'TalentTribe';
  constructor(private authService: AuthService, private router: Router) {}

  isAuthenticated(): boolean {
    return this.authService.isAuthenticated();
  }

}
