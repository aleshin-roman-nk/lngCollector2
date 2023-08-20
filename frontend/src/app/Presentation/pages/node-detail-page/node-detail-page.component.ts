import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, delay } from 'rxjs';
import { EditThoughtComponent } from 'src/app/Presentation/comps-edit/edit-thought/edit-thought.component';
import { INodeDetail } from 'src/app/Core/Models/nodedetail';
import { IThought } from 'src/app/Core/Models/thought';
import { UserResponse } from 'src/app/Presentation/Models/user-response';
import { ModalService } from 'src/app/Presentation/services/modal.service';
import { NodeDetailService } from 'src/app/Core/services/node-detail.service';
import { NodeService } from 'src/app/Core/services/node.service';
import { ConfirmationComponent } from '../../comps-tools/confirmation/confirmation.component';
import { ButtonKind } from '../../Models/buttons-kind-enum';

@Component({
  selector: 'app-node-detail-page',
  templateUrl: './node-detail-page.component.html',
  styleUrls: ['./node-detail-page.component.css']
})
export class NodeDetailPageComponent implements OnInit {

  @ViewChild(EditThoughtComponent, {static: false}) editThoughtDlg!: EditThoughtComponent
  @ViewChild("confirmDeleteNodeDlg", {static: false}) confirmDeleteNodeDlg!: ConfirmationComponent

  nodeId: number
  nodeDetail: INodeDetail
  loading: boolean

  constructor(
    private activateRoute: ActivatedRoute,
    private router: Router,
    public nodeDetailSrv: NodeDetailService,
    private nodeService: NodeService
  ) {}

  ngOnInit() {
    this.loading = true

    this.activateRoute.params.subscribe(params => {
      this.nodeId = params['id'];
    });

    this.nodeDetailSrv.getNodeDetail(this.nodeId)
    .pipe(
      //delay(2000)
    )
      .subscribe(data => {

        if(data.Success){

          this.nodeDetail = data.Content
          this.loading = false
        }
      })
  }

  ngAfterViewInit(){
    this.editThoughtDlg.finished.subscribe(data => {
      if(data.value) this.createThought(data.value)
    })

    this.confirmDeleteNodeDlg.finished.subscribe(resp => {
      if(resp === ButtonKind.yes){
        this.nodeService.deleteNode(this.nodeId)
        .subscribe(resp => {
          this.router.navigate(['terrain', this.nodeDetail.Node.terrainId, 'nodes'])
        })
      }
    })
  }

  openDeletingNode(){
    this.confirmDeleteNodeDlg.openDialog()
  }

  opentCreatingThought(){
    this.editThoughtDlg.openDialog()
  }

  createThought(o: {text: string, descr: string}){

    const th: IThought  = {
      id: 0,
      nodeId: this.nodeId,
      text: o.text,
      description: o.descr,
      createdDate: new Date(),
      expressions: []
    }

    /**
     * получается здесь после отработки createThought один раз по завершении выполняется
     * .subscribe(...) и все забыли
     *
     * https://rxjs.dev/guide/observable
     * To invoke the Observable and see these values, we need to subscribe to it:
     */
    this.nodeDetailSrv.createThought(this.nodeId, th)
      .subscribe(resp => {

        if(resp.Success){
          this.nodeDetail.Thoughts.push(resp.Content)
        }
      })

  }
}
