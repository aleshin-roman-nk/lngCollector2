import { Component, EventEmitter } from '@angular/core';
import { elementAt, first } from 'rxjs';
import { IResearchText } from 'src/app/Core/Models/research-text';
import { ResearchTextService } from 'src/app/Core/services/research-text.service';

@Component({
  selector: 'app-edit-research-text',
  templateUrl: './edit-research-text.component.html',
  styleUrls: ['./edit-research-text.component.css']
})
export class EditResearchTextComponent {

  isShown: boolean = false

  onCreated: EventEmitter<IResearchText> = new EventEmitter<IResearchText>()
  onUpdated: EventEmitter<IResearchText> = new EventEmitter<IResearchText>()
  onDeleted: EventEmitter<number> = new EventEmitter<number>()

  researchText: IResearchText

  constructor(
    private researchTextService: ResearchTextService
  ){

  }

  ngAfterViewInit(){

  }

  openDialog(rt: IResearchText) {
    this.researchText = rt
    this.isShown = true
  }

  __accept() {

    if (this.researchText.id === 0) {
      this.researchTextService
        .create({ nodeId: this.researchText.nodeId, text: this.researchText.text })
        .pipe(
          first()
        )
        .subscribe((resp) => {
          this.onCreated.emit(resp)
          this.isShown = false
        })
    }
    else {
      this.researchTextService
        .update({ id: this.researchText.id, text: this.researchText.text })
        .pipe(
          first()
        )
        .subscribe(() => {
          this.onUpdated.emit({
            id: this.researchText.id,
            nodeId: this.researchText.nodeId,
            text: this.researchText.text
          })
          this.isShown = false
        })
    }
  }

  __delete() {
    this.researchTextService
      .delete(this.researchText.id)
      .pipe(
        first()
      )
      .subscribe({
        next: () => {
          this.onDeleted.emit(this.researchText.id)
          this.isShown = false
        }/* ,
        error: (e) => {
          console.log(`error : ${e}`)
        } */
      })
  }
}
