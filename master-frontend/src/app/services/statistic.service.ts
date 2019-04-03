import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UrlService } from './url.service';
import { AverageModelPrice } from '../additional/models/average-model-price';
import { Observable, of } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { errorHandler } from '@angular/platform-browser/src/browser';

@Injectable({
  providedIn: 'root'
})
export class StatisticService {

  constructor(
    private http: HttpClient,
    private urlService: UrlService
  ) { }

  public getAveragePricePerModel(): Observable<AverageModelPrice[]> {
    const url = `${this.urlService.statisticUrl}/AveragePricePerModels`;
    return this.http.get<AverageModelPrice[]>(url).pipe(
      tap(_ => console.log('Data fetched!')),
      catchError((error: any): Observable<AverageModelPrice[]> => {
        // TODO: send the error to remote logging infrastructure
        console.error(error); // log to console instead

        // Let the app keep running by returning an empty result.
        return of([] as AverageModelPrice[]);
    }));
  }

  public getEngineTypePartsPerManufacturers(): Observable<any> {
    const url = `${this.urlService.statisticUrl}/EngineTypePartsPerManufacturers`;
    return this.http.get<any>(url).pipe(
      tap(_ => console.log('Data fetched!')),
      catchError((error: any): Observable<any> => {
        // TODO: send the error to remote logging infrastructure
        console.error(error); // log to console instead

        // Let the app keep running by returning an empty result.
        return of([]);
    }));
  }
}
