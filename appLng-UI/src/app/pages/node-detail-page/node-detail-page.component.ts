import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, delay } from 'rxjs';
import { INodeDetail } from 'src/app/models/nodedetail';
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
}
