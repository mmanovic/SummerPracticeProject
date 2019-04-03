import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { BaseService } from './base-service';
import { Model } from '../models/model';
import { Manufacturer } from '../models/manufacturer';
import { Inventory } from '../models/inventory';
import { Engine } from '../models/engine';
import { Equipment } from '../models/equipment';
import { UrlService } from './url.service';

@Injectable({
  providedIn: 'root'
})
export class ModelService extends BaseService<Model> {
  constructor(public http: HttpClient, public urls: UrlService) { super(http, urls); }

  getInventoriesOfModel(id: number): Observable<Inventory[]> {
    return this.http.get<Inventory[]>(`${this.urls.inventoryUrl}?modelId=` + id);
  }
}
