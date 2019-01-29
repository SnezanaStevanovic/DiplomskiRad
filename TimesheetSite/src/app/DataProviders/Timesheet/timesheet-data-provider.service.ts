import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { BaseService } from '../base.service';
import { catchError } from 'rxjs/operators';
import { throwError, Observable } from 'rxjs';
import { BaseResponse } from 'src/app/Model/baseResponse';

@Injectable({
  providedIn: 'root'
})
export class TimesheetDataProviderService extends BaseService {

  constructor(protected _client: HttpClient) {
    super(_client);

  }

  startWork(employeeId: number): Observable<BaseResponse> {
    const url = this._baseUrl + 'Timesheet/setStartTime';
    const postBudy = JSON.stringify(employeeId);
    return this._http.post<BaseResponse>(url, postBudy, this.httpOptions)
      .pipe(
        catchError(err => throwError(err))
      );
  }

  endWork(employeeId: number): Observable<BaseResponse> {
    const url = this._baseUrl + 'Timesheet/setEndTime';
    const postBudy = JSON.stringify(employeeId);
    return this._http.post<BaseResponse>(url, postBudy, this.httpOptions)
      .pipe(
        catchError(err => throwError(err))
      );
  }

}
