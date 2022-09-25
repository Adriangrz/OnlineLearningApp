import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import {
  faBoxArchive,
  faCircleInfo,
  faEllipsis,
  faGear,
  faInfo,
  faPen,
  faPlus,
  faTrash,
  faTrashArrowUp,
} from '@fortawesome/free-solid-svg-icons';
import { AuthService } from 'src/app/auth/services/auth.service';
import { Team } from '../interfaces/team.interface';
import { TeamService } from '../services/team.service';

declare var bootstrap: any;

@Component({
  selector: 'app-teams-dashboard',
  templateUrl: './teams-dashboard.component.html',
  styleUrls: ['./teams-dashboard.component.scss'],
})
export class TeamsDashboardComponent implements OnInit {
  faEllipsis = faEllipsis;
  faGear = faGear;
  faBoxArchive = faBoxArchive;
  faPlus = faPlus;
  faTrashArrowUp = faTrashArrowUp;
  faCircleInfo = faCircleInfo;

  loggedInUser: string | null = null;
  teams: Team[] = [];
  archivedTeams: Team[] = [];
  error: string | undefined;
  selectedTeam: Team | undefined;

  constructor(
    private teamService: TeamService,
    public authService: AuthService
  ) {}

  ngOnInit(): void {
    this.loggedInUser = this.authService.getLoggedInUser;
    this.getTeams(false);
  }

  getTeams(isArchived: boolean) {
    this.teamService.getTeams(isArchived).subscribe({
      next: (data) => {
        if (isArchived) {
          this.archivedTeams = data;
          return;
        }
        this.teams = data;
      },
      error: (err) => {
        this.error = err;
      },
    });
  }

  addedTeam(team: Team) {
    this.teams.push(team);
  }

  archive(id: string) {
    this.teamService.updateTeamIsArchived(id, true).subscribe({
      next: (team: Team) => {
        this.teams = this.teams.filter((t) => t.id !== team.id);
        this.archivedTeams.push(team);
      },
      error: (err) => {
        this.error = err;
      },
    });
  }

  restore(id: string) {
    this.teamService.updateTeamIsArchived(id, false).subscribe({
      next: (team: Team) => {
        this.archivedTeams = this.archivedTeams.filter((t) => t.id !== team.id);
        this.teams.push(team);
      },
      error: (err) => {
        this.error = err;
      },
    });
  }
}
