import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { LangService } from 'src/app/Core/services/lang.service';
import { ILanguage } from 'src/app/Core/Models/language';
import { IFlashCardAnswer } from 'src/app/Core/Models/flash-card-answer';
import { UserFormResultSimple } from '../../Models/user-form-result-simple';
import { isNullOrWhiteSpace } from '../../functions/isNullOrWhiteSpace';
import { ErrorHandlerService } from 'src/app/Core/services/error-handler.service';

/**
 * Модалка редактирования flash-card-answer
 */

@Component({
  selector: 'app-edit-flash-card-answer-modal',
  templateUrl: './edit-flash-card-answer-modal.component.html',
  styleUrls: ['./edit-flash-card-answer-modal.component.css']
})
export class EditFlashCardAnswerModalComponent {

  cardAnswer: IFlashCardAnswer

  @Output()
  finished = new EventEmitter<UserFormResultSimple<IFlashCardAnswer>>()

  @Output()
  finishedDelete = new EventEmitter<IFlashCardAnswer>()

  isShown: boolean = false

  flashCardAnswerForm: FormGroup

  submitted: boolean = false

  @Input()
  languages: ILanguage[] = []
  selectedLanguage: ILanguage | undefined

  constructor(
    ){
    this.flashCardAnswerForm = new FormGroup({
      cardAnswerTextControl: new FormControl<string>(this.cardAnswer?.text ?? '', [Validators.required, isNullOrWhiteSpace()]),
      lngControl: new FormControl<ILanguage | undefined>(this.selectedLanguage, [Validators.required])
    })
  }

  get CardAnswerTextControl() {
    return (this.flashCardAnswerForm.controls.cardAnswerTextControl as FormControl)
  }

  get LngControl() {
    return (this.flashCardAnswerForm.controls.lngControl as FormControl)
  }

  openEditDialog(cardAnswer: IFlashCardAnswer){

    if(!cardAnswer.cardId) throw new Error('cardId must not be 0 or null');

    this.cardAnswer = cardAnswer
    this.isShown = true
    this.submitted = false

    if(cardAnswer.language)
      this.selectedLanguage = this.languages.find(x => x.id == cardAnswer.language?.id)
    else
      this.selectedLanguage = undefined

    this.flashCardAnswerForm.patchValue({
      cardAnswerTextControl: this.cardAnswer?.text ?? '',
      lngControl: this.selectedLanguage
    })

  }

  openCreateDialog(cardId: number){

    this.cardAnswer = {
      cardId: cardId,
      id: 0
    }

    this.isShown = true
    this.submitted = false

    this.selectedLanguage = undefined

     this.flashCardAnswerForm.patchValue({
      cardAnswerTextControl: this.cardAnswer?.text ?? '',
      lngControl: this.selectedLanguage
    })

  }

  refuse() {
    this.submitted = false
    this.isShown = false
  }

  delete(){
    this.isShown = false
    this.finishedDelete.emit(this.cardAnswer)
  }

  submit() {
    this.submitted = true

    if (!this.flashCardAnswerForm.valid) return;

    this.submitted = false

    this.cardAnswer.text = this.CardAnswerTextControl.value
    this.cardAnswer.language = this.LngControl.value
    this.finished.emit(new UserFormResultSimple<IFlashCardAnswer>(this.cardAnswer))

    this.isShown = false
  }

}
