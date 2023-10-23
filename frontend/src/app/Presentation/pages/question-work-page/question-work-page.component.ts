import { Component, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CardInputComponent } from '../../comps-play/card-input/card-input.component';
import { IFlashCard } from 'src/app/Core/Models/flash-card';

@Component({
  selector: 'app-question-work-page',
  templateUrl: './question-work-page.component.html',
  styleUrls: ['./question-work-page.component.css']
})
export class QuestionWorkPageComponent {

@ViewChild("inputCardDlg", { static : false }) inputCardDlg!: CardInputComponent

cards: IFlashCard[] = [
  {
    question: "Dogs do not fly",
    answer: "Собаки не летают",
    answerLng: "RUS"
  },
  {
    question: "Dogs do not flay 1",
    answer: "Собаки не летают 1",
    answerLng: "RUS"
  },
  {
    question: "This morning is beautiful",
    answer: "Это утро прекрасно",
    answerLng: "RUS"
  },
  {
    question: "I love my wife",
    answer: "Я люблю мою жену",
    answerLng: "RUS"
  },
  {
    question: "My son is handsome",
    answer: "Мой сын красив",
    answerLng: "RUS"
  },
  {
    question: "We do not have a cat",
    answer: "У нас нету кошки",
    answerLng: "RUS"
  }
]



  nodeId: number

  constructor(private activateRoute: ActivatedRoute) {
    this.activateRoute.params.subscribe(params => {
      this.nodeId = params['id']
    })
  }

  goCardDlg(c: IFlashCard){
    this.inputCardDlg.openDialog(c)
  }
}
