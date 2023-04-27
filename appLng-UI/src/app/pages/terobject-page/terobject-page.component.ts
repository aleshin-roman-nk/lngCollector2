import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { BuildingService } from 'src/app/services/building.service';
import { MissionService } from 'src/app/services/mission.service';

@Component({
  selector: 'app-terobject-page',
  templateUrl: './terobject-page.component.html',
  styleUrls: ['./terobject-page.component.css']
})
export class BuildingPageComponent {
  private subscription: Subscription;
  bldId: number

  constructor(
    private activateRoute: ActivatedRoute,
    public srvBld: BuildingService,
    public srvMission: MissionService
  ) {
    this.subscription = activateRoute.params.subscribe(params => {
      this.bldId = params['id']
    });
  }
}
