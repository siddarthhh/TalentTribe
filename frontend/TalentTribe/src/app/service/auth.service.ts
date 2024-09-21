import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Router } from '@angular/router';
import { Token } from './token';
import { login } from './login';
import {jwtDecode} from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7188/api/Users/login'; // Ensure correct API URL
  private tokenKey = 'token'; // Use the correct key for localStorage
  private userRoleSubject = new BehaviorSubject<string>('');
  userRole$ = this.userRoleSubject.asObservable();

  private userIdSubject = new BehaviorSubject<string | null>(null);
  userId$ = this.userIdSubject.asObservable();

  private profileIdSubject = new BehaviorSubject<string | null>(null);
  profileId$ = this.profileIdSubject.asObservable();

  private usernameSubject = new BehaviorSubject<string | null>(null);
  username$ = this.usernameSubject.asObservable();

  constructor(private http: HttpClient, private router: Router) 
  { 
    this.setUserDetailsFromToken();
  }

  login(login: login): Observable<Token> {
    return this.http.post<Token>(this.apiUrl, login).pipe(
      tap(response => {
        localStorage.setItem(this.tokenKey, response.token); // Store the token in localStorage
        this.setUserDetailsFromToken(); // Update user details
        this.router.navigate(['/']); // Navigate to home or dashboard
      })
    );
  }

  private setUserDetailsFromToken(): void {
    const token = this.getToken();
    if (token) {
      try {
        const decodedToken: any = jwtDecode(token);
        this.userRoleSubject.next(decodedToken.Role || '');
        this.userIdSubject.next(decodedToken.UserId || null);
        this.profileIdSubject.next(decodedToken.ProfileId || null);
        this.usernameSubject.next(decodedToken.sub || null); // sub is typically the username

      

      } catch (e) {
        console.error('Error decoding token:', e);
        this.userRoleSubject.next('');
        this.userIdSubject.next(null);
        this.profileIdSubject.next(null);
        this.usernameSubject.next(null);
      }
    } else {
      this.userRoleSubject.next('');
      this.userIdSubject.next(null);
      this.profileIdSubject.next(null);
      this.usernameSubject.next(null);
    }
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  isAuthenticated(): boolean {
    const token = this.getToken();
    return !!token;
  }

  hasRole(requiredRole: string): boolean {
    const token = this.getToken();
    if (!token) return false;
    try {
      const decodedToken: any = jwtDecode(token);
      return decodedToken.Role === requiredRole;
    } catch (e) {
      console.error('Error decoding token:', e);
      return false;
    }
  }

  getUserRole(): string {
    return this.userRoleSubject.value;
  }

  getUserId(): string | null {
    return this.userIdSubject.value;
  }

  getProfileId(): string | null {
    return this.profileIdSubject.value;
  }

  getUsername(): string | null {
    return this.usernameSubject.value;
  }

  isAdmin(): boolean {
    return this.getUserRole() === 'Admin';
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
    this.router.navigate(['/login']);
  }
}
