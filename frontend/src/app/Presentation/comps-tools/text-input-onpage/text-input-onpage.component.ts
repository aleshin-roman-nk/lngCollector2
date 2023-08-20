import { Component, EventEmitter, Input } from '@angular/core';

@Component({
  selector: 'app-text-input-onpage',
  templateUrl: './text-input-onpage.component.html',
  styleUrls: ['./text-input-onpage.component.css']
})
export class TextInputOnpageComponent {
  @Input() value: string = ""
  accepted: EventEmitter<string> = new EventEmitter<string>()

  isShown: boolean

  onAccept(){
    this.accepted.emit(this.value)
    this.isShown = false
  }

  openDialog(){
    this.isShown = true
  }
}
