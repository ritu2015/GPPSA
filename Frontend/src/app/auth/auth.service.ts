import { Router } from "@angular/router";
import { EmployeeService } from "../Services/employee.service";
import { Injectable } from "@angular/core";

@Injectable({ providedIn: 'root' })
export class AuthService {
  constructor(
    private router: Router,
    private employeeService: EmployeeService
  ) {}

  logout(): void {
    localStorage.removeItem('token');

    // clear app state
    this.employeeService.clear();

    // optional: clear other cached data
    // localStorage.clear(); ❌ (don’t wipe everything)

    this.router.navigate(['/login']);
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }
}