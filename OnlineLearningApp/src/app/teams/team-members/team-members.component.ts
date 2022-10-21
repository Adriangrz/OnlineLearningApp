import {
  ChangeDetectorRef,
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
} from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import {
  faCircleInfo,
  faEllipsis,
  faPlus,
  faTrash,
} from '@fortawesome/free-solid-svg-icons';
import { AuthService } from 'src/app/auth/services/auth.service';
import { User } from '../interfaces/user.interface';
import { TeamService } from '../services/team.service';

@Component({
  selector: 'app-team-members',
  templateUrl: './team-members.component.html',
  styleUrls: ['./team-members.component.scss'],
})
export class TeamMembersComponent implements OnInit, OnChanges {
  @Output() selectedUserId = new EventEmitter<string>();
  @Input()
  teamId: string = '';
  @Input()
  adminEmail: string = '';

  teamMemberForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
  });

  error: string | undefined;
  loggedInUser: string | null = null;
  isUserBeingAdded: boolean = false;
  isBeingProcessed: boolean = false;
  users: User[] = [];

  faPlus = faPlus;
  faCircleInfo = faCircleInfo;
  faTrash = faTrash;
  faEllipsis = faEllipsis;

  constructor(
    private fb: FormBuilder,
    private cd: ChangeDetectorRef,
    private teamService: TeamService,
    public authService: AuthService
  ) {}

  ngOnInit(): void {
    this.loggedInUser = this.authService.getLoggedInUser;
  }

  ngOnChanges() {
    this.clear();
    this.getTeamMembers();
  }

  clear() {
    this.users = [];
  }

  getTeamMembers() {
    if (!this.teamId) return;
    this.teamService.getTeamMembers(this.teamId).subscribe({
      next: (data) => {
        this.error = undefined;
        this.users = data;
      },
      error: (err) => {
        this.error = err;
      },
    });
  }

  get email() {
    return this.teamMemberForm.get('email')!;
  }

  get emailIsInvalid() {
    return this.email.invalid && (this.email.dirty || this.email.touched);
  }

  addTeamMember() {
    this.isBeingProcessed = true;
    this.teamService
      .addUserToTeam(this.teamId, { ...this.teamMemberForm.value })
      .subscribe({
        next: (data) => {
          this.error = undefined;
          this.users.push(data);
          this.isUserBeingAdded = false;
          this.isBeingProcessed = false;
        },
        error: (err) => {
          this.error = err;
          this.isBeingProcessed = false;
        },
      });
  }

  deleteUserFromTeam(userId: string) {
    this.teamService.deleteUserFromTeam(this.teamId, userId).subscribe({
      next: () => {
        this.error = undefined;
        this.users = this.users.filter((u) => u.id !== userId);
      },
      error: (err) => {
        this.error = err;
      },
    });
  }
}
