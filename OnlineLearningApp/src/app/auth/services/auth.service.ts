import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { ResponseToken } from '../interfaces/response-token.interface';
import { catchError, map, of, tap } from 'rxjs';
import { LoginData } from '../interfaces/login-data.interface';
import { RegistrationData } from '../interfaces/registration-data.interface';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly JWT_TOKEN = 'JWT_TOKEN';
  private readonly REFRESH_TOKEN = 'REFRESH_TOKEN';
  private loggedInUser: string | null = null;
  private clientId: string = 'OnlineLearningApp';
  constructor(private http: HttpClient) {}

  register(registrationData: RegistrationData) {
    return this.http
      .post('/api/Authentication/Register', {
        ...registrationData,
      })
      .pipe(
        map(() => {
          return {
            isSuccess: true,
            responseStatus: 200,
          };
        }),
        catchError((errorData: HttpErrorResponse) => {
          return of({
            isSuccess: false,
            responseStatus: errorData.status,
            responseErrorMessage: errorData.error,
          });
        })
      );
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
        map(() => {
          return {
            isSuccess: true,
            responseStatus: 200,
          };
        }),
        catchError((errorData: HttpErrorResponse) => {
          return of({
            isSuccess: false,
            responseStatus: errorData.status,
            responseErrorMessage: errorData.error,
          });
        })
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

  private doLoginUser(email: string, responseToken: ResponseToken) {
    this.loggedInUser = email;
    this.storeTokens(responseToken);
  }

  private storeTokens(responseToken: ResponseToken) {
    localStorage.setItem(this.JWT_TOKEN, responseToken.token);
    localStorage.setItem(this.REFRESH_TOKEN, responseToken.refreshToken);
  }

  logout() {
    this.doLogoutUser();
  }

  private doLogoutUser() {
    this.loggedInUser = null;
    this.removeToken();
  }

  private removeToken() {
    localStorage.removeItem(this.JWT_TOKEN);
    localStorage.removeItem(this.REFRESH_TOKEN);
  }
}
