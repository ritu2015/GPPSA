import { Component, ChangeDetectorRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  imports: [FormsModule, CommonModule],
  selector: 'app-login',
  template: `
  <div class="login-container">
    <h2>Login</h2>

    <form (ngSubmit)="login()">
      <div class="form-group">
        <input 
          type="email" 
          [(ngModel)]="email" 
          name="email" 
          placeholder="Email" 
          required 
          [disabled]="loading"
        />
      </div>

      <div class="form-group">
        <input 
          type="password" 
          [(ngModel)]="password" 
          name="password" 
          placeholder="Password" 
          required 
          [disabled]="loading"
        />
      </div>

      <button type="submit" [disabled]="loading">
        {{ loading ? 'Logging in...' : 'Login' }}
      </button>

      <div *ngIf="error" class="error-message">
        {{ error }}
      </div>
    </form>
  </div>
  `,
  styles: [`
    .login-container {
      width: 350px;
      margin: 120px auto;
      padding: 24px;
      border-radius: 12px;
      box-shadow: 0 10px 30px rgba(0,0,0,.1);
      text-align: center;
      background: white;
    }

    h2 {
      margin-bottom: 24px;
      color: #333;
      font-size: 24px;
    }

    .form-group {
      margin-bottom: 16px;
    }

    input {
      width: 100%;
      padding: 12px;
      border: 1px solid #ddd;
      border-radius: 6px;
      font-size: 14px;
      box-sizing: border-box;
      font-family: inherit;
      transition: border-color 0.3s;
    }

    input:focus {
      outline: none;
      border-color: #667eea;
      box-shadow: 0 0 0 3px rgba(102, 126, 234, 0.1);
    }

    input:disabled {
      background-color: #f5f5f5;
      cursor: not-allowed;
      opacity: 0.6;
    }

    button {
      width: 100%;
      padding: 12px;
      background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
      color: white;
      border: none;
      border-radius: 6px;
      font-size: 16px;
      font-weight: 600;
      cursor: pointer;
      transition: transform 0.2s, box-shadow 0.2s;
      margin-top: 8px;
    }

    button:hover:not(:disabled) {
      transform: translateY(-2px);
      box-shadow: 0 8px 20px rgba(102, 126, 234, 0.3);
    }

    button:active:not(:disabled) {
      transform: translateY(0);
    }

    button:disabled {
      opacity: 0.7;
      cursor: not-allowed;
    }

    .error-message {
      color: #d32f2f;
      font-size: 14px;
      margin-top: 12px;
      padding: 12px;
      background-color: #ffebee;
      border-radius: 6px;
      text-align: left;
    }
  `]
})
export class LoginComponent {
  email = '';
  password = '';
  loading = false;
  error = '';

  constructor(
    private http: HttpClient, 
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  login() {
    if (!this.email || !this.password) {
      this.error = 'Please enter both email and password';
      return;
    }

    this.loading = true;
    this.error = '';

    this.http.post<any>('http://localhost:5047/api/auth/login', {
      email: this.email,
      password: this.password
    }).subscribe({
      next: (res) => {
        this.loading = false;
        localStorage.setItem('token', res.accessToken);
        this.cdr.detectChanges();
        this.router.navigate(['/employees']);
      },
      error: (err) => {
        this.loading = false;
        // Handle different error types
        if (err.error instanceof ErrorEvent) {
          // Client-side error
          this.error = err.error.message;
        } else {
          // Server-side error or network error
          this.error = err.error?.message || err.message || 'Login failed. Please check your credentials or try again later.';
        }
        this.cdr.detectChanges();
        console.error('Login error:', err);
      }
    });
  }
}