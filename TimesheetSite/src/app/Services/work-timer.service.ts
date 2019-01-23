import { Injectable } from '@angular/core';
import { TimesheetDataProviderService } from '../DataProviders/Timesheet/timesheet-data-provider.service';

@Injectable({
  providedIn: 'root'
})

export class WorkTimerService {

  constructor(private _timeSheetDP: TimesheetDataProviderService) { }

  private startSecounds = 0;

  startWork(): void {

  }


}
