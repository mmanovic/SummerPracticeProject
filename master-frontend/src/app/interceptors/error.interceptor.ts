import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse } from '@angular/common/http';

import { AlertService } from '../services/alert.service';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { ErrorService } from '../services/error.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(
        private alertService: AlertService,
        private errorService: ErrorService
    ) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(
            tap(resp => {
                if (resp instanceof HttpResponse) {
                    if (resp.status === 201) {
                        const name = resp.body.name ? resp.body.name : `with #ID${resp.body.id}`;
                        this.alertService.success(`Entity ${name} created`, true);
                    } else if (resp.status === 204) {
                        this.alertService.success('Entity updated!');
                    }
                }
            }),
            catchError(err => {
                if (err.status === 400) {
                    const name = err.error.item.name ? `name ${err.error.item.name}` : `#ID ${err.error.item.id}`;
                    this.alertService.error(`${err.error.message} Item with ${name}`);
                } else {
                    if (err.status === 401) {
                        const str = err.error ? err.error.message : 'unauthorized';
                        this.errorService.activateMessageCode(`Can't access ${str}`, 401);
                    } else if (err.status === 403) {
                        const str = err.error ? err.error.message : 'access';
                        this.errorService.activateMessageCode(`Forbidden ${str}`, 403);
                    } else if (err.status === 500) {
                        this.errorService.activateMessageCode(`Internal server error. Sorry, bad luck :(((\n${err.message}`, 500);
                    } else {
                        this.errorService.activateMessageCode(`Error`, err.status);
                    }
                }
                return throwError(err);
            })
        );
    }
}
