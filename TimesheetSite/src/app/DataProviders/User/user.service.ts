import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { LoginResponse } from 'src/app/Model/loginResponse';
import { Employee } from 'src/app/Model/employee';
import { AllEmployeesResponse } from 'src/app/Model/allEmployeeRes';
import { map } from 'rxjs/operators';
import { User } from 'src/app/Model/user';
import { BaseResponse } from 'src/app/Model/baseResponse';
import { RegistrationRequest } from 'src/app/Model/registrationRequest';

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService {

  constructor(protected _client: HttpClient) {
    super(_client);

  }

  login(reqest: User): Observable<LoginResponse> {
    const url = this._baseUrl + 'user/auth';
    const postBudy = JSON.stringify(reqest);
    return this._http.post<LoginResponse>(url, postBudy, this.httpOptions)
      .pipe(
        catchError(err => throwError(err))
      );
  }

  public register(regRequest: RegistrationRequest): Observable<BaseResponse> {
    const url = this._baseUrl + 'user/register';
    const postBudy = JSON.stringify(regRequest);
    return this._http.post<BaseResponse>(url, postBudy, this.httpOptions)
      .pipe(
        catchError(err => throwError(err))
      );
  }

  public getAll(): Observable<Array<Employee>> {
    const url = this._baseUrl + 'Employee/GetAll';
    return this._http.get<AllEmployeesResponse>(url, this.httpOptions)
      .pipe(
        map(res => {
          return res.employees;
        }),
        catchError(err => throwError(err))
      );
  }

public getAllForProject(projectId: number): Observable<Array<Employee>> {
  const url = this._baseUrl + `Employee/GetAllEmployeesPerProject/${projectId}`;
    return this._http.get<AllEmployeesResponse>(url, this.httpOptions)
      .pipe(
        map(res => {
          return res.employees;
        }),
        catchError(err => throwError(err))
      );
}


}
