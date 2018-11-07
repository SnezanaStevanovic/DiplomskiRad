import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() { }

  public isAuthenticated(): boolean {
    const helper = new JwtHelperService();
    const token = localStorage.getItem('token');
    return !helper.isTokenExpired(token);

  }


  public login(token: string): void {
    localStorage.setItem('token', token);
  }

}
