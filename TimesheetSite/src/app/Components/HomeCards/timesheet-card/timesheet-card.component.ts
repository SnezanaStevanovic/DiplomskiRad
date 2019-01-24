import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-timesheet-card',
  templateUrl: './timesheet-card.component.html',
  styleUrls: ['./timesheet-card.component.css']
})
export class TimesheetCardComponent implements OnInit {

  constructor() { }

  working: boolean;

  ngOnInit() {
  }

  work(): void {
    if (this.working) {
      this.working = false;
    } else {
      this.working = true;
    }
  }


}
