import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from '../../../../node_modules/rxjs';
import { ErrorService } from '../../services/error.service';

@Component({
  selector: 'app-error-page',
  templateUrl: './error-page.component.html',
  styleUrls: ['./error-page.component.css']
})
export class ErrorPageComponent implements OnInit {

  public message: any;

  constructor(protected errorService: ErrorService) { }

  ngOnInit() {
    this.message = this.errorService.getMessage();
  }
}
