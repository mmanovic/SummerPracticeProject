import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { UrlService } from './url.service';
import { AlertService } from './alert.service';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

export class BaseService<T extends { id: number }> {

    constructor(
        public http: HttpClient,
        public urls: UrlService) { }

    /**GET all models from the server */
    getAll(url: string): Observable<T[]> {
        return this.http.get<T[]>(url).pipe(
            tap(_ => console.log('fetched models')),
            catchError(this.handleError<T[]>('getModels', []))
        );
    }

    /**GET model by id. Return 404 when not found */
    getById(url: string, id: number): Observable<T> {
        const _url = `${url}/${id}`;
        return this.http.get<T>(_url).pipe(
            tap(_ => console.log(`model fetched id=${id}`)),
            catchError(this.handleError<T>(`getModel (id=${id})`))
        );
    }

    /**POST: add a new model to the server */
    add(url: string, model: T): Observable<T> {
        return this.http.post<T>(url, model, httpOptions).pipe(
            tap((e: T) => console.log(`added model with id=${e.id}`)),
            catchError(this.handleError<T>('addModel'))
        );
    }

    /**DELETE: delete the model from the server */
    delete(url: string, model: T | number): Observable<T> {
        const id = typeof model === 'number' ? model : model.id;
        const _url = `${url}/${id}`;

        return this.http.delete<T>(_url, httpOptions).pipe(
            tap(_ => console.log(`deleted model id=${id}`)),
            catchError(this.handleError<T>('deleteModel'))
        );
    }

    /** PUT: update the model on the server */
    update(url: string, model: T): Observable<T> {
        return this.http.put<T>(url, model, httpOptions).pipe(
            tap(_ => console.log(`updated model id=${model.id}`)),
            catchError(this.handleError<T>('updateModel'))
        );
    }


    /**
     * Handle Http operation that failed.
     * Let the app continue.
     * @param operation - name of the operation that failed
     * @param result - optional value to return as the observable result
     */
    private handleError<R>(operation = 'operation', result?: R) {
        return (error: any): Observable<R> => {
            // TODO: send the error to remote logging infrastructure
            console.error(error); // log to console instead

            // TODO: better job of transforming error for user consumption
            console.log(`${operation} failed: ${error.message}`);

            // Let the app keep running by returning an empty result.
            return of(result as R);
        };
    }
}
