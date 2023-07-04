import { Component, Input } from '@angular/core';
import { INode } from 'src/app/models/node';

@Component({
  selector: 'app-node',
  templateUrl: './node.component.html',
  styleUrls: ['./node.component.css']
})
export class NodeComponent {
  @Input() node: INode
}
