import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, map, Observable, of, tap } from 'rxjs';
import { environment } from '../../../environments/environment';
import { TokenResponse, LoginRequest, RegisterRequest, ResetPasswordRequest } from '../models/identity.model';

const TOKEN_NAME = 'token';
const API_ROOT = `${environment.apiUrl}/identity`;
const ACCOUNTS_URL = `${environment.apiUrl}/api/v1/accounts`;

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private _tokenSubject: BehaviorSubject<TokenResponse>;
  public token: Observable<TokenResponse>;
  constructor(
    private httpClient: HttpClient
  ) {
    const token = localStorage.getItem(TOKEN_NAME);
    this._tokenSubject = new BehaviorSubject<any>(token ? JSON.parse(token) : null);
    this.token = this._tokenSubject.asObservable();
  }

  getToken() {
    return this._tokenSubject.value;
  }

  getAccessToken() {
    var token = this.getToken();
    return token ? `${token.tokenType} ${token.accessToken}` : '';
  }

  setToken(token: TokenResponse) {
    this._tokenSubject.next(token);
    localStorage.setItem(TOKEN_NAME, JSON.stringify(token));
  }

  refreshToken() {
    return this.httpClient.post<TokenResponse>(`${API_ROOT}/refreshToken`, {})
      .pipe(tap(token => {
        this.setToken(token);
      }));
  }

  login(payload: LoginRequest, useCookies?: boolean, useSessionCookies?: boolean) {
    let queryParams = {};
    if (useCookies) {
      queryParams = { useCookies: true };
    } else if (useSessionCookies) {
      queryParams = { useSessionCookies: true };
    }
    return this.httpClient.post<TokenResponse>(`${API_ROOT}/login`, payload, {
      params: { ...queryParams }
    }).pipe(tap(token => {
      this.setToken(token);
    }));
  }

  logout() {
    localStorage.clear();
    location.href = '/';
  }

  register(payload: RegisterRequest) {
    return this.httpClient.post<void>(`${API_ROOT}/register`, payload);
  }

  resendConfirmationEmail(payload: { email: string }) {
    return this.httpClient.post<void>(`${API_ROOT}/resendConfirmationEmail`, payload);
  }

  forgotPassword(payload: { email: string }) {
    return this.httpClient.post<void>(`${API_ROOT}/forgotPassword`, payload);
  }

  resetPassword(payload: ResetPasswordRequest) {
    return this.httpClient.post<void>(`${API_ROOT}/resetPassword`, payload);
  }

  isSignedIn() {
    if (!this._tokenSubject.value) return of(false);
    return this.httpClient.get<void>(`${ACCOUNTS_URL}/checkSession`, {
      observe: 'response'
    }).pipe(map(() => true), catchError(() => of(false)));
  }
}
