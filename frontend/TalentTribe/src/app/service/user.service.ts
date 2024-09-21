import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user.model'; // User model interface

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'https://localhost:7188/api/Users/'; // Replace with your actual backend URL

  constructor(private http: HttpClient) {}

  // Method to register a new user
  registerUser(user: User): Observable<User> {
    return this.http.post<User>(`${this.apiUrl}`, user);
  }
}
