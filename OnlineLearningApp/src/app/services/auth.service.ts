import { Inject, Injectable, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { catchError, map, Observable, throwError } from 'rxjs';
import { TokenResponse } from '../interfaces/token.response';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  authKey: string = 'auth';
  constructor(
    private http: HttpClient,
    @Inject(PLATFORM_ID) private platformId: any
  ) {}

  login(email: string, password: string): Observable<boolean> {
    let url: string = 'api/Authentication/Login';
    let data = {
      email: email,
      password: password,
    };
    return this.http.post<TokenResponse>(url, data).pipe(
      map((res) => {
        let token = res && res.token;
        if (token) {
          this.setAuth(res);
          return true;
        }
        return false;
      })
    );
  }

  setAuth(auth: TokenResponse | null): boolean {
    if (isPlatformBrowser(this.platformId)) {
      if (auth) localStorage.setItem(this.authKey, JSON.stringify(auth));
      else localStorage.removeItem(this.authKey);
    }
    return true;
  }

  logout(): boolean {
    this.setAuth(null);
    return true;
  }

  getAuth(): TokenResponse | null {
    if (isPlatformBrowser(this.platformId)) {
      var i = localStorage.getItem(this.authKey);
      if (i) {
        return JSON.parse(i);
      }
    }
    return null;
  }

  isLoggedIn(): boolean {
    if (isPlatformBrowser(this.platformId)) {
      return localStorage.getItem(this.authKey) != null;
    }
    return false;
  }
}
