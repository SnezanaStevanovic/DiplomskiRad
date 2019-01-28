import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { HttpClient } from '@angular/common/http';
import { LoginRequest } from 'src/app/Model/LoginRequest';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { LoginResponse } from 'src/app/Model/loginResponse';

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService {

  constructor(protected _client: HttpClient) {
    super(_client);

  }

  login(reqest: LoginRequest): Observable<LoginResponse> {
    const url = this._baseUrl + 'user/auth';
    const postBudy = JSON.stringify(reqest);
    return this._http.post<LoginResponse>(url, postBudy, this.httpOptions)
      .pipe(
        catchError(err => throwError(err))
      );
  }


}
