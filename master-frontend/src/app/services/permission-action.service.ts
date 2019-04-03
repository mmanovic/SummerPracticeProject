import { Injectable } from '@angular/core';
import { KeycloakService } from 'keycloak-angular';
import { Observable, defer } from '../../../node_modules/rxjs';

@Injectable({
  providedIn: 'root'
})
export class PermissionActionService {

  constructor(protected keycloakService: KeycloakService) { }

  public hasPermission(roles: string[]): Observable<boolean> {
      return defer(() => this.hasPermissionPromise(roles));
    }

  public hasPermissionPromise(roles: string[]): Promise<boolean> {
    return new Promise<boolean>(async (resolve, reject) => {
      try {
        const authenticated = await this.keycloakService.isLoggedIn();
        if (!authenticated) {
          resolve(false);
        }

        if (!roles || roles.length === 0) {
          resolve(true);
        } else {
          const userRoles = await this.keycloakService.getUserRoles(true);
          if (!userRoles || userRoles.length === 0) {
            resolve(false);
          }

          let granted = false;
          for (const requiredRole of roles) {
            if (userRoles.indexOf(requiredRole) > -1) {
              granted = true;
              break;
            }
          }

          resolve(granted);
        }
      } catch (error) {
        console.log(`error while trying to check user permision for roles`);
        reject(false);
      }
    });
  }
}
