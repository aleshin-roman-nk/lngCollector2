import { Component, Input } from '@angular/core';
import { IFlashCard } from 'src/app/Core/Models/flash-card';

@Component({
  selector: 'app-card-input',
  templateUrl: './card-input.component.html',
  styleUrls: ['./card-input.component.css']
})
export class CardInputComponent {
@Input() card: IFlashCard
}
