import { Routes } from '@angular/router';
import { LoginComponent } from './auth/login';
import { EmployeeListComponent } from '../app/pages/employees/employees';
import { EmployeeCreateComponent } from '../app/pages/employee-create/employee-create';
import { EmployeeEditComponent } from '../app/pages/employee-edit/employee-edit';
import { authGuard } from './auth/auth.guard';

export const routes: Routes = [

  {path: 'login', component: LoginComponent},
  {path: 'employees', component: EmployeeListComponent, canActivate: [authGuard]},
  {path: 'employees/create', component: EmployeeCreateComponent, canActivate: [authGuard]},
  {path: 'employees/edit/:id', component: EmployeeEditComponent, canActivate: [authGuard]},
  { path: '**', redirectTo: 'login' }

];