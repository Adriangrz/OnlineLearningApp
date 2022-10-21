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
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { ResponseToken } from 'src/app/auth/interfaces/response-token.interface';
import { switchMap, pipe, lastValueFrom, of } from 'rxjs';
import { TokenInterceptor } from 'src/app/auth/token.interceptor';

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
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.loggedInUser = this.authService.getLoggedInUser;
    this.getTeams(false);
  }

  connectToSignalR() {
    let connection = new HubConnectionBuilder()
      .withUrl('https://localhost:7067/hubs/teamHub', {
        accessTokenFactory: async () => {
          let expirationTime = new Date(
            this.authService.getTokenExpirationTime()!
          );
          expirationTime.setMinutes(expirationTime.getMinutes() - 5);
          if (new Date().getTime() >= expirationTime.getTime()) {
            let token = await lastValueFrom(this.authService.refreshToken());
            return token.token;
          }
          return this.authService.getJwtToken()!;
        },
      })
      .withAutomaticReconnect()
      .configureLogging(LogLevel.Information)
      .build();

    connection.on('addToTeam', (data: Team) => {
      this.teams.push(data);
    });

    connection.on('deleteFromTeam', (id: string) => {
      this.teams = this.teams.filter((t) => t.id !== id);
    });

    connection.on('changeTeamName', (team: Team) => {
      let i = this.teams.findIndex((t) => t.id === team.id);
      this.teams[i].name = team.name;
    });

    connection.on('changeTeamIsArchived', (team: Team) => {
      if (!team.isArchived) {
        this.archivedTeams = this.archivedTeams.filter((t) => t.id !== team.id);
        this.teams.push(team);
        return;
      }

      if (team.isArchived) {
        this.teams = this.teams.filter((t) => t.id !== team.id);
        this.archivedTeams.push(team);
        return;
      }
    });

    connection.start();
  }

  getTeams(isArchived: boolean) {
    this.teamService.getTeams(isArchived).subscribe({
      next: (data) => {
        this.error = undefined;
        this.connectToSignalR();
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
        this.error = undefined;
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
        this.error = undefined;
        this.archivedTeams = this.archivedTeams.filter((t) => t.id !== team.id);
        this.teams.push(team);
      },
      error: (err) => {
        this.error = err;
      },
    });
  }
}
