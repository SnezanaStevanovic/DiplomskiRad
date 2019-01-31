import { Injectable } from '@angular/core';
import { TimesheetDataProviderService } from '../DataProviders/Timesheet/timesheet-data-provider.service';
import { AuthService } from './auth.service';
import { LoggedUser } from '../Model/loggedUser';
import { TimerService } from './Timer/timer-service.service';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})

export class TimesheetService {

  working = false;

  constructor(
    private _timeSheetDP: TimesheetDataProviderService,
    private _authService: AuthService,
    private _timerService: TimerService) { }



  public startWork(): Observable<boolean> {
    const loggedUser: LoggedUser = this._authService.getLoggedUser();
    return this._timeSheetDP.startWork(loggedUser.employeeId).pipe(
      map(x => {
        if (x.success) {
          this._timerService.playTimer(0);
          this.working = true;
          return true;

        } else {
          return false;
        }
      }));
  }

  public stopWork(): Observable<boolean> {
    const loggedUser: LoggedUser = this._authService.getLoggedUser();

    return this._timeSheetDP.endWork(loggedUser.employeeId).pipe(
      map(x => {
        if (x.success) {
          this._timerService.stopTimer();
          this.working = false;
          return true;

        } else {
          return false;
        }
      }));
  }

  public checkIfWorking(): Observable<boolean> {
    if (this.working) {
      return of(true);
    } else {
      const loggedUser: LoggedUser = this._authService.getLoggedUser();

      return this._timeSheetDP.checkCurrentWorkStateOfDay(loggedUser.employeeId).pipe(
        map(res => {
          if (res.success) {
            if (res.workingSecounds > 0) {
              this._timerService.playTimer(res.workingSecounds);
              return true;
            } else {
              return false;
            }
          } else {
            return false;
          }
        })
      );
    }
  }


}
