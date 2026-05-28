import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { Employee } from '../../Models/employee.model';
import { EmployeeService } from '../../Services/employee.service';
import { Router } from '@angular/router';
import { Department, DepartmentService } from '../../Services/department.service';

@Component({
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatInputModule,
    MatButtonModule,
    MatFormFieldModule
  ],
  template: `
  <div class="max-w-xl mx-auto mt-10 p-6 bg-white shadow rounded">

    <h2 class="text-xl font-semibold mb-4">Create Employee</h2>

    <form (ngSubmit)="save()" class="space-y-4">
      <mat-form-field class="w-full">
        <mat-label>Employee Code</mat-label>
        <input matInput [(ngModel)]="model.employeeCode" name="code" required />
      </mat-form-field>

      <mat-form-field class="w-full">
        <mat-label>Full Name</mat-label>
        <input matInput [(ngModel)]="model.fullName" name="name" required />
      </mat-form-field>

      <mat-form-field class="w-full">
        <mat-label>Email</mat-label>
        <input matInput [(ngModel)]="model.email" name="email" required />
      </mat-form-field>

      <mat-form-field class="w-full">
        <mat-label>Salary</mat-label>
        <input matInput type="number" [(ngModel)]="model.salary" name="salary" required />
      </mat-form-field>
      <mat-form-field class="w-full">
  <mat-label>Date Of Joining</mat-label>
  <input matInput type="date"
         [(ngModel)]="model.dateOfJoining"
         name="dateOfJoining"
         required />
</mat-form-field>

<mat-form-field class="w-full">
  <mat-label>Department</mat-label>
  <select
    matNativeControl
    [(ngModel)]="model.departmentId"
    name="departmentId"
    required>

    <option *ngFor="let d of departments" [value]="d.id">
      {{ d.departmentName }}
    </option>
  </select>
</mat-form-field>

      <div class="flex justify-end gap-3">
        <button mat-stroked-button type="button" (click)="cancel()">Cancel</button>
        <button mat-raised-button color="primary" type="button" (click)="save()">
  Save
</button>
      </div>
    </form>
  </div>
  `
})
export class EmployeeCreateComponent {
  model: Employee = {} as Employee;
  departments: Department[] = [];

  constructor(
    private service: EmployeeService,
    private router: Router,
    private departmentService: DepartmentService
  ) {}

  ngOnInit() {
    this.loadDepartments();
  }

  loadDepartments() {
  this.departmentService.getAll().subscribe({
    next: res => this.departments = res,
    error: err => console.error('Failed to load departments', err)
  });
}
  save() {
  this.service.create(this.model).subscribe({
    next: () => {
      this.router.navigateByUrl('/employees'); // 👈 HERE
    },
    error: err => console.error(err)
  });
}

  cancel() {
    this.router.navigate(['/employees']);
  }
}
