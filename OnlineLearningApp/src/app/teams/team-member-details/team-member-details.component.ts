import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { UserDetails } from '../interfaces/user-details.interface';
import { User } from '../interfaces/user.interface';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-team-member-details',
  templateUrl: './team-member-details.component.html',
  styleUrls: ['./team-member-details.component.scss'],
})
export class TeamMemberDetailsComponent implements OnChanges {
  @Input()
  userId: string = '';

  error: string | undefined;
  user: UserDetails | undefined;

  constructor(public userService: UserService) {}

  ngOnChanges() {
    this.error = undefined;
    if (!this.userId) return;
    this.getUserById();
  }
  getUserById() {
    this.userService.getUserById(this.userId).subscribe({
      next: (data) => {
        this.error = undefined;
        this.user = data;
      },
      error: (err) => {
        this.error = err;
      },
    });
  }
}
