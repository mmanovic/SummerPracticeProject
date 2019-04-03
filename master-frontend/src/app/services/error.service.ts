import { Injectable } from '@angular/core';
import { Subject, Observable } from '../../../node_modules/rxjs';
import { Router, NavigationStart } from '../../../node_modules/@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ErrorService {

  private message: any;
  private keepAfterNavChanges: boolean;

  constructor(private router: Router) {
    router.events.subscribe(event => {
      if (event instanceof NavigationStart) {
        if (this.keepAfterNavChanges) {
          this.keepAfterNavChanges = false;
        } else {
          this.message = null;
        }
      }
    });
   }

  public activateMessage(message: string): void {
    this.activateMessageCode(message, undefined);
  }

  public activateMessageCode(message: string, statusCode: number): void {
    this.message = {message: message, statusCode: statusCode};
    this.keepAfterNavChanges = true;
    this.router.navigate(['/errorPage']);
  }

  public getMessage(): any {
    return this.message;
  }

}
