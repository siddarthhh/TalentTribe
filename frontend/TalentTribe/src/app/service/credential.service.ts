import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class CredentialService {
  private apiUrl = 'https://localhost:7188/api/Credentials'; // Ensure correct API URL

  constructor(private http: HttpClient, private authService: AuthService) {}

  uploadCredential(credentialName: string, issuedBy: string, issueDate: string, credentialType: string, file: File): Observable<any> {
    const formData = new FormData();
    formData.append('CredentialName', credentialName);
    formData.append('IssuedBy', issuedBy);
    formData.append('IssueDate', issueDate);
    formData.append('CredentialType', credentialType);
    formData.append('JobSeekerProfileId', this.authService.getProfileId() || '');
    formData.append('file', file);

    return this.http.post(`${this.apiUrl}/upload`, formData);
  }

  // Fetch credentials for the logged-in job seeker
  getJobSeekerCredentials(profileId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/jobseeker/${profileId}`);
  }

  // Download a credential PDF
  downloadCredential(id: number): Observable<Blob> {
    return this.http.get(`${this.apiUrl}/download/${id}`, { responseType: 'blob' });
  }
  // Delete a credential by its ID
deleteCredential(id: number): Observable<any> {
  return this.http.delete(`${this.apiUrl}/${id}`);
}
}
