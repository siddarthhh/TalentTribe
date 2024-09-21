import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EmployerProfile } from '../models/employer-profile.model';
import { EmpApplication } from '../models/application.model';
import { Job } from '../models/job.model';

@Injectable({
  providedIn: 'root'
})
export class EmployerProfileService {
  private apiUrl = 'https://localhost:7188/api/EmployerProfiles';  // Replace with your actual API URL

  constructor(private http: HttpClient) {}


  // Get all employers
  getEmployers(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  // Delete an employer
  deleteEmployer(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }



 // Fetch employer profile by ID
 getProfileById(profileId: string): Observable<any> {
  return this.http.get<any>(`${this.apiUrl}/${profileId}`);
}

// Update employer profile
updateProfile(profileId: string, profileData: any): Observable<any> {
  return this.http.put(`${this.apiUrl}/${profileId}`, profileData);
}
getEmployerProfileById(profileId: string): Observable<EmployerProfile> {
  return this.http.get<EmployerProfile>(`${this.apiUrl}/${profileId}`);
}
  
  createOrUpdateEmployerProfile(employerProfile: any): Observable<any> {
    if (employerProfile.employerProfileId) {
      // If the employerProfileId exists, update the profile
      return this.http.put(`${this.apiUrl}/${employerProfile.employerProfileId}`, employerProfile);
    } else {
      // If employerProfileId is not provided, create a new profile
      return this.http.post(this.apiUrl, employerProfile);
    }
  }



  getJobsByEmployerProfileId(employerProfileId: number): Observable<Job[]> {
    return this.http.get<Job[]>(`${this.apiUrl}/JobsOfEmployer/${employerProfileId}`);
  }
  getApplicationsByEmployerProfileId(employerProfileId: number): Observable<EmpApplication[]> {
    return this.http.get<EmpApplication[]>(`${this.apiUrl}/applicationsByEmployer/${employerProfileId}`);
  }
}
