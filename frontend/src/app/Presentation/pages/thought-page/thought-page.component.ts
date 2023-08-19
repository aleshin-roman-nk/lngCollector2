import { Component, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { EditThexpressionComponent } from 'src/app/Presentation/comps-edit/edit-thexpression/edit-thexpression.component';
import { TextInputComponent } from 'src/app/Presentation/comps-tools/text-input/text-input.component';
import { IThExpression } from 'src/app/Core/Models/thexpression';
import { IThought } from 'src/app/Core/Models/thought';
import { UserOperationEnum } from 'src/app/Presentation/Models/user-operation';
import { ThoughtService } from 'src/app/Core/services/thought.service';

@Component({
  selector: 'app-thought-page',
  templateUrl: './thought-page.component.html',
  styleUrls: ['./thought-page.component.css']
})
export class ThoughtPageComponent {

  @ViewChild(EditThexpressionComponent, {static: false}) editThExpression!: EditThexpressionComponent
  @ViewChild("inputText", {static: false}) textInput!: TextInputComponent
  @ViewChild("inputDescription", {static: false}) descriptionInput!: TextInputComponent

  thoughtId: number
  thought: IThought
  loaded: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false)

  creatingExpression: boolean = false
  editingExpression: boolean = false

  constructor(
    private activateRoute: ActivatedRoute,
    private thoughtService: ThoughtService
    ) { }

  ngOnInit(){
    this.activateRoute.params.subscribe(params => {
      this.thoughtId = params['id'];
    });

    this.thoughtService.getThought(this.thoughtId)
      .subscribe(resp => {
        this.thought = resp.Content
        this.loaded.next(true)
      })    
  }

  ngAfterViewInit(){

    // Subscribing edit expression dialog results
    this.editThExpression.finished.subscribe(data => {

      if(!data.value) return

      switch(data.userOperation){
        case UserOperationEnum.create:
          this.createThExpression(data.value)
          break
        case UserOperationEnum.update:
          this.updateThExpression(data.value)
          break
        case UserOperationEnum.delete:
         this.deleteThExpression(data.value.id)
         break
        default:
      }

    })    

    this.textInput.accepted.subscribe(data => {
      this.thought.text = data
      this.thoughtService.updateThoughtStrginProperty(this.thoughtId, "text", data)
      .subscribe()
    }) 

    this.descriptionInput.accepted.subscribe(data => {
      this.thought.description = data
    }) 

  }

  onInputText(){
    this.textInput.value = this.thought.text
    this.textInput.isShown = true
  }

  onInputDescription(){
    this.descriptionInput.value = this.thought.description
    this.descriptionInput.isShown = true
  }

  openEditTheExpression(o: IThExpression | undefined){
    this.editThExpression.openDialog(this.thoughtId, o)
  }

  /**
   * Что если сделать единый finished, а в сообщении уже смотреть
   * - переименовать UserMessage
   * - добавить message-type [accepted, rejected, delete]
   *  
   */
  createThExpression(o: IThExpression){
      this.thoughtService.createExpression(this.thoughtId, o)
      .subscribe(resp => {
        this.thought.expressions.push(resp.Content)
      })
  }

  updateThExpression(o: IThExpression){

    this.thoughtService.updateExpression(o)
    .subscribe()
  }

  deleteThExpression(expId: number){
    this.thoughtService.deleteExpression(expId)
    .subscribe(resp => { 
      const i = this.thought.expressions.findIndex((item) => item.id === expId)
      if(i !== -1) this.thought.expressions.splice(i, 1);
    })
  }

}
