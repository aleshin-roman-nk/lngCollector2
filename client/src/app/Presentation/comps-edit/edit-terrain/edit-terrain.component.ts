import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NodeService } from 'src/app/Core/services/node.service';
import { TerriansService } from 'src/app/Core/services/terrians.service';
import { ITerrainTitle } from 'src/app/Core/Models/terrain';
import { AuthService } from 'src/app/Core/services/auth.service';

@Component({
  selector: 'app-edit-terrain',
  templateUrl: './edit-terrain.component.html',
  styleUrls: ['./edit-terrain.component.css']
})
export class EditTerrainComponent {

  submitted: boolean = false

  isShown: boolean = false

  @Output() finished = new EventEmitter<ITerrainTitle>()

  constructor(
    private terrSrv: TerriansService,
    private userSrv: AuthService
    ) {

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

      if(this.userSrv.currentUser == null) return

      this.terrSrv
      .create({
        name: this.creatingForm.value.terrainName as string,
        description: this.creatingForm.value.terrainDescription as string,
        userId: parseInt(this.userSrv.currentUser!.Id)})
        .subscribe({
          next: (resp) => {
            this.finished.emit(resp)
            this.isShown = false
          },
          error: (error) => {
// doing nothing
          }
        })
    }

  }

  close() {
    this.isShown = false
  }

  openCreateDialog(){
    this.isShown = true
  }

}
