import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface Department {
  id: string;
  departmentName: string;
  employees?: any[];
}

@Injectable({ providedIn: 'root' })
export class DepartmentService {
  private api = 'http://localhost:5047/api/departments';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Department[]> {
    return this.http.get<Department[]>(this.api);
  }
}