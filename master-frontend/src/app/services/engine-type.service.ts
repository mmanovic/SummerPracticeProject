import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseService } from './base-service';
import { EngineType } from '../models/engine-type';
import { Engine } from '../models/engine';
import { UrlService } from './url.service';

@Injectable({
  providedIn: 'root'
})
export class EngineTypeService extends BaseService<EngineType> {
  constructor(public http: HttpClient, public urls: UrlService) { super(http, urls); }

  getEnginesOfEngineType(id: number): Observable<Engine[]> {
    return this.http.get<Engine[]>(`${this.urls.engineUrl}?engineTypeId=` + id);
  }
}
