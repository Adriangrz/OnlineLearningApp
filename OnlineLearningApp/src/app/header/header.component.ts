import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { faHome, faPeopleGroup } from '@fortawesome/free-solid-svg-icons';
import { AuthService } from '../auth/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent {
  faHome = faHome;
  faPeopleGroup = faPeopleGroup;
  constructor(
    public authService: AuthService,
    private http: HttpClient,
    private router: Router
  ) {}

  onLogOut() {
    this.authService.logout();
    this.router.navigate(['/logowanie']);
  }
}
