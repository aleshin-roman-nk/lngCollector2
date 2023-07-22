import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IThExpression } from 'src/app/models/thexpression';
import { ThExpressionType } from 'src/app/models/thexpression-type';
import { UserResponse } from 'src/app/models/user-response';
import { WhoMade } from 'src/app/models/whomade';

@Component({
  selector: 'app-edit-thexpression',
  templateUrl: './edit-thexpression.component.html',
  styleUrls: ['./edit-thexpression.component.css']
})
export class EditThexpressionComponent {
  @Output() finished: EventEmitter<UserResponse<IThExpression>> = new EventEmitter<UserResponse<IThExpression>>()
  @Input() thoughtId: number
  @Input() thExpresion!: IThExpression | undefined

  creatingForm: FormGroup

  submittingTried: boolean = false

  langs: { name: string, id: number }[] = [{ name: "Russian", id: 1 }, { name: "English", id: 2 }, { name: "Turkish", id: 3 }]

  ngOnInit() {
    this.creatingForm = new FormGroup({
      text: new FormControl<string>(this.thExpresion?.text ?? '', [Validators.required]),
      lngId: new FormControl<number>(this.firstOrUndefined(this.langs)?.id ?? 0, [Validators.required])
    })
  }

  private firstOrUndefined<T>(arr: T[]): T | undefined {
    return arr.length > 0 ? arr[0] : undefined;
  }

  get Text() {
    return this.creatingForm.controls.text as FormControl
  }

  get LngId() {
    return this.creatingForm.controls.lngId as FormControl
  }

  refuse() {
    this.finished.emit(new UserResponse<IThExpression>(undefined, false))
  }

  submit() {
    this.submittingTried = true

    if (!this.creatingForm.valid) return;

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
      }, true))      

    }
    else{
      this.thExpresion.text = this.Text.value
      this.finished.emit(new UserResponse<IThExpression>(this.thExpresion, true)) 
    }
  }
}
