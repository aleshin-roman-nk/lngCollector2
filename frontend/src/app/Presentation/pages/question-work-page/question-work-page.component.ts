import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-question-work-page',
  templateUrl: './question-work-page.component.html',
  styleUrls: ['./question-work-page.component.css']
})
export class QuestionWorkPageComponent {

  nodeId: number

  constructor(private activateRoute: ActivatedRoute) {
    this.activateRoute.params.subscribe(params => {
      this.nodeId = params['id']
    })
  }

  goCardDlg(){
    console.log("goCardDlg")
  }
}
