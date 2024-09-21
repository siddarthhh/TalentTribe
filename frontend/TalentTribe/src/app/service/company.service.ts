import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {
  private apiUrl = `https://localhost:7188/api/Companies`;

  constructor(private http: HttpClient) {}

  getCompany(employerProfileId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/byEmployeeProfileId${employerProfileId}`);
  }

  upsertCompany(employerProfileId: number, company: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${employerProfileId}`, company);
  }
}
