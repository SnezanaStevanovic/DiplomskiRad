import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { BaseService } from '../base.service';
import { catchError, tap, map } from 'rxjs/operators';
import { throwError, Observable } from 'rxjs';
import { BaseResponse } from 'src/app/Model/baseResponse';
import { TimesheetStateOfDayResponse } from 'src/app/Model/timesheetStateOfDayResponse';
import { Timesheet } from 'src/app/Model/timesheet';
import { PeriodTimeheetGetResponse } from 'src/app/Model/periodTimeheetGetResponse';
import { GetWorkingHoursForPeriodResponse } from 'src/app/Model/getWorkingHoursForPeriodResponse';
import { HoursPerDay } from 'src/app/Model/hoursPerDay';

@Injectable({
  providedIn: 'root'
})
export class TimesheetDataProviderService extends BaseService {

  constructor(protected _client: HttpClient) {
    super(_client);

  }

  public startWork(employeeId: number): Observable<BaseResponse> {
    const url = this._baseUrl + 'Timesheet/setStartTime';
    const postBudy = JSON.stringify(employeeId);
    return this._http.post<BaseResponse>(url, postBudy, this.httpOptions)
      .pipe(
        catchError(err => throwError(err))
      );
  }

  public endWork(employeeId: number): Observable<BaseResponse> {
    const url = this._baseUrl + 'Timesheet/setEndTime';
    const postBudy = JSON.stringify(employeeId);
    return this._http.post<BaseResponse>(url, postBudy, this.httpOptions)
      .pipe(
        catchError(err => throwError(err))
      );
  }

  public checkCurrentWorkStateOfDay(employeeId: number): Observable<TimesheetStateOfDayResponse> {
    const url = this._baseUrl + `Timesheet/TimesheetStateForDay?employeeId=${employeeId}`;
    return this._http.get<TimesheetStateOfDayResponse>(url, this.httpOptions)
      .pipe(
        catchError(err => throwError(err))
      );
  }

  public periodTimesheetGet(employeeId: number, startDate: string, endDate: string): Observable<Array<Timesheet>> {
    // tslint:disable-next-line:max-line-length
    const url = this._baseUrl + `Timesheet/periodTimesheetGet?employeeId=${employeeId}&startDate=${startDate}&endDate=${endDate}`;
    return this._http.get<PeriodTimeheetGetResponse>(url, this.httpOptions)
      .pipe(
        map(x => {
          if (x.success) {
            return x.allTimesheetsForPeriod;
          } else {
            return new Array<Timesheet>();
          }
        }),
        catchError(err => throwError(err))
      );
  }


  public getWorkingHoursForPeriod(employeeId: number, lastNDays: number): Observable<Array<HoursPerDay>> {
    const url = this._baseUrl + `Timesheet/GetWorkingHoursForLastDays?employeeId=${employeeId}&lastNDays=${lastNDays}`;
    return this._http.get<GetWorkingHoursForPeriodResponse>(url, this.httpOptions)
      .pipe(
        map(x => {
          if (x.success) {
            return x.hoursPerDay;
          } else {
            return new Array<HoursPerDay>();
          }
        }),
        catchError(err => throwError(err))
      );
  }


}
