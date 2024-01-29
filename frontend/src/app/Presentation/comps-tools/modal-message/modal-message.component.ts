import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-modal-message',
  templateUrl: './modal-message.component.html',
  styleUrls: ['./modal-message.component.css']
})
export class ModalMessageComponent {

  @Input() title: string
  @Input() message: string

  isShown: boolean = false

  openDialog(mes: string) {
    this.message = mes
    this.isShown = true
  }

}
