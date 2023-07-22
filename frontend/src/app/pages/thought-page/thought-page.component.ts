import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IThExpression } from 'src/app/models/thexpression';
import { IThought } from 'src/app/models/thought';
import { UserResponse } from 'src/app/models/user-response';
import { ThoughtService } from 'src/app/services/thought.service';

@Component({
  selector: 'app-thought-page',
  templateUrl: './thought-page.component.html',
  styleUrls: ['./thought-page.component.css']
})
export class ThoughtPageComponent {

  thoughtId: number
  thought: IThought
  loaded: boolean = false

  creatingExpression: boolean = false
  editingExpression: boolean = false

  expressionToEdit: IThExpression | undefined

  constructor(
    private activateRoute: ActivatedRoute,
    private thoughtService: ThoughtService
    ) { }

  ngOnInit(){
    this.activateRoute.params.subscribe(params => {
      this.thoughtId = params['id'];
    });

    this.thoughtService.getThought(this.thoughtId)
      .subscribe(data => {
        this.thought = data
        this.loaded = true
      })
  }

  startCreatingExpression(){
    this.creatingExpression = true
  }

  startEditingExpression(thexpr: IThExpression){
    this.expressionToEdit = thexpr
    this.editingExpression = true
  }

  /**
   * Что если сделать единый finished, а в сообщении уже смотреть
   * - переименовать UserMessage
   * - добавить message-type [accepted, rejected, delete]
   *  
   */
  finishedCreatingExpression(event: UserResponse<IThExpression>){

    if(event.hasUserAccepted){

      if(!event.value) return

      this.thoughtService.createExpression(this.thoughtId, event.value)
      .subscribe(data => {
        //console.log(data)
        this.thought.expressions.push(data)
      })
    }

    this.creatingExpression = false
  }

  finishedEditingExpression(event: UserResponse<IThExpression>){

    if(event.hasUserAccepted && event.value){
/*       this.thoughtService.createExpression(this.thoughtId, event.value)
      .subscribe(data => {

        this.thought.expressions.push(data)
      }) */

      this.thoughtService.updateExpression(event.value)
      .subscribe(data => {

      })
    }

    this.editingExpression = false
  }

}
