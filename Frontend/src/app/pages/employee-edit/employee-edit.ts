import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { Employee } from '../../Models/employee.model';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeService } from '../../Services/employee.service';
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

    <h2 class="text-xl font-semibold mb-4">Update Employee</h2>

    <form  (ngSubmit)="update()" class="space-y-4">

      <mat-form-field class="w-full">
        <mat-label>Full Name</mat-label>
        <input matInput [(ngModel)]="model.fullName" name="name" />
      </mat-form-field>

      <mat-form-field class="w-full">
        <mat-label>Salary</mat-label>
        <input matInput type="number" [(ngModel)]="model.salary" name="salary" />
      </mat-form-field>
<mat-form-field class="w-full">
  <mat-label>Email</mat-label>
  <input matInput [(ngModel)]="model.email" name="email" required />
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
        <button mat-raised-button color="primary">Update</button>
      </div>
    </form>
  </div>
  `
})
export class EmployeeEditComponent implements OnInit {
  
  id!: string;
  model: Employee = {} as Employee;
  departments: Department[] = [];

  constructor(
    private route: ActivatedRoute,
    private service: EmployeeService,
    private router: Router,
    private departmentService: DepartmentService
 
  ) {}

  ngOnInit() {
   this.loadDepartments();
  this.id = this.route.snapshot.params['id'];

  this.service.getById(this.id).subscribe({
    next: e => this.model = e,
    error: err => {
      console.error(err);
      this.router.navigateByUrl('/employees');
    }
  });


}

  loadDepartments() {
  this.departmentService.getAll().subscribe({
    next: res => this.departments = res,
    error: err => console.error('Failed to load departments', err)
  });
}
  update() {
    const dto = {
    fullName: this.model.fullName,
    email: this.model.email,
    salary: this.model.salary,
    departmentId: this.model.departmentId
  };
  this.service.update(this.id, dto).subscribe({
    next: () => {
      this.router.navigateByUrl('/employees'); 
    },
    error: err => console.error(err)
  });
}
  cancel() {
    this.router.navigate(['/employees']);
  }
}