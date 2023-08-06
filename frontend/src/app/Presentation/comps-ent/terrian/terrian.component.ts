import { Component, Input } from '@angular/core';
import { ITerrain } from 'src/app/Core/Models/terrain';

@Component({
  selector: 'app-terrian',
  templateUrl: './terrian.component.html',
  styleUrls: ['./terrian.component.css']
})
export class TerrianComponent {
  @Input() terrain: ITerrain
}
