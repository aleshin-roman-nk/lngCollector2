import { Injectable } from '@angular/core';
import { Observable, interval } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TimertickerService {

  private interval$: Observable<number> = interval(1000);

  constructor() { }

  getTimerObservable(): Observable<number> {
    return this.interval$;
  }

}
