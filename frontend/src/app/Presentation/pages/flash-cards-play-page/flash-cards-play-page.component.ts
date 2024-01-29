import { Component, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CardInputComponent } from '../../comps-play/card-input/card-input.component';
import { IFlashCard } from 'src/app/Core/Models/flash-card';
import { FlashCardService } from 'src/app/Core/services/flash-card.service';
import { CardCheckResultComponent } from '../../comps-play/card-check-result/card-check-result.component';
import { IFlashCardAnswer } from 'src/app/Core/Models/flash-card-answer';

@Component({
  selector: 'app-question-work-page',
  templateUrl: './flash-cards-play-page.component.html',
  styleUrls: ['./flash-cards-play-page.component.css']
})
export class FlashCardsPlayPageComponent {

  @ViewChild("inputCardDlg", { static: false }) inputCardDlg!: CardInputComponent
  @ViewChild("checkResultDlg", { static: false }) checkResultDlg!: CardCheckResultComponent

  cards: IFlashCard[] = []

  nodeId: number
  loading: boolean = false

  constructor(
    private activateRoute: ActivatedRoute,
    public flashCardService: FlashCardService
    ) {
    this.activateRoute.params.subscribe(params => {
      this.nodeId = params['id']
    })
  }

  ngOnInit(): void {

    this.loading = true

    this.flashCardService
      .getCardsForPlay(this.nodeId, new Date())
      .subscribe((result) => {

        this.cards = result
        .filter(x => x.answers?.length)
        .sort((a, b) => new Date(a.nextExamDate).getTime() - new Date(b.nextExamDate).getTime())

        this.loading = false
      })
  }

  ngAfterViewInit() {
    this.inputCardDlg.finished
    .subscribe((data) => {

      let helpUsed: boolean = data.helpIsShown

      this.flashCardService.checkFlashCard({
        cardId: data.cardId,
        solution: data.answer,
        helpIsUsed: data.helpIsShown
      })
      .subscribe(resp => {

        const i = this.cards.findIndex((item) => item.id === resp.cardId)
        let answers: IFlashCardAnswer[] = []
        let question: string = ""
        if(i !== -1){
          //this.cards[i].NextExamDate = resp.nextExamDate // так неправильно, не происходит реактивного обновления
          this.cards[i] = {
            ...this.cards[i],
            nextExamDate: resp.nextExamDate,
            hitsInRow: resp.hitsInRow,
            totalHits: resp.totalHits,
            level: resp.level
          };
          answers = this.cards[i].answers!
          question = this.cards[i].question!
        }

        //this.cards = this.cards.sort((a, b) => new Date(a.NextExamDate).getTime() - new Date(b.NextExamDate).getTime())

        this.cards = [...this.cards].sort((a, b) => new Date(a.nextExamDate).getTime() - new Date(b.nextExamDate).getTime());

        if(!helpUsed)
          this.checkResultDlg.openDialog(resp.isCorrect, question, answers)
      })
    })
  }

  goInputCardDlg(c: IFlashCard) {
    this.inputCardDlg.openDialog(c)
  }
}
