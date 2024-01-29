import { Component } from '@angular/core';
import { IFlashCardAnswer } from 'src/app/Core/Models/flash-card-answer';

@Component({
  selector: 'app-card-check-result',
  templateUrl: './card-check-result.component.html',
  styleUrls: ['./card-check-result.component.css']
})
export class CardCheckResultComponent {
  openModal: boolean

  checkText: string
  isCorrect: boolean
  answers: IFlashCardAnswer[]
  question: string

  openDialog(correct: boolean, question: string, answers: IFlashCardAnswer[]) {
    this.isCorrect = correct
    this.openModal = true

    this.answers = answers
    this.question = question

    this.checkText = this.isCorrect ? "CORRECT" : "INCORRECT"
  }
}
