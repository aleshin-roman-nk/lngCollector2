import { Component, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { ITerobject } from 'src/app/models/terobject';

@Component({
  selector: 'app-building',
  templateUrl: './terobject.component.html',
  styleUrls: ['./terobject.component.css']
})
export class TerobjectComponent {

@Input() terobject: ITerobject
}
