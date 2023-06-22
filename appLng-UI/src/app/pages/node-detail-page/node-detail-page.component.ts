import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, delay } from 'rxjs';
import { INodeDetail } from 'src/app/models/nodedetail';
import { IThought } from 'src/app/models/thought';
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

  constructor(
    private activateRoute: ActivatedRoute,
    private router: Router,
    public nodeDetailSrv: NodeDetailService,
    public modalService: ModalService
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

    //this.nodeDetail$ = this.nodeSrv.getNodeDetail(this.nodeId);
  }

  createThought(){
    const th: IThought  = {
      id: 0,
      nodeId: this.nodeId,
      text: 'dog dog dog',
      description: 'четвероногое существо',
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
  }
}
