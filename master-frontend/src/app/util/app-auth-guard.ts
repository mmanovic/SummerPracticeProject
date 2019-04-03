import { Injectable } from '@angular/core';
import { Router, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { KeycloakService, KeycloakAuthGuard } from 'keycloak-angular';
import { ErrorService } from '../services/error.service';

@Injectable()
export class AppAuthGuard extends KeycloakAuthGuard {

    constructor(protected router: Router,
        protected keycloakAngular: KeycloakService,
        protected errorService: ErrorService) {
        super(router, keycloakAngular);
    }

    isAccessAllowed(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<boolean> {
        return new Promise(async (resolve, reject) => {
            if (!this.authenticated) {
                this.keycloakAngular.login();
                return;
            }

            const requiredRoles = route.data.roles;
            if (!requiredRoles || requiredRoles.length === 0) {
                return resolve(true);
            } else {
                if (!this.roles || this.roles.length === 0) {
                    this.errorService.activateMessageCode('Forbidden access', 403);
                    resolve(false);
                }

                let granted = false;
                for (const requiredRole of requiredRoles) {
                    if (this.roles.indexOf(requiredRole) > -1) {
                        granted = true;
                        break;
                    }
                }
                if (granted === false) {
                    this.errorService.activateMessageCode('Forbidden access', 403);
                }
                resolve(granted);
            }
        });
    }
}

