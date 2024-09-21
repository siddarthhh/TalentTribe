import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private apiUrl = 'https://localhost:7188/api/Admins'; // Adjust to your actual API URL

  constructor(private http: HttpClient) { }

   // Get all jobs
   getJobs(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/job`);
  }

  // Delete a job
  deleteJob(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/job/${id}`);
  }



  getCompaniesWithMostJobs(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/CompaniesWithMostJobs`);
  }


  getEmployersWithMostJobs(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/EmployersWithMostJobs`);
  }

}
