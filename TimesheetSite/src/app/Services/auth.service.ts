import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { LoggedUser } from '../Model/loggedUser';
import { Role } from '../Model/role.enum';

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

  public logout() {
    sessionStorage.removeItem('token');
  }


  public getLoggedUser(): LoggedUser {
    const helper = new JwtHelperService();
    const token = localStorage.getItem('token');
    const decoded = helper.decodeToken(token);
    const roleVal = +decoded.role;
    const logged = new LoggedUser();
    logged.role = roleVal;
    logged.username = decoded.unique_name;
    logged.employeeId = +decoded.nameid;
    return logged;
  }


}
