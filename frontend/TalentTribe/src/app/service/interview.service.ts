import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Interview } from '../models/interview.model'; // Adjust the path to your model

@Injectable({
  providedIn: 'root'
})
export class InterviewService {
  private apiUrl = 'https://localhost:7188/api/Interviews'; // Replace with your API endpoint

  constructor(private http: HttpClient) {}

  // Fetch interview details by ApplicationId
  getInterviewByApplicationId(applicationId: number): Observable<Interview> {
    return this.http.get<Interview>(`${this.apiUrl}/application/${applicationId}`);
  }

  // Create a new interview
  createInterview(interview: Interview): Observable<Interview> {
    return this.http.post<Interview>(this.apiUrl, interview);
  }
  // Update an existing interview
  updateInterview(interview: Interview): Observable<Interview> {
    return this.http.put<Interview>(`${this.apiUrl}/${interview.interviewId}`, interview);
  }
}
