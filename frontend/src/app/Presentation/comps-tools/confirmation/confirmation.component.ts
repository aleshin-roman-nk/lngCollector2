import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ButtonKind } from 'src/app/Presentation/Models/buttons-kind-enum';

@Component({
  selector: 'app-confirmation',
  templateUrl: './confirmation.component.html',
  styleUrls: ['./confirmation.component.css']
})
export class ConfirmationComponent {
  @Input() message: string
  @Input() title: string
  @Input() strong_confirm_text: string

  strong_confirm_value: string
  isShown: boolean = false

  @Output() finished: EventEmitter<ButtonKind> = new EventEmitter<ButtonKind>()

  yes() {
    if (this.strong_confirm_text === this.strong_confirm_value) {
      this.finished.emit(ButtonKind.yes)
      this.isShown = false
    }
  }

  no() {
    this.finished.emit(ButtonKind.no)
    this.isShown = false
  }

  openDialog() {
    this.isShown = true
  }
}
