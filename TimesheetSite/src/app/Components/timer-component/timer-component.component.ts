import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription, Observable, timer } from 'rxjs';
import { TimerService } from 'src/app/Services/Timer/timer-service.service';


@Component({
  selector: 'app-timer-component',
  templateUrl: './timer-component.component.html',
  styleUrls: ['./timer-component.component.css']
})
export class TimerComponentComponent implements OnInit, OnDestroy {
  private playPauseStopUnsubscribe: any;

  minutesDisplay = 0;
  hoursDisplay = 0;
  secondsDisplay = 0;

  sub: Subscription;

  constructor(private timerService: TimerService) {
  }

  ngOnInit() {
    this.playPauseStopUnsubscribe = this.timerService.playPauseStop$.subscribe((res: any) => this.playPauseStop(res));
  }

  ngOnDestroy() {
    this.playPauseStopUnsubscribe.unsubscribe();
  }

  private playPauseStop(ticks: number) {
    this.secondsDisplay = this.getSeconds(ticks);
    this.minutesDisplay = this.getMinutes(ticks);
    this.hoursDisplay = this.getHours(ticks);
  }

  private getSeconds(ticks: number) {
    return this.pad(ticks % 60);
  }

  private getMinutes(ticks: number) {
    return this.pad((Math.floor(ticks / 60)) % 60);
  }

  private getHours(ticks: number) {
    return this.pad(Math.floor((ticks / 60) / 60));
  }

  private pad(digit: any) {
    return digit <= 9 ? '0' + digit : digit;
  }
}
