import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
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
*/

export class CreateThoughtComponent {
  submitted: boolean = false

  constructor(
    private modalService: ModalService,
    public nodeDetailService: NodeDetailService) {}

  creatingForm = new FormGroup({
    thoughtText: new FormControl<string>('', [Validators.required]),
    thoughtDescription: new FormControl<string>('')
  })

  get thoughtText() {
    return this.creatingForm.controls.thoughtText as FormControl
  }

  submit() {

/*     this.submitted = true
    if (this.creatingForm.valid) {

      this.nodeDetail.create({
        name: this.creatingForm.value.terrainName as string,
        description: this.creatingForm.value.terrainDescription as string
      }).subscribe(() => {
        this.modalService.close()
      })
    } */

  }

  close() {
    this.modalService.close()
  }
}
