import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { AddTeam } from '../interfaces/add-team.interface';
import { AddUser } from '../interfaces/add-user.interface';
import { Team } from '../interfaces/team.interface';
import { UpdateTeam } from '../interfaces/update-team.interface';
import { User } from '../interfaces/user.interface';

@Injectable({
  providedIn: 'root',
})
export class TeamService {
  constructor(private http: HttpClient) {}

  private handleError(error: HttpErrorResponse) {
    console.log(error);
    if (error.status === 0 || error.status >= 500 || !error.error) {
      return throwError(() => 'Coś poszło nie tak');
    }
    return throwError(() => error.error);
  }

  getTeams(archived: boolean) {
    return this.http
      .get<Team[]>('/api/Team', { params: { archived: archived } })
      .pipe(catchError(this.handleError));
  }

  getTeamMembers(teamId: string) {
    return this.http
      .get<User[]>(`/api/Team/${teamId}/User`)
      .pipe(catchError(this.handleError));
  }

  addTeam(addTeam: AddTeam) {
    return this.http
      .post<Team>('/api/Team', { ...addTeam })
      .pipe(catchError(this.handleError));
  }

  addUserToTeam(teamId: string, addUser: AddUser) {
    return this.http
      .post<User>(`/api/Team/${teamId}/User`, { ...addUser })
      .pipe(catchError(this.handleError));
  }

  deleteUserFromTeam(teamId: string, userId: string) {
    return this.http
      .delete(`/api/Team/${teamId}/User/${userId}`)
      .pipe(catchError(this.handleError));
  }

  updateTeamIsArchived(id: string, isArchived: boolean) {
    return this.http
      .put<Team>(`/api/Team/${id}/IsArchived`, { isArchived: isArchived })
      .pipe(catchError(this.handleError));
  }

  updateTeamName(id: string, name: string) {
    return this.http
      .put<Team>(`/api/Team/${id}/Name`, { name: name })
      .pipe(catchError(this.handleError));
  }
}
