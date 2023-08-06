import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { ButtonKind } from 'src/app/Presentation/Models/buttons-kind-enum';
import { ITerrain } from 'src/app/Core/Models/terrain';
import { NodeService } from 'src/app/Core/services/node.service';
import { TerriansService } from 'src/app/Core/services/terrians.service';
import { INode } from 'src/app/Core/Models/node';

@Component({
  selector: 'app-terrain-page',
  templateUrl: './terrain-page.component.html',
  styleUrls: ['./terrain-page.component.css']
})
export class TerrainPageComponent {

  private subscription: Subscription;

  id: number
  terrain: ITerrain
  nodes: INode[] = []
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
    //this.nodeSrv.loadNodesOf(this.id)

    this.nodeSrv.getNodesByTerrainId(this.id)
    .subscribe(data => {
      if(data.Success){
        this.nodes = data.Content
      }
    })
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
