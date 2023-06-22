import { Component, Input } from '@angular/core';
import { IThought } from 'src/app/models/thought';

@Component({
  selector: 'app-thought',
  templateUrl: './thought.component.html',
  styleUrls: ['./thought.component.css']
})
export class ThoughtComponent {
  @Input() thought: IThought
}