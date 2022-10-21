import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { ResponseToken } from '../interfaces/response-token.interface';
import { catchError, map, of, tap, throwError } from 'rxjs';
import { LoginData } from '../interfaces/login-data.interface';
import { RegistrationData } from '../interfaces/registration-data.interface';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly JWT_TOKEN = 'JWT_TOKEN';
  private readonly JWT_EXPIRATION_TIME = 'JWT_EXPIRATION_TIME';
  private readonly REFRESH_TOKEN = 'REFRESH_TOKEN';
  private readonly Logged_In_User = 'Logged_In_User';
  private clientId: string = 'OnlineLearningApp';
  constructor(private http: HttpClient) {}

  private handleError(error: HttpErrorResponse) {
    if (error.status === 0 || error.status >= 500 || !error.error) {
      return throwError(() => 'Coś poszło nie tak');
    }
    return throwError(() => error.error);
  }

  get getLoggedInUser() {
    return localStorage.getItem(this.Logged_In_User);
  }

  register(registrationData: RegistrationData) {
    return this.http
      .post<RegistrationData>('/api/Authentication/Register', {
        ...registrationData,
      })
      .pipe(catchError(this.handleError));
  }

  login(loginData: LoginData) {
    return this.http
      .post<ResponseToken>('/api/Authentication/Login', {
        ...loginData,
        clientId: this.clientId,
      })
      .pipe(
        tap((responseToken) =>
          this.doLoginUser(loginData.email, responseToken)
        ),
        catchError(this.handleError)
      );
  }

  isLoggedIn() {
    return !!this.getJwtToken();
  }

  getJwtToken() {
    return localStorage.getItem(this.JWT_TOKEN);
  }

  refreshToken() {
    return this.http
      .post<ResponseToken>('/api/Authentication/RefreshToken', {
        clientId: this.clientId,
        refreshToken: this.getRefreshToken(),
      })
      .pipe(
        tap((responseToken) => {
          this.storeTokens(responseToken);
        })
      );
  }

  getRefreshToken() {
    return localStorage.getItem(this.REFRESH_TOKEN);
  }

  getTokenExpirationTime() {
    return localStorage.getItem(this.JWT_EXPIRATION_TIME);
  }

  private doLoginUser(email: string, responseToken: ResponseToken) {
    localStorage.setItem(this.Logged_In_User, email);
    this.storeTokens(responseToken);
  }

  private storeTokens(responseToken: ResponseToken) {
    localStorage.setItem(
      this.JWT_EXPIRATION_TIME,
      responseToken.expiration.toString()
    );
    localStorage.setItem(this.JWT_TOKEN, responseToken.token);
    localStorage.setItem(this.REFRESH_TOKEN, responseToken.refreshToken);
  }

  logout() {
    this.doLogoutUser();
  }

  private doLogoutUser() {
    localStorage.removeItem(this.Logged_In_User);
    this.removeToken();
  }

  private removeToken() {
    localStorage.removeItem(this.JWT_TOKEN);
    localStorage.removeItem(this.REFRESH_TOKEN);
  }
}
