import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { ITerrain } from 'src/app/models/terrain';
import { BuildingService } from 'src/app/services/building.service';
import { TerriansService } from 'src/app/services/terrians.service';

@Component({
  selector: 'app-terrain-page',
  templateUrl: './terrain-page.component.html',
  styleUrls: ['./terrain-page.component.css']
})
export class TerrainPageComponent {

  private subscription: Subscription;

  id: number
  terrain: ITerrain

  constructor(
    activateRoute: ActivatedRoute,
    private router: Router,
    public srvTerr: TerriansService,
    public srvBuilding: BuildingService) {
    this.subscription = activateRoute.params.subscribe(params => {
      this.id = params['id']
    });

  }

  ngOnInit(): void {
    this.srvTerr
    .getOne(this.id)
    .subscribe((result) => {
      this.terrain = result
    })
  }

  killme(): void{
    this.srvTerr.delete(this.id)
    .subscribe(()=>{this.router.navigate([''])})
  }

}
