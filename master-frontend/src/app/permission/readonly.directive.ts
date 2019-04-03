import { Directive, ElementRef, OnInit, OnDestroy, Input } from '@angular/core';
import { PermissionActionService } from '../services/permission-action.service';
import { Subscription } from '../../../node_modules/rxjs';

@Directive({
  selector: '[appModify]'
})
export class ReadonlyDirective implements OnInit, OnDestroy {

  // tslint:disable-next-line:no-input-rename
  @Input('appModify') roles: string[];
  private permission$: Subscription;

  constructor(
    private element: ElementRef,
    private permissionService: PermissionActionService) {
  }

  ngOnInit() {
    this.applyDisable();
  }

  ngOnDestroy() {
    this.permission$.unsubscribe();
  }

  applyDisable() {
    this.permission$ = this.permissionService
      .hasPermission(this.roles)
      .subscribe(allowed => {
          this.element.nativeElement.disabled = !allowed;
      });
  }

}
