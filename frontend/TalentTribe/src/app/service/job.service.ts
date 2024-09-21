import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Job } from '../models/job.model';
import { Company } from '../models/company.model';

@Injectable({
  providedIn: 'root'
})
export class JobService {
  private apiUrl = 'https://localhost:7188/api/Jobs';  // Replace with your API endpoint

  constructor(private http: HttpClient) {}

  createJob(job: any): Observable<any> {
    return this.http.post(this.apiUrl, job);
  }

  // Get all jobs
  getJobs(): Observable<Job[]> {
    return this.http.get<Job[]>(this.apiUrl);
  }
  getCompanyByEmployerProfileId(employerProfileId: number): Observable<Company> {
    return this.http.get<Company>(`${this.apiUrl}/companyByEmployerProfileId/${employerProfileId}`);
  }
  
  
  getJob(id: number): Observable<Job> {
    return this.http.get<Job>(`${this.apiUrl}/${id}`);
  }
  // Get a single job by ID
  getJobById(id: number): Observable<Job> {
    return this.http.get<Job>(`${this.apiUrl}/${id}`);
  }



  // Update job
  updateJob(jobId: number, job: Job): Observable<Job> {
    return this.http.put<Job>(`${this.apiUrl}/${jobId}`, job);
  }
}
