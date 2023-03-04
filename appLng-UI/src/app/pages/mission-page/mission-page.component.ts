import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-mission-page',
  templateUrl: './mission-page.component.html',
  styleUrls: ['./mission-page.component.css']
})
export class MissionPageComponent {


  private subscription: Subscription;
  missId: number

  constructor(
    private activateRoute: ActivatedRoute
  ) {
    this.subscription = activateRoute.params.subscribe(params => {
      this.missId = params['id']
    });
  }
}
