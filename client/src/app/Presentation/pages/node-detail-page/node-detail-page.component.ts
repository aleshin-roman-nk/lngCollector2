import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription, delay, switchMap } from 'rxjs';
import { INodeDetail } from 'src/app/Core/Models/node-detail';
import { NodeService } from 'src/app/Core/services/node.service';
import { ConfirmationComponent } from '../../comps-tools/confirmation/confirmation.component';
import { EditResearchTextComponent } from '../../comps-edit/edit-research-text/edit-research-text.component';
import { ResearchTextService } from 'src/app/Core/services/research-text.service';
import { IResearchText } from 'src/app/Core/Models/research-text';

@Component({
  selector: 'app-node-detail-page',
  templateUrl: './node-detail-page.component.html',
  styleUrls: ['./node-detail-page.component.css']
})
export class NodeDetailPageComponent implements OnInit {

  @ViewChild("editResearchText", { static: false }) editResearchText!: EditResearchTextComponent

  nodeId: number
  nodeDetail: INodeDetail
  loading: boolean

  showEditNode: boolean = false

  constructor(
    private activateRoute: ActivatedRoute,
    private router: Router,
    private nodeService: NodeService
  ) {}

  ngOnInit() {
    this.loading = true

    this.activateRoute.params.subscribe(params => {
      this.nodeId = params['id'];
    });

    this.nodeService.getNodeDetail(this.nodeId)
/*     .pipe(
      //delay(2000)
    ) */
      .subscribe(data => {

        this.nodeDetail = data
        this.loading = false

      })
  }

  ngAfterViewInit() {
    this.editResearchText.onCreated
      .subscribe((res) => {
        this.nodeDetail.ResearchTexts.push(res)
      })

    this.editResearchText.onUpdated
      .subscribe((res) => {
        const i = this.nodeDetail.ResearchTexts.findIndex(x => x.id === res.id)
        if (i !== -1) {
          this.nodeDetail.ResearchTexts[i] = res
        }
      })

    this.editResearchText.onDeleted
      .subscribe(
        (res) => {
          const i = this.nodeDetail.ResearchTexts.findIndex(x => x.id === res)
          if (i !== -1) {
            this.nodeDetail.ResearchTexts.splice(i)
          }
        })
  }

  nodeNameAndDescriptionUpdatingSubscription: Subscription | null = null
  saveNode() {

    this.loading = true
    if (this.nodeNameAndDescriptionUpdatingSubscription)
      this.nodeNameAndDescriptionUpdatingSubscription.unsubscribe()

      this.nodeNameAndDescriptionUpdatingSubscription = this.nodeService
      .updateNodeNameAndDescription({
      description: this.nodeDetail.description,
      name: this.nodeDetail.name,
      id: this.nodeDetail.id
    })
      .subscribe({
        next: () => this.loading = false,
        error: (error) => this.loading = false
      })

    this.showEditNode = false
  }

  openCreateResearchText() {
    this.editResearchText.openDialog({
      id: 0,
      nodeId: this.nodeDetail.id,
      text: ""
    })
  }

  openEditResearchText(rt: IResearchText) {
    this.editResearchText.openDialog({...rt})
  }

  calcQuestPointsTotal(): number {
    let res = this.nodeDetail.FlashCardsTitles.reduce((sum, item) => sum + item.questPrice, 0)
    return res
  }

  calcCompletedQuestPoints(): number{
    let res = this.nodeDetail.FlashCardsTitles.reduce((sum, item) => item.isCompleted ? item.questPrice + sum : sum, 0)

    return res
  }

  calcFlashCardsAimHits(): number{
    let res = this.nodeDetail.FlashCardsTitles.reduce((sum, item) => item.requiredHits! + sum, 0)
    return res
  }

  calcFlashCardsHitsInRow(): number{
    let res = this.nodeDetail.FlashCardsTitles.reduce((sum, item) => item.hitsInRow! + sum, 0)
    return res
  }

  nodeCouldBeCompleted: boolean = false
  calcIfNodeCouldBeCompleted(): boolean{
    return true
  }

  completeNode(){

  }
}
