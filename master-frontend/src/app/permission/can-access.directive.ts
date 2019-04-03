import { Directive, Input, OnInit, OnDestroy, TemplateRef, ViewContainerRef } from '@angular/core';
import { Subscription } from '../../../node_modules/rxjs';
import { PermissionActionService } from '../services/permission-action.service';

@Directive({
  // tslint:disable-next-line:directive-selector
  selector: '[canAccess]'
})
export class CanAccessDirective implements OnInit, OnDestroy {

  // tslint:disable-next-line:no-input-rename
  @Input('canAccess') canAccessRoles: string[];
  private permission$: Subscription;

  constructor(
    private templateRef: TemplateRef<any>,
    private viewContainer: ViewContainerRef,
    private permissionService: PermissionActionService
  ) {
}
  ngOnInit(): void {
    this.applyPermission();
  }

  private applyPermission(): void {
    this.permission$ = this.permissionService
      .hasPermission(this.canAccessRoles)
      .subscribe(allowed => {
        if (allowed) {
          this.viewContainer.createEmbeddedView(this.templateRef);
        } else {
          this.viewContainer.clear();
        }
      });
  }

  ngOnDestroy(): void {
    this.permission$.unsubscribe();
  }

}
