import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-text-input-inline',
  templateUrl: './text-input-inline.component.html',
  styleUrls: ['./text-input-inline.component.css']
})
export class TextInputComponent {
  @Input() value: string
  accepted: EventEmitter<string> = new EventEmitter<string>()

  isShown: boolean

  onAccept(){
    this.accepted.emit(this.value)
    this.isShown = false
  }

  ngAfterViewInit(){

  }

/*   ngOnDestroy(): void{
    console.log("onDestroy")
  } */
}
