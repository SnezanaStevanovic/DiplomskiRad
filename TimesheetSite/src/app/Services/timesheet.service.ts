import { Injectable } from '@angular/core';
import { TimesheetDataProviderService } from '../DataProviders/Timesheet/timesheet-data-provider.service';
import { AuthService } from './auth.service';
import { LoggedUser } from '../Model/loggedUser';
import { TimerService } from './Timer/timer-service.service';
import { Observable } from 'rxjs';
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

  public checkIfWorking(): boolean {
    return this.working;
  }


}
