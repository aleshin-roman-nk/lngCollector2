import { Injectable, OnDestroy } from '@angular/core';
import { IMTestPoint } from '../../models/tmpdev/testpoint';

@Injectable({
  providedIn: 'root'
})
export class DoSimpleTestService {

  constructor() { }

  getQuest(): string {
    return this.points[this.current].quest
  }

  gethelp(): string {
    return this.points[this.current].solution
  }

  next() {
    if (this.current == this.points.length - 1) {
      this.current = 0
    }
    else
      this.current++
  }

  current: number = 0

public Reset(): void{
  this.current = 0
}

  public get Current() {
    return this.current
  }

  public get Total() {
    return this.points.length
  }

  points: IMTestPoint[] = [
    {
      id: 1,
      quest: "Я ее знаю",
      solution: "Ben onu biliyorum"
    },
    {
      id: 2,
      quest: "Он пришел вчера",
      solution: "O dün geldi"
    },
    {
      id: 3,
      quest: "Вчера был дождь",
      solution: "Dün yağmur vardı"
    },
    {
      id: 4,
      quest: "Кот хочет молоко",
      solution: "Kedi süt istiyor"
    }
  ]

}
