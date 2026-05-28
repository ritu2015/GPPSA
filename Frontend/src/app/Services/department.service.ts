import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface Department {
  id: string;
  departmentName: string;
  employees?: any[];
}

@Injectable({ providedIn: 'root' })
export class DepartmentService {
  private api = `${environment.apiBaseUrl}/api/departments`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<Department[]> {
    return this.http.get<Department[]>(this.api);
  }
}