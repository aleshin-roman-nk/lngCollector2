import { Component, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BehaviorSubject, catchError, delay, first, map, of, switchMap, tap } from 'rxjs';
import { ICreateFlashCardDto, IFlashCard } from 'src/app/Core/Models/flash-card';
import { IFlashCardAnswer } from 'src/app/Core/Models/flash-card-answer';
import { ILanguage } from 'src/app/Core/Models/language';
import { FlashCardService } from 'src/app/Core/services/flash-card.service';
import { LangService } from 'src/app/Core/services/lang.service';
import { EditFlashCardAnswerModalComponent } from 'src/app/Presentation/comps-edit/edit-flash-card-answer/edit-flash-card-answer-modal.component';
import { TextInputComponent } from 'src/app/Presentation/comps-tools/text-input-inline/text-input-inline.component';
import { ModalMessageComponent } from '../../comps-tools/modal-message/modal-message.component';
import { isStringEmptyOrUndefined } from '../../functions/isStringEmptyOrUndefined';

@Component({
  selector: 'app-thought-page',
  templateUrl: './flash-card-page.component.html',
  styleUrls: ['./flash-card-page.component.css']
})
export class FlashCardPageComponent {

  @ViewChild("editFlashCardAnswer", { static: false }) editFlashCardAnswer!: EditFlashCardAnswerModalComponent
  @ViewChild("inputQuestion", { static: false }) cardQuestionInput!: TextInputComponent
  @ViewChild("inputDescription", { static: false }) descriptionInput!: TextInputComponent
  @ViewChild("modalMessage", { static: false }) modalMessage!: ModalMessageComponent

/**
 * Переносим удаление сюда.
 * Здесь добавим кнопку удалить, а не в компоненте
 */

  cardQuestion: string
  cardDesctiption: string

  card: IFlashCard
  loaded: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false)
  isNewCard: boolean = false

  creatingExpression: boolean = false
  editingExpression: boolean = false

  langs: ILanguage[] = []
  selectedLng: ILanguage

  savingTried: boolean = false

  constructor(
    private activateRoute: ActivatedRoute,
    private router: Router,
    private flashCardService: FlashCardService,
    private serviceLanguage: LangService
  ) {
    serviceLanguage.getAll().subscribe(resp => {
      this.langs = resp
    })
  }

  ngOnInit() {
    //analyzing route

    this.activateRoute.url
    .pipe(
      map(segments => segments.length === 4 && segments[3].path === "new"),
      tap(isNew => this.isNewCard = isNew),
      switchMap(isNew => {
        if (isNew) {// вообще, если карточка новая, нужно открыть окно именно создания карточки, а потом от него уже переходить на страницу правки карточки
          return this.activateRoute.params.pipe(
            map(params => ({
              nodeId: params['nodeId'],
              id: 0,
              nextExamDate: new Date(),
              question: "",
              description: "",
              questPrice: 0,
              hitsInRow: 0,
              isCompleted: false,
              level: 0,
              requiredHits: 0,
              totalHits: 0
            }))
          );
        } else {
          return this.activateRoute.params.pipe(
            switchMap(params => this.flashCardService.getSingleFlashCard(params['id']))
          );
        }
      }),
      catchError(error => {
        // Handle error
        return of(null);
      })
    )
    .subscribe(card => {
      if (card) {
        this.card = card;


        this.selectedLng = this.langs.find(l => l.id === this.card.language?.id)!


        if (!this.isNewCard) {
          this.loaded.next(true);
        }
      }
    });


  }

/**
 * Сами определяем, если flash-card-answer.id === 0 то вызываем создаение, иначе вызвать обновить
 */

  ngAfterViewInit() {



    // Subscribing edit expression dialog results
/*          this.editThExpression.finished.subscribe(data => {

          if(!data.value) return

          switch(data.userOperation){
            case UserOperationEnum.create:
              this.createThExpression(data.value)
              break
            case UserOperationEnum.update:
              this.updateThExpressionStringProperty(data.value.id, "text", data.value.text)
              break
            case UserOperationEnum.delete:
             this.deleteThExpression(data.value.id)
             break
            default:
          }

        }) */

/*         this.cardQuestionInput.accepted.subscribe(data => {
          this.card.question = data
           this.flashCardService.CardUpdateString(this.card.id, "text", data)
          .subscribe()
        }) */

/*         this.descriptionInput.accepted.subscribe(data => {
          this.card.description = data
        }) */

        this.editFlashCardAnswer.finished.subscribe(res => {
          if(res.value?.id === 0)
            {
              this.createCardAnswer(res.value)
            }
            else{
              this.updateCardAnswer(res.value!)
            }
        })

        this.editFlashCardAnswer.finishedDelete.subscribe((ca) => {
          this.deleteCardAnswer(ca)
        })
  }

  onSaveCard() {

    this.savingTried = true

    if(isStringEmptyOrUndefined(this.card.question)) return
    if (!this.selectedLng) return

    if (this.isNewCard)
          this.createCard()
        else this.updateCard()
  }

  private createCard() {
    const newCard: ICreateFlashCardDto = {
      languageId: this.selectedLng.id,
      nextExamDate: new Date(),
      nodeId: this.card.nodeId,
      requiredHits: this.card.requiredHits!,
      description: this.card.description,
      question: this.card.question
    }

    this.flashCardService.createCard(newCard).subscribe(resp => {
/*       this.card = resp
      this.isNewCard = this.card.id === 0 */
      this.router.navigate(['/flash-card', resp.id])
    })
  }

  private updateCard() {
    this.flashCardService.updateCard({
      id: this.card.id,
      languageId: this.selectedLng.id,
      //nextExamDate: this.card.NextExamDate,
      //points: this.card.points!,
      //requiredPoints: this.card.requiredPoints!,
      description: this.card.description,
      question: this.card.question
    }).subscribe(resp => {
      this.modalMessage.openDialog(`Card #${resp.id} has been successfully updated`)
    })
  }

  private updateCardAnswer(answ: IFlashCardAnswer) {
    this.flashCardService.updateCardAnswer({
      id: answ.id,
      languageId: answ.language?.id!,
      text: answ.text
    }).subscribe()
  }

  private createCardAnswer(answ: IFlashCardAnswer){
    this.flashCardService.createCardAnswer({
      cardId: answ.cardId,
      lngId: answ.language?.id!,
      text: answ.text!
    })
    .pipe(first())
    .subscribe(resp => {
      this.card.answers?.push(resp)
    })
  }

  goEditFlashCardAnswer(o: IFlashCardAnswer) {
    this.editFlashCardAnswer.openEditDialog(o)
  }

  goCreateFlashCardAnswer() {
    this.editFlashCardAnswer.openCreateDialog(this.card.id)
  }

  onDeleteCard() {
    /*     this.thoughtService.deleteThought(this.thoughtId)
          .subscribe(resp => {
            this.router.navigate(['/node', this.thought.nodeId, 'detail'])
          }) */
  }

  /**
   * Что если сделать единый finished, а в сообщении уже смотреть
   * - переименовать UserMessage
   * - добавить message-type [accepted, rejected, delete]
   *
   */
  /*   createThExpression(o: IThExpression){
        this.thExpressionSrv.createExpression(this.thoughtId, o)
        .subscribe(resp => {
          this.thought.expressions.push(resp.Content)
        })
    } */

  deleteCardAnswer(ca: IFlashCardAnswer) {
      this.flashCardService.deleteCardAnswer(ca.id).subscribe(() => {

        const i = this.card.answers?.findIndex((item) => item.id === ca.id)
        if(i !== -1 && i) this.card.answers?.splice(i!, 1)

      })
  }


  //createCard(o: { text: string, descr: string }) {

    /*     const th: IThought  = {
          id: 0,
          nodeId: this.nodeId,
          text: o.text,
          description: o.descr,
          createdDate: new Date(),
          expressions: []
        } */

    /**
     * получается здесь после отработки createThought один раз по завершении выполняется
     * .subscribe(...) и все забыли
     *
     * https://rxjs.dev/guide/observable
     * To invoke the Observable and see these values, we need to subscribe to it:
     */
    /*     this.nodeDetailSrv.createThought(this.nodeId, th)
          .subscribe(resp => {

            if(resp.Success){
              this.nodeDetail.Thoughts.push(resp.Content)
            }
          }) */

  //}

  checkString(str: string | undefined): boolean{
    return isStringEmptyOrUndefined(str)
  }

}
