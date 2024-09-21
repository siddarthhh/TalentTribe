import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { Application } from '../models/application.model';
import { JobSeekerProfile } from '../models/job-seeker-profile.model';

@Injectable({
  providedIn: 'root'
})
export class JobSeekerProfileService {

  private apiUrl = 'https://localhost:7188/api/JobSeekerProfiles'; // Replace with actual API URL

  constructor(private http: HttpClient) { }

  createJobSeekerProfile(profileData: any): Observable<any> {
    return this.http.post(this.apiUrl, profileData);
  }

  // Get job seeker profile by profileId
  getProfile(profileId: number): Observable<JobSeekerProfile> {
    return this.http.get<JobSeekerProfile>(`${this.apiUrl}/${profileId}`);
  }

  // Update job seeker profile
  updateProfile(profileId: number, formData: FormData): Observable<any> {
    return this.http.put(`${this.apiUrl}/${profileId}`, formData);
  }
  // Fetch applications by JobSeekerProfileId
  getApplicationsByJobSeekerProfileId(jobSeekerProfileId: number): Observable<Application[]> {
    return this.http.get<Application[]>(`${this.apiUrl}/ApplicationByJobseeker/${jobSeekerProfileId}`);
  }
 
  // Fetch the JobSeekerProfile by ID
  getJobSeekerProfile(id: string): Observable<JobSeekerProfile> {
    return this.http.get<JobSeekerProfile>(`${this.apiUrl}/${id}`);
  }

  // Get the URL for the profile picture
  getProfilePictureUrl(profileId: number): string {
    return `${this.apiUrl}/download-profile-picture/${profileId}`;
  }

    // Download resume by profile ID
    downloadResume(profileId: number): Observable<Blob> {
      return this.http.get(`${this.apiUrl}/download-resume/${profileId}`, { responseType: 'blob' })
        .pipe(
          catchError(error => {
            console.error('Error downloading resume:', error);
            return throwError(error);
          })
        );
    }


  // Get all job seekers
  getJobSeekers(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  // Delete a job seeker
  deleteJobSeeker(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
  
}
