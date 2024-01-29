import { Component, Input } from '@angular/core';
import { IFlashCardTitle } from 'src/app/Core/Models/flash-card-title';

@Component({
  selector: 'app-flash-card-title',
  templateUrl: './flash-card-title.component.html',
  styleUrls: ['./flash-card-title.component.css']
})
export class FlashCardTitleComponent {
  @Input() flashCardTitle: IFlashCardTitle

constructor(){

}

ngOnInit(){
}

}
