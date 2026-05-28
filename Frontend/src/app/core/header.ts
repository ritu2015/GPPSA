import { Component } from "@angular/core";
import { AuthService } from "../auth/auth.service";

@Component({
  selector: 'app-header',
  template: `
    <button
      class="text-red-600 font-semibold"
      (click)="logout()">
      Logout
    </button>
  `
})
export class HeaderComponent {
  constructor(private authService: AuthService) {}

  logout() {
    this.authService.logout();
  }
}