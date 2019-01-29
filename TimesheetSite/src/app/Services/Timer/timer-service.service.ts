import { Injectable, EventEmitter } from '@angular/core';
import { Subscription, Observable, timer } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TimerService {


  start = 0;
  ticks = 0;

  timerSub: Subscription;


  public playPauseStop$ = new EventEmitter();

  public playTimer(fromSecounds: number) {
    this.start = fromSecounds;
    const timerr = timer(1, 1000);

    this.timerSub = timerr.subscribe(x => {
      this.ticks = this.start + x;
      this.playPauseStop$.emit(this.ticks);
    });
  }

  public stopTimer() {
    this.start = 0;
    this.ticks = 0;
    this.timerSub.unsubscribe();
  }



}
