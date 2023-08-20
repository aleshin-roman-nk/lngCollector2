import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IThought } from 'src/app/Core/Models/thought';
import { UserOperationEnum } from 'src/app/Presentation/Models/user-operation';
import { UserResponse } from 'src/app/Presentation/Models/user-response';
import { ModalService } from 'src/app/Presentation/services/modal.service';
import { NodeDetailService } from 'src/app/Core/services/node-detail.service';
import { TerriansService } from 'src/app/Core/services/terrians.service';

@Component({
  selector: 'app-edit-thought',
  templateUrl: './edit-thought.component.html',
  styleUrls: ['./edit-thought.component.css']
})

/*
Этот компонент должен вернуть созданный объект
Либо вызвать метод сервиса, который затем внутри себя все обновит
Тогда сервис должен хранить объект, обновляя его состояние

26.06.2023
Для создания Thought требуется указатель nodeId
Я хочу, чтобы этот компонент только получил необходжимые данные Thought и вернул созданный объект
*/

export class EditThoughtComponent {
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
