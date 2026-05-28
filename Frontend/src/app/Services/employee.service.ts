import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Employee } from '../Models/employee.model';
import { UpdateEmployeeDto } from '../Models/UpdateEmployeeDto';

@Injectable({ providedIn: 'root' })
export class EmployeeService {

  private api = 'http://localhost:5047/api/employees';

  employees = signal<Employee[]>([]);

  constructor(private http: HttpClient) {}

  loadEmployees() {
    this.http.get<Employee[]>(this.api)
      .subscribe(data => this.employees.set(data));
  }

  getById(id: string) {
    return this.http.get<Employee>(`${this.api}/${id}`);
  }

  create(employee: Employee) {
    return this.http.post(this.api, employee);
  }

 update(id: string, dto: UpdateEmployeeDto) {
  return this.http.put(`${this.api}/${id}`, dto);
}
  delete(id: string) {
    return this.http.delete(`${this.api}/${id}`);
  }

  clear() {
    this.employees.set([]);
  }
}