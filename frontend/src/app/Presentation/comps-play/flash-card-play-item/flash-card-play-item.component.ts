import { Component, EventEmitter, Input, Output, SimpleChange, SimpleChanges } from '@angular/core';
import { Subscription, interval } from 'rxjs';
import { IFlashCard } from 'src/app/Core/Models/flash-card';
import { TimertickerService } from '../../services/timerticker.service';
import { FillPipe } from '../../pipes/fill.pipe';

@Component({
  selector: 'app-flash-card-play-item',
  templateUrl: './flash-card-play-item.component.html',
  styleUrls: ['./flash-card-play-item.component.css']
})
export class FlashCardPlayItemComponent {

  @Input() card: IFlashCard
  isDisabled: boolean

  @Output()
  goPlayThisCard: EventEmitter<IFlashCard> = new EventEmitter<IFlashCard>()

  timeLeft: string | undefined

  private timerSubscription: Subscription;

  constructor(private serviceTimerTicker: TimertickerService) {

    this.timerSubscription = this.serviceTimerTicker.getTimerObservable().subscribe(() => {
      this.checkTime();
    });

  }

  ngOnInit() {
    this.checkTime()
  }

  onClichForPlay() {
    if(!this.isDisabled)
      this.goPlayThisCard.emit(this.card)
  }

  checkTime() {
    const currentTime = new Date()
    const cardNextExamDate = new Date(this.card!.nextExamDate)

    if(currentTime < cardNextExamDate){
      // Calculate the difference in milliseconds
      const differenceInMilliseconds = Math.abs(currentTime.getTime() - cardNextExamDate.getTime());

      // Convert to hours, minutes, and seconds
      const hours = Math.floor(differenceInMilliseconds / 3600000); // 1 hour = 3600000 milliseconds
      const minutes = Math.floor((differenceInMilliseconds % 3600000) / 60000); // 1 minute = 60000 milliseconds
      const seconds = Math.floor((differenceInMilliseconds % 60000) / 1000); // 1 second = 1000 milliseconds

      // Format the time difference as hh:mm:ss
      const formatTime = (timeValue: number) => timeValue.toString().padStart(2, '0');
      this.timeLeft = `${formatTime(hours)}:${formatTime(minutes)}:${formatTime(seconds)}`;
    }
    else
    this.timeLeft = undefined

    this.isDisabled = currentTime < cardNextExamDate
  }

  ngOnDestroy() {
      if (this.timerSubscription) {
        this.timerSubscription.unsubscribe();
    }
  }

}
