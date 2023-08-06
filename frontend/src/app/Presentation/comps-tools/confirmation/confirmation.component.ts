import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ButtonKind } from 'src/app/Presentation/Models/buttons-kind-enum';

@Component({
  selector: 'app-confirmation',
  templateUrl: './confirmation.component.html',
  styleUrls: ['./confirmation.component.css']
})
export class ConfirmationComponent {
  @Input() message: string

  @Output() finished: EventEmitter<ButtonKind> = new EventEmitter<ButtonKind>()

  yes(){
    this.finished.emit(ButtonKind.yes)
  }

  no(){
    this.finished.emit(ButtonKind.no)
  }
}
