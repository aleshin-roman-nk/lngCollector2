import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, delay } from 'rxjs';
import { CreateThoughtComponent } from 'src/app/Presentation/comps-edit/create-thought/create-thought.component';
import { INodeDetail } from 'src/app/Core/Models/nodedetail';
import { IThought } from 'src/app/Core/Models/thought';
import { UserResponse } from 'src/app/Presentation/Models/user-response';
import { ModalService } from 'src/app/Presentation/services/modal.service';
import { NodeDetailService } from 'src/app/Core/services/node-detail.service';
import { NodeService } from 'src/app/Core/services/node.service';

@Component({
  selector: 'app-node-detail-page',
  templateUrl: './node-detail-page.component.html',
  styleUrls: ['./node-detail-page.component.css']
})
export class NodeDetailPageComponent implements OnInit {

  @ViewChild(CreateThoughtComponent, {static: false}) createThoughtDlg!: CreateThoughtComponent

  nodeId: number
  nodeDetail: INodeDetail
  loading: boolean

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
        if(data.Success){
          this.nodeDetail = data.Content
          this.loading = false          
        }
      })
  }

  ngAfterViewInit(){
    this.createThoughtDlg.finished.subscribe(data => {
      if(data.value) this.createThought(data.value)
    })
  }

  opentCreatingThought(){
    this.createThoughtDlg.openDialog()
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
          this.nodeDetail.thoughts.push(resp.Content)
        }
      })

  }
}
