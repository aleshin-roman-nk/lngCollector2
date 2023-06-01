import { Component, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { INode } from 'src/app/models/node';

@Component({
  selector: 'app-building',
  templateUrl: './terobject.component.html',
  styleUrls: ['./terobject.component.css']
})
export class TerobjectComponent {

@Input() terobject: INode
}
