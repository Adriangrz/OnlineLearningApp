import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { UserDetails } from '../interfaces/user-details.interface';
import { User } from '../interfaces/user.interface';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient) {}

  private handleError(error: HttpErrorResponse) {
    if (error.status === 0 || error.status >= 500 || !error.error) {
      return throwError(() => 'Coś poszło nie tak');
    }
    return throwError(() => error.error);
  }

  getUserById(id: string) {
    return this.http
      .get<UserDetails>(`/api/User/${id}`)
      .pipe(catchError(this.handleError));
  }
}
