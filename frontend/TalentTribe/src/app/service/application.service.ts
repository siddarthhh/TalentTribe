import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Application, ApplicationUpdatePayload } from '../models/application.model';

@Injectable({
  providedIn: 'root'
})
export class ApplicationService {
  private apiUrl = 'https://localhost:7188/api/Applications'; // Adjust to your actual API URL

  constructor(private http: HttpClient) { }
// Get all applications
getApplications(): Observable<any[]> {
  return this.http.get<any[]>(`${this.apiUrl}/manage`);
}

// Delete an application
deleteApplication(id: number): Observable<any> {
  return this.http.delete(`${this.apiUrl}/${id}`);
}
  createApplication(application: Application): Observable<Application> {
    return this.http.post<Application>(this.apiUrl, application);
  }

    // Get Application by ID
    getApplicationById(id: number): Observable<Application> {
      return this.http.get<Application>(`${this.apiUrl}/Apply/${id}`);
    }
  
    updateApplication(applicationId: number, updatedApplication:ApplicationUpdatePayload ): Observable<void> {
      return this.http.put<void>(`${this.apiUrl}/${applicationId}`, updatedApplication);
    }
    

  
}
