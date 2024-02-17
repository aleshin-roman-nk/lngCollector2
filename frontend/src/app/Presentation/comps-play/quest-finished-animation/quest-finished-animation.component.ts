import { Component, ElementRef, ViewChild } from '@angular/core';

@Component({
  selector: 'app-quest-finished-animation',
  templateUrl: './quest-finished-animation.component.html',
  styleUrls: ['./quest-finished-animation.component.css']
})
export class QuestFinishedAnimationComponent {
  @ViewChild('lottieContainer') lottieContainer: ElementRef; 
 
  isShown: boolean = false

  ngAfterViewInit() {

  }


}
