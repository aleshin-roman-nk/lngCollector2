import { Component, EventEmitter, Input } from '@angular/core';
import { IFlashCard } from 'src/app/Core/Models/flash-card';

@Component({
  selector: 'app-card-input',
  templateUrl: './card-input.component.html',
  styleUrls: ['./card-input.component.css']
})
export class CardInputComponent {

  answer: string

  card: IFlashCard

  isShown: boolean = false

  helpShown: boolean = false

  finished: EventEmitter<{ answer: string, cardId: number, helpIsShown: boolean }>
    = new EventEmitter<{ answer: string, cardId: number, helpIsShown: boolean }>()

  openDialog(c: IFlashCard) {
    this.card = c
    this.answer = ""
    this.isShown = true
    this.helpShown = false
  }

  btnHelp() {
    this.helpShown = true
  }

  accept() {
    this.isShown = false
    this.finished.emit({ answer: this.answer, cardId: this.card.id, helpIsShown: this.helpShown })
  }

}
