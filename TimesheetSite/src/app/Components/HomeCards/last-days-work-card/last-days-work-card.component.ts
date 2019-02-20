import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Chart } from 'chart.js';
import { AuthService } from 'src/app/Services/auth.service';
import { LoggedUser } from 'src/app/Model/loggedUser';
import { TimesheetService } from 'src/app/Services/timesheet.service';
import { TimesheetDataProviderService } from 'src/app/DataProviders/Timesheet/timesheet-data-provider.service';
import { groupBy, switchMap, mergeMap, tap, map, merge } from 'rxjs/operators';
import { from } from 'rxjs';



@Component({
  selector: 'app-last-days-work-card',
  templateUrl: './last-days-work-card.component.html',
  styleUrls: ['./last-days-work-card.component.css']
})
export class LastDaysWorkCardComponent implements OnInit {

  constructor(
    private _authService: AuthService,
    private _timesheetService: TimesheetDataProviderService) { }

  days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];


  chart = [];

  ngOnInit(): void {

    this.loadDataForWeek();
  }


  loadDataForWeek() {
    const loggedUser: LoggedUser = this._authService.getLoggedUser();
   this._timesheetService.getWorkingHoursForPeriod(loggedUser.employeeId, 6).subscribe(res => {

    const days = res.map(x => this.days[new Date(x.date).getDay()]);
    const hours = res.map(x => x.hours);


      this.chart = new Chart('canvas', {
        type: 'line',
        data: {
          labels: days,
          datasets: [
            {
              data: hours,
              borderColor: "#3cba9f",
              fill: false
            }
          ]
        },
        options: {
          legend: {
            display: false
          },
          scales: {
            xAxes: [{
              display: true,
            }],
            yAxes: [{
              display: true,
              ticks: {
                beginAtZero: true
              }
            }],
          },
          maintainAspectRatio: false
        }
      });


    });

  }




}
