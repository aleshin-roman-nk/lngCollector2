import { Component, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription, tap } from 'rxjs';
import { ITerrain } from 'src/app/Core/Models/terrain';
import { NodeService } from 'src/app/Core/services/node.service';
import { TerriansService } from 'src/app/Core/services/terrians.service';
import { INode } from 'src/app/Core/Models/node';
import { TextInputOnpageComponent } from '../../comps-tools/text-input-onpage/text-input-onpage.component';
import { ConfirmationComponent } from '../../comps-tools/confirmation/confirmation.component';
import { ButtonKind } from '../../Models/buttons-kind-enum';
import { TextInputComponent } from '../../comps-tools/text-input-inline/text-input-inline.component';

@Component({
  selector: 'app-terrain-page',
  templateUrl: './terrain-page.component.html',
  styleUrls: ['./terrain-page.component.css']
})
export class TerrainPageComponent {

  @ViewChild(TextInputOnpageComponent, { static: false }) newNodeDlg!: TextInputOnpageComponent
  @ViewChild(ConfirmationComponent, { static: false }) confirmDeleteDlg!: ConfirmationComponent

  private subscription: Subscription;

  id: number
  terrain: ITerrain
  nodes: INode[] = []

  showEditTerrain: boolean = false

  loading: boolean = false

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
        this.nodes = data
      })
  }

  ngAfterViewInit() {
    this.newNodeDlg.accepted
    .subscribe(resp => {
      if(resp.trim() === "") return
      this.nodeSrv.addNode(resp, this.terrain.id!)
      .subscribe(resp => {
        this.nodes.push(resp)
      })
    })

    this.confirmDeleteDlg.finished.subscribe(resp => {
      if(resp === ButtonKind.yes){
        this.srvTerr.delete(this.id)
        .subscribe(() => { this.router.navigate(['']) })
      }
    })

  }

  startAskingToKill(): void {
    this.confirmDeleteDlg.openDialog()
  }

  startNewNodeDlg() {
    this.newNodeDlg.value = ""
    this.newNodeDlg.openDialog()
  }

  terrainUpdatingSubscription: Subscription | null = null
  saveTerrain() {
    this.loading = true
    if (this.terrainUpdatingSubscription)
      this.terrainUpdatingSubscription.unsubscribe()

      this.terrainUpdatingSubscription = this.srvTerr.update({
      description: this.terrain.description,
      name: this.terrain.name,
      id: this.terrain.id
      //id: 1111
    })
      .subscribe({
        next: () => this.loading = false,
        error: (error) => this.loading = false
      })

    this.showEditTerrain = false
  }

}
