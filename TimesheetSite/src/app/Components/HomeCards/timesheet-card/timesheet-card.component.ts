import { Component, OnInit } from '@angular/core';
import { TimesheetService } from 'src/app/Services/timesheet.service';

@Component({
  selector: 'app-timesheet-card',
  templateUrl: './timesheet-card.component.html',
  styleUrls: ['./timesheet-card.component.css']
})
export class TimesheetCardComponent implements OnInit {

  constructor(private _timesheetService: TimesheetService) { }

  working: boolean;

  ngOnInit() {
  }

  work(): void {
    if (this.working) {
      this._timesheetService.stopWork().subscribe(res => {
        if (res) {
          this.working = false;
        }
      });
    } else {
      this._timesheetService.startWork().subscribe(res => {
        if (res) {
          this.working = true;
        }
      });
    }
  }


}
