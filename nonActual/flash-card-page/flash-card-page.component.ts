import { Component, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BehaviorSubject, catchError, map, of, switchMap, tap } from 'rxjs';
import { IFlashCard } from 'src/app/Core/Models/flash-card';
import { IFlashCardAnswer } from 'src/app/Core/Models/flash-card-answer';
import { FlashCardService } from 'src/app/Core/services/flash-card.service';
import { EditFlashCardAnswerComponent } from 'src/app/Presentation/comps-edit/edit-flash-card-answer/edit-flash-card-answer.component';
import { TextInputComponent } from 'src/app/Presentation/comps-tools/text-input-inline/text-input-inline.component';
import { UserOperationEnum } from 'src/app/Presentation/Models/user-operation';

@Component({
  selector: 'app-thought-page',
  templateUrl: './flash-card-page.component.html',
  styleUrls: ['./flash-card-page.component.css']
})
export class FlashCardPageComponent {

  @ViewChild("editFlashCardAnswer", { static: false }) editFlashCardAnswer!: EditFlashCardAnswerComponent
  @ViewChild("inputQuestion", { static: false }) cardQuestionInput!: TextInputComponent
  @ViewChild("inputDescription", { static: false }) descriptionInput!: TextInputComponent

  //cardId: number
  card: IFlashCard
  loaded: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false)
  isNewCard: boolean = false

  creatingExpression: boolean = false
  editingExpression: boolean = false

  constructor(
    private activateRoute: ActivatedRoute,
    private router: Router,
    private flashCardService: FlashCardService
  ) { }

  ngOnInit() {
    //analyzing route

 /*    this.activateRoute.url.pipe(

      switchMap(segments => {
        if (segments.length === 4 && segments[3].path === "new") {
          this.isNewCard = true
        }

        return this.activateRoute.params
      })
    )
      .subscribe(params => {
        if (this.isNewCard) {
          this.card = {
            nodeId: params['nodeId'],
            id: 0,
            nextExamDate: new Date(),
            question: "",
            description: ""
          }
        }
      })

    if (!this.isNewCard) {
      let cardId: number

      this.activateRoute.params.pipe(
        switchMap(params => {
          cardId = params['id']
          return this.flashCardService.getSingleFlashCard(cardId)
        })
      )
        .subscribe(resp => {
          this.card = resp
          this.loaded.next(true)
        })
    } */


    this.activateRoute.url.pipe(
      map(segments => segments.length === 4 && segments[3].path === "new"),
      tap(isNew => this.isNewCard = isNew),
      switchMap(isNew => {
        if (isNew) {
          return this.activateRoute.params.pipe(
            map(params => ({
              nodeId: params['nodeId'],
              id: 0,
              nextExamDate: new Date(),
              question: "",
              description: ""
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
    ).subscribe(card => {
      if (card) {
        this.card = card;
        if (!this.isNewCard) {
          this.loaded.next(true);
        }
      }
    });


  }

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

        this.cardQuestionInput.accepted.subscribe(data => {
          this.card.question = data
           this.flashCardService.CardUpdateString(this.card.id, "text", data)
          .subscribe()
        })

        this.descriptionInput.accepted.subscribe(data => {
          this.card.description = data
        })

  }

  onShowCardQuestionInput() {
      this.cardQuestionInput.value = this.card.question!
      this.cardQuestionInput.isShown = true
  }

  onShowInputDescription() {
      this.descriptionInput.value = this.card.description ?? ""
      this.descriptionInput.isShown = true
  }

  openEditFlashCardAnswer(o: IFlashCardAnswer | undefined) {
    /*     this.editThExpression.openDialog(this.thoughtId, o) */
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

  updateThExpressionStringProperty(id: number, propName: string, newValue: string) {
    /*     this.thExpressionSrv.updateStringProperty(id, propName, newValue)
        .subscribe() */
  }

  deleteThExpression(expId: number) {
    /*     this.thExpressionSrv.deleteExpression(expId)
        .subscribe(resp => {
          const i = this.thought.expressions.findIndex((item) => item.id === expId)
          if(i !== -1) this.thought.expressions.splice(i, 1);
        }) */
  }


  createCard(o: { text: string, descr: string }) {

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

  }



}
