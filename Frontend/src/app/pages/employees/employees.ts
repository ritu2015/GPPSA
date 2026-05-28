import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeService } from '../../Services/employee.service';
import { Employee } from '../../Models/employee.model';
import { Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';

@Component({
  standalone: true,
  imports: [CommonModule, MatButtonModule],
  template: `
  <div class="p-6">

    <div class="flex justify-between items-center mb-6">
      <h2 class="text-2xl font-semibold">Employees</h2>

      <div class="space-x-3">
        <button mat-raised-button color="primary" (click)="add()">Add Employee</button>
        <button mat-stroked-button color="warn" (click)="logout()">Logout</button>
      </div>
    </div>

    <table class="w-full border rounded-lg overflow-hidden">
      <thead class="bg-gray-100">
        <tr>
          <th class="p-3 text-left">Code</th>
          <th>Name</th>
          <th>Department</th>
          <th>Salary</th>
          <th class="text-center">Actions</th>
        </tr>
      </thead>

      <tbody>
        <tr *ngFor="let e of service.employees()" class="border-t">
          <td class="p-3">{{e.employeeCode}}</td>
          <td>{{e.fullName}}</td>
          <td>{{e.departmentName}}</td>
          <td>{{e.salary | currency}}</td>
          <td class="text-center space-x-2">
            <button mat-button color="accent" (click)="edit(e.id)">Edit</button>
            <button mat-button color="warn" (click)="remove(e.id)">Delete</button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
  `
})
export class EmployeeListComponent {
  constructor(
    public service: EmployeeService,
    private router: Router
  ) {
    this.service.loadEmployees();
  }

  add() {
    this.router.navigate(['/employees/create']);
  }

  edit(id: string) {
    this.router.navigate(['/employees/edit', id]);
  }

  remove(id: string) {
    this.service.delete(id).subscribe(() => this.service.loadEmployees());
  }

  logout() {
    localStorage.removeItem('token');
    this.service.clear();
    this.router.navigate(['/login']);
  }
}