import { Injectable } from '@angular/core';
import { TimesheetDataProviderService } from '../DataProviders/Timesheet/timesheet-data-provider.service';
import { AuthService } from './auth.service';
import { LoggedUser } from '../Model/loggedUser';

@Injectable({
  providedIn: 'root'
})

export class TimesheetService {

  constructor(
    private _timeSheetDP: TimesheetDataProviderService,
    private _authService: AuthService) { }



  startWork(): void {

    const loggedUser: LoggedUser = this._authService.getLoggedUser();

    this._timeSheetDP.startWork(loggedUser.employeeId).subscribe(x => {
      const a = x;
    });

  }


}
