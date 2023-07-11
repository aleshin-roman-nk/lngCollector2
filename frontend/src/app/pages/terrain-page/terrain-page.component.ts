import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { ButtonKind } from 'src/app/models/buttons-kind-enum';
import { ITerrain } from 'src/app/models/terrain';
import { NodeService } from 'src/app/services/node.service';
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
  askingToKill: boolean = false

  constructor(
    activateRoute: ActivatedRoute,
    private router: Router,
    public srvTerr: TerriansService,
    public nodeSrv: NodeService) {

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

    //this.nodeSrv.dataNodes$.subscribe()
    this.nodeSrv.loadNodesOf(this.id)
  }

  startAskingToKill(): void {
    this.askingToKill = true
  }

  finishedAskingToKill(event: ButtonKind){
    if(event === ButtonKind.yes){
    this.srvTerr.delete(this.id)
      .subscribe(() => { this.router.navigate(['']) })     
    }
    this.askingToKill = false
  }
}
