import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IThExpression } from 'src/app/models/thexpression';
import { ThExpressionType } from 'src/app/models/thexpression-type';
import { UserOperationEnum } from 'src/app/models/user-operation';
import { UserResponse } from 'src/app/models/user-response';
import { WhoMade } from 'src/app/models/whomade';

@Component({
  selector: 'app-edit-thexpression',
  templateUrl: './edit-thexpression.component.html',
  styleUrls: ['./edit-thexpression.component.css']
})
export class EditThexpressionComponent {
  finished: EventEmitter<UserResponse<IThExpression>> = new EventEmitter<UserResponse<IThExpression>>()

  isShown: boolean = false

  thoughtId: number
  thExpresion: IThExpression | undefined

  creatingForm: FormGroup

  submittingTried: boolean = false

  langs: { name: string, id: number }[] = [{ name: "Russian", id: 1 }, { name: "English", id: 2 }, { name: "Turkish", id: 3 }]

  ngOnInit() {

  }

  ngAfterViewInit(){
  }

  private firstOrUndefined<T>(arr: T[]): T | undefined {
    return arr.length > 0 ? arr[0] : undefined;
  }

  get Text() {
    return (this.creatingForm.controls.text as FormControl)
  }

  get LngId() {
    return (this.creatingForm.controls.lngId as FormControl)
  }

  openDialog(thId: number, thexpr: IThExpression | undefined){
    this.thoughtId = thId
    this.thExpresion = thexpr
    this.isShown = true

    this.creatingForm = new FormGroup({
      text: new FormControl<string>(this.thExpresion?.text ?? '', [Validators.required]),
      lngId: new FormControl<number>(this.firstOrUndefined(this.langs)?.id ?? 0, [Validators.required])
    })
  }

  refuse() {
    this.submittingTried = false
    this.isShown = false
  }

  delete(){
    this.finished.emit(new UserResponse<IThExpression>(this.thExpresion, UserOperationEnum.delete))
    this.isShown = false
  }

  submit() {
    this.submittingTried = true

    if (!this.creatingForm.valid) return;

    this.submittingTried = false

    if(this.thExpresion === undefined){

      this.finished.emit(new UserResponse<IThExpression>({
        lngId: this.LngId.value,
        text: this.Text.value,
        createdDate: new Date(),
        id: 0,
        madeBy: WhoMade.Natives,
        scores: 0,
        thoughtId: this.thoughtId,
        type: ThExpressionType.expression
      }, UserOperationEnum.create))      

    }
    else{
      this.thExpresion.text = this.Text.value
      this.finished.emit(new UserResponse<IThExpression>(this.thExpresion, UserOperationEnum.update)) 
    }

    this.isShown = false
  }
}
