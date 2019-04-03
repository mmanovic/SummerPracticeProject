import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { Engine } from '../models/engine';
import { EngineType } from '../models/engine-type';
import { Model } from '../models/model';
import { BaseService } from './base-service';
import { UrlService } from './url.service';

@Injectable({
  providedIn: 'root'
})
export class EngineService extends BaseService<Engine> {

  constructor(public http: HttpClient, public urls: UrlService) { super(http, urls); }

  getModelsOfEngine(id: number): Observable<Model[]> {
    return this.http.get<Model[]>(`${this.urls.modelUrl}?engineId=` + id);
  }

}
