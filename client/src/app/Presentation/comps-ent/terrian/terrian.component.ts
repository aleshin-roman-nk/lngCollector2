import { Component, Input } from '@angular/core';
import { ITerrainTitle } from 'src/app/Core/Models/terrain';

@Component({
  selector: 'app-terrian',
  templateUrl: './terrian.component.html',
  styleUrls: ['./terrian.component.css']
})
export class TerrianComponent {
  @Input() terrain: ITerrainTitle
}
