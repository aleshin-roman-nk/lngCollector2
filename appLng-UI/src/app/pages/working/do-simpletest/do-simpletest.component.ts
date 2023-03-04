import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { DoSimpleTestService } from 'src/app/services/working/do-simpletest.service';

@Component({
  selector: 'app-do-simpletest',
  templateUrl: './do-simpletest.component.html',
  styleUrls: ['./do-simpletest.component.css']
})
export class DoSimpletestComponent {

  constructor(
    public srv: DoSimpleTestService,
    private router: Router,
    private actRout: ActivatedRoute) {

    srv.Reset()
  }

  hit() {
    this.checkingIsOk = this.usersolution.toUpperCase() === this.srv.gethelp().toUpperCase();
    if(this.checkingIsOk)
      this.resultClaim = "GREAT!!!"
    else{
       this.resultClaim = "You need to know this better..."
       this.ishelpshown = true
    }

    this.hashit = true

    this.testIsCompleted = this._checkTestIfCompleted()
  }

  gethelp() {
    this.ishelpshown = true
    this.resultClaim = "Well, go and repeat this. You do not have this grade..."
    this.hashit = true

    this.testIsCompleted = this._checkTestIfCompleted()
  }

  next(){
    this.usersolution = ""
    this.srv.next()
    this.ishelpshown = false
    this.checkingIsOk = false

    this.hashit = false
  }

  exit() {
    this.router.navigate([''])
  }

  private _checkTestIfCompleted(): boolean{
    return this.srv.Current + 1 == this.srv.Total
  }

  usersolution: string = ''

  checkingIsOk: boolean = false;
  resultClaim: string = "none"
  pointsArray = new Array(this.srv.Total)

  testIsCompleted: boolean = false;

  hashit: boolean = false
  ishelpshown = false
}
