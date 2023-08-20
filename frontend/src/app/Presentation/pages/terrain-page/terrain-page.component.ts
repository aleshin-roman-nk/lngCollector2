import { Component, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { ButtonKind } from 'src/app/Presentation/Models/buttons-kind-enum';
import { ITerrain } from 'src/app/Core/Models/terrain';
import { NodeService } from 'src/app/Core/services/node.service';
import { TerriansService } from 'src/app/Core/services/terrians.service';
import { INode } from 'src/app/Core/Models/node';
import { TextInputOnpageComponent } from '../../comps-tools/text-input-onpage/text-input-onpage.component';
import { ConfirmationComponent } from '../../comps-tools/confirmation/confirmation.component';

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
        this.terrain = result.Content
      })

    //this.nodeSrv.dataNodes$.subscribe()
    //this.nodeSrv.loadNodesOf(this.id)

    this.nodeSrv.getNodesByTerrainId(this.id)
      .subscribe(data => {
        if (data.Success) {
          this.nodes = data.Content
        }
      })
  }

  ngAfterViewInit() {
    this.newNodeDlg.accepted.subscribe(resp => {
      if(resp.trim() === "") return
      this.nodeSrv.addNode(resp, this.terrain.id!)
      .subscribe(resp => {
        this.nodes.push(resp.Content)
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
}
