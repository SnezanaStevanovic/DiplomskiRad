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

  start = 0;
  ticks = 0;

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

  private playPauseStop(res: any) {
    if (res.play) {
      this.startTimer();
    } else if (res.pause) {
      this.pauseTimer();
    } else if (res.stop) {
      this.stopTimer();
    }
  }

  private startTimer() {

    const timerr = timer(1, 1000);
    this.sub = timerr.subscribe(
      t => {
        this.ticks = this.start + t;
        this.secondsDisplay = this.getSeconds(this.ticks);
        this.minutesDisplay = this.getMinutes(this.ticks);
        this.hoursDisplay = this.getHours(this.ticks);
      }
    );
  }

  private pauseTimer() {
    this.start = ++this.ticks;
    if (this.sub) {
      this.sub.unsubscribe();
    }
  }

  private stopTimer() {
    this.start = 0;
    this.ticks = 0;

    this.minutesDisplay = 0;
    this.hoursDisplay = 0;
    this.secondsDisplay = 0;
    if (this.sub) {
      this.sub.unsubscribe();
    }
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
