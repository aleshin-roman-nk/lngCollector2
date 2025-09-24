import { Component, Input } from '@angular/core';
import { INodeTitle } from 'src/app/Core/Models/node-title';

@Component({
  selector: 'app-node-title',
  templateUrl: './node-title.component.html',
  styleUrls: ['./node-title.component.css']
})
export class NodeComponent {
  @Input() node: INodeTitle

  nodeLevelImage: string

  progressPosition: number = 0;

  nodeIsCompleted: boolean = false

  constructor() {

  }

  ngOnInit() {
    this.nodeLevelImage = ""

    const levelImageUrls: { [key: number]: string } = {
      0: 'assets/nodes/unknown.png',
      1: 'assets/nodes/node-lvl-1-0.svg',
      2: 'assets/nodes/node-lvl-2-0.svg',
      3: 'assets/nodes/node-lvl-3-0.svg',
      4: 'assets/nodes/node-lvl-4-0.svg',
    };

    const levelImagesCompletedUrls: { [key: number]: string } = {
      0: 'assets/nodes/unknown.png',
      1: 'assets/nodes/node-lvl-1-1.svg',
      2: 'assets/nodes/node-lvl-2-1.svg',
      3: 'assets/nodes/node-lvl-3-1.svg',
      4: 'assets/nodes/node-lvl-4-1.svg',
    };

    if (this.node.questCount === this.node.completedQuestCount)
      this.nodeLevelImage = levelImagesCompletedUrls[this.node.level]
    else
      this.nodeLevelImage = levelImageUrls[this.node.level] || 'assets/nodes/unknown.png'

    if (this.node.level > 0){
      this.progressPosition = (this.node.completedQuestCount / this.node.questCount) * 100;
      this.nodeIsCompleted = this.node.completedQuestCount === this.node.questCount
    }

  }

}
