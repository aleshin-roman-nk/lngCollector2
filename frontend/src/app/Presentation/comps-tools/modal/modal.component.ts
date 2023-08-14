import { Component, Input } from '@angular/core';
import { ModalService } from 'src/app/Presentation/services/modal.service';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css']
})
export class ModalComponent {
  constructor(public modalService: ModalService) {
  }

@Input() title: string

}