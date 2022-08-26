import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { faHome } from '@fortawesome/free-solid-svg-icons';
import { AuthService } from '../auth/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent {
  faHome = faHome;
  constructor(public authService: AuthService, private http: HttpClient) {}

  onLogOut() {
    this.authService.logout();
  }
}
