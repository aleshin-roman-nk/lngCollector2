import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IThought } from 'src/app/models/thought';
import { UserOperationEnum } from 'src/app/models/user-operation';
import { UserResponse } from 'src/app/models/user-response';
import { ModalService } from 'src/app/services/modal.service';
import { NodeDetailService } from 'src/app/services/node-detail.service';
import { TerriansService } from 'src/app/services/terrians.service';

@Component({
  selector: 'app-create-thought',
  templateUrl: './create-thought.component.html',
  styleUrls: ['./create-thought.component.css']
})

/*
Этот компонент должен вернуть созданный объект
Либо вызвать метод сервиса, который затем внутри себя все обновит
Тогда сервис должен хранить объект, обновляя его состояние

26.06.2023
Для создания Thought требуется указатель nodeId
Я хочу, чтобы этот компонент только получил необходжимые данные Thought и вернул созданный объект
*/

export class CreateThoughtComponent {
  submitted: boolean = false
  isShown: boolean = false

  finished: EventEmitter<UserResponse<{ text: string, descr: string }>> = new EventEmitter<UserResponse<{ text: string, descr: string }>>()

  constructor(
    public nodeDetailService: NodeDetailService) { }

  creatingForm = new FormGroup({
    thoughtText: new FormControl<string>('', [Validators.required]),
    thoughtDescription: new FormControl<string>('')
  })

  get thoughtText() {
    return this.creatingForm.controls.thoughtText as FormControl
  }
  get thoughtDescription() {
    return this.creatingForm.controls.thoughtDescription as FormControl
  }

  openDialog() {
    this.isShown = true
  }

  submit() {
    this.submitted = true
    if (this.creatingForm.valid) {
      this.finished.emit(new UserResponse({ text: this.thoughtText.value, descr: this.thoughtDescription.value }, UserOperationEnum.create))
    }
    this.isShown = false
  }

  refused() {
    this.isShown = false
  }
}
