import { Component, Input } from '@angular/core';
import { IMission } from 'src/app/models/mission';

@Component({
  selector: 'app-mission',
  templateUrl: './mission.component.html',
  styleUrls: ['./mission.component.css']
})
export class MissionComponent {
  @Input() mission: IMission
}
