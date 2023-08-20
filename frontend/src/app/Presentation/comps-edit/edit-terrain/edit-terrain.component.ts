import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ModalService } from 'src/app/Presentation/services/modal.service';
import { NodeService } from 'src/app/Core/services/node.service';
import { TerriansService } from 'src/app/Core/services/terrians.service';

@Component({
  selector: 'app-edit-terrain',
  templateUrl: './edit-terrain.component.html',
  styleUrls: ['./edit-terrain.component.css']
})
export class EditTerrainComponent {

  submitted: boolean = false

  constructor(private modalService: ModalService,
    public terrSrv: TerriansService) {

  }

  creatingForm = new FormGroup({
    terrainName: new FormControl<string>('', [Validators.required]),
    terrainDescription: new FormControl<string>('')
  })

  get terrainName() {
    return this.creatingForm.controls.terrainName as FormControl
  }

  submit() {

    this.submitted = true
    if (this.creatingForm.valid) {

      this.terrSrv.create({
        name: this.creatingForm.value.terrainName as string,
        description: this.creatingForm.value.terrainDescription as string
      }).subscribe(() => {
        this.modalService.close()
      })
    }

  }

  close() {
    this.modalService.close()
  }

}
