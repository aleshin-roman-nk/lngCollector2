import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IThExpression } from 'src/app/Core/Models/thexpression';
import { ThExpressionType } from 'src/app/Core/Models/thexpression-type';
import { UserOperationEnum } from 'src/app/Presentation/Models/user-operation';
import { UserResponse } from 'src/app/Presentation/Models/user-response';
import { WhoMade } from 'src/app/Core/Models/whomade';
import { LangService } from 'src/app/Core/services/lang.service';
import { ILanguage } from 'src/app/Core/Models/language';

@Component({
  selector: 'app-edit-thexpression',
  templateUrl: './edit-thexpression.component.html',
  styleUrls: ['./edit-thexpression.component.css']
})
export class EditThexpressionComponent {
  finished: EventEmitter<UserResponse<IThExpression>> = new EventEmitter<UserResponse<IThExpression>>()
  updateStringProperty: EventEmitter<{id: number, propName: string, newValue: string}> = new EventEmitter<{id: number, propName: string, newValue: string}>()

  isShown: boolean = false

  thoughtId: number
  thExpresion: IThExpression | undefined

  creatingForm: FormGroup

  submittingTried: boolean = false

  langs: ILanguage[] = []
  selectedLanguage: ILanguage | undefined

  constructor(private lngSrv: LangService){
    lngSrv.getAll().subscribe(resp => {
      this.langs = resp.Content
    })

    this.creatingForm = new FormGroup({
      textControl: new FormControl<string>(this.thExpresion?.text ?? '', [Validators.required]),
      lngControl: new FormControl<ILanguage | undefined>(this.selectedLanguage, [Validators.required])
    })
  }

  ngOnInit() {

  }

  ngAfterViewInit(){
  }

  get TextControl() {
    return (this.creatingForm.controls.textControl as FormControl)
  }

  get LngControl() {
    return (this.creatingForm.controls.lngControl as FormControl)
  }

  openDialog(thId: number, thexpr: IThExpression | undefined){
    this.thoughtId = thId
    this.thExpresion = thexpr
    this.isShown = true
    this.submittingTried = false

    if(thexpr)
      this.selectedLanguage = this.langs.find(x => x.id == thexpr.lngId)
    else
      this.selectedLanguage = this.langs[0]

    this.creatingForm.patchValue({
      textControl: this.thExpresion?.text ?? '',
      lngControl: this.selectedLanguage
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
        lngId: this.LngControl.value.id,
        text: this.TextControl.value,
        createdDate: new Date(),
        id: 0,
        madeBy: WhoMade.Natives,
        scores: 0,
        thoughtId: this.thoughtId,
        type: ThExpressionType.expression
      }, UserOperationEnum.create))      

    }
    else{
      this.thExpresion.text = this.TextControl.value
      this.thExpresion.lngId = this.LngControl.value.id
      this.finished.emit(new UserResponse<IThExpression>(this.thExpresion, UserOperationEnum.update)) 
    }

    this.isShown = false
  }

/*   submitStringProperty(){

  } */
}
