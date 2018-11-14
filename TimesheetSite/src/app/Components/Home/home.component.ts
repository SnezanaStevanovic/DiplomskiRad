import { Component } from '@angular/core';
import { map } from 'rxjs/operators';
import { HomeCard } from 'src/app/Model/card';
import { Breakpoints, BreakpointState, BreakpointObserver } from '@angular/cdk/layout';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  /** Based on the screen size, switch from standard to one column per row */
  cards = this.breakpointObserver.observe(Breakpoints.Handset).pipe(
    map(({ matches }) => {
      if (matches) {
        return [
          { title: 'Start your day', cols: 1, rows: 1, name: 'app-timesheet-card' },
          { title: 'Your tasks', cols: 1, rows: 1, name: 'app-your-tasks' },
          {
            title: 'Your work time', cols: 1, rows: 1, name: 'app-last-days-work-card'
          }
        ];
      }

      return [
        { title: 'Start your day', cols: 1, rows: 1, name: 'app-timesheet-card' },
        { title: 'Your tasks', cols: 1, rows: 1, name: 'app-your-tasks' },
        { title: 'Your work time', cols: 2, rows: 1, name: 'app-last-days-work-card' },
      ];
    })
  );

  constructor(private breakpointObserver: BreakpointObserver) { }
}
