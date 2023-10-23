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

  finished: EventEmitter<{ answer: string, cardId: number, needHelp: boolean }> = new EventEmitter<{ answer: string, cardId: number, needHelp: boolean }>()

  openDialog(c: IFlashCard) {
    this.card = c
    this.answer = ""
    this.isShown = true
  }

  accept(){
    this.isShown = false
  }

}
