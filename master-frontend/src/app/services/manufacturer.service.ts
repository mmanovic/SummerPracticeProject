import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { BaseService } from './base-service';
import { Manufacturer } from '../models/manufacturer';
import { Model } from '../models/model';
import { Country } from '../models/country';
import { City } from '../models/city';
import { UrlService } from './url.service';

@Injectable({
  providedIn: 'root'
})
export class ManufacturerService extends BaseService<Manufacturer> {
  constructor(public http: HttpClient, public urls: UrlService) { super(http, urls); }

  getModelsOfManufacturer(id: number): Observable<Model[]> {
    return this.http.get<Model[]>(`${this.urls.modelUrl}?manufacturerId=` + id);
  }
}
