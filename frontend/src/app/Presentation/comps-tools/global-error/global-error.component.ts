import { Component } from '@angular/core';
import { ErrorHandlerService } from '../../../Core/services/error-handler.service';

@Component({
  selector: 'app-global-error',
  templateUrl: './global-error.component.html',
  styleUrls: ['./global-error.component.css']
})
export class GlobalErrorComponent {
  constructor(public errorService: ErrorHandlerService) {

  }
}
