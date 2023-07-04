import { Component } from '@angular/core';

@Component({
  selector: 'app-playquest',
  templateUrl: './playquest.component.html',
  styleUrls: ['./playquest.component.css']
})
export class PlayquestComponent {
  constructor() {
  }

  hit() {
    // this.checkingIsOk = this.usersolution.toUpperCase() === this.srv.gethelp().toUpperCase();
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
    // this.usersolution = ""
    // this.srv.next()
    // this.ishelpshown = false
    // this.checkingIsOk = false

    // this.hashit = false
  }

  exit() {
    //this.router.navigate([''])
  }

  private _checkTestIfCompleted(): boolean{
    // return this.srv.Current + 1 == this.srv.Total
    return true
  }

  usersolution: string = ''

  checkingIsOk: boolean = false;
  resultClaim: string = "none"
  // pointsArray = new Array(this.srv.Total)

  testIsCompleted: boolean = false;

  hashit: boolean = false
  ishelpshown = false
}
