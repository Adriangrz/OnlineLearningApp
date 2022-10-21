import {
  ChangeDetectorRef,
  Component,
  ElementRef,
  Input,
  OnChanges,
  ViewChild,
} from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import {
  faCircleInfo,
  faEllipsis,
  faPen,
  faTrash,
  faPlus,
} from '@fortawesome/free-solid-svg-icons';
import { AuthService } from 'src/app/auth/services/auth.service';
import { Team } from '../interfaces/team.interface';
import { User } from '../interfaces/user.interface';
import { TeamService } from '../services/team.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-manage-team',
  templateUrl: './manage-team.component.html',
  styleUrls: ['./manage-team.component.scss'],
})
export class ManageTeamComponent implements OnChanges {
  @Input()
  team: Team | undefined;

  admin: User | undefined;
  error: string | undefined;
  selectedUser: string = '';

  faCircleInfo = faCircleInfo;
  faEllipsis = faEllipsis;

  constructor(
    public authService: AuthService,
    public userService: UserService,
    private teamService: TeamService
  ) {}

  ngOnChanges() {
    if (!this.team) return;
    this.clear();
    this.getUserById();
  }

  clear() {
    this.admin = undefined;
  }

  getUserById() {
    if (!this.team) return;
    this.userService.getUserById(this.team.adminId).subscribe({
      next: (data) => {
        this.error = undefined;
        this.admin = data;
      },
      error: (err) => {
        this.error = err;
      },
    });
  }

  newTeamName(name: string) {
    if (!this.team) return;
    this.team.name = name;
  }

  selectedUserId(id: string) {
    this.selectedUser = id;
  }
}
