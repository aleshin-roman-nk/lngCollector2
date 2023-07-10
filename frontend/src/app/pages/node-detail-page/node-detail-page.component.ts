import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, delay } from 'rxjs';
import { INodeDetail } from 'src/app/models/nodedetail';
import { IThought } from 'src/app/models/thought';
import { UserResponse } from 'src/app/models/user-response';
import { ModalService } from 'src/app/services/modal.service';
import { NodeDetailService } from 'src/app/services/node-detail.service';
import { NodeService } from 'src/app/services/node.service';

@Component({
  selector: 'app-node-detail-page',
  templateUrl: './node-detail-page.component.html',
  styleUrls: ['./node-detail-page.component.css']
})
export class NodeDetailPageComponent implements OnInit {

  nodeId: number
  nodeDetail: INodeDetail
  loading: boolean

  startCreateThought: boolean = false

  constructor(
    private activateRoute: ActivatedRoute,
    private router: Router,
    public nodeDetailSrv: NodeDetailService
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
        this.nodeDetail = data
        this.loading = false
      })
  }

  openCreatingThought(){
    this.startCreateThought = true
  }

  closeCreatingThought(){
    this.startCreateThought = false
  }

  Finished(eventdata: UserResponse<{text: string, descr: string}>){
    
    if(!eventdata.hasUserAccepted) {this.closeCreatingThought(); return}

    const th: IThought  = {
      id: 0,
      nodeId: this.nodeId,
      text: eventdata.value.text,
      description: eventdata.value.descr,
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
        this.nodeDetail.thoughts.push(resp)
      })

      this.closeCreatingThought()
  }
}
