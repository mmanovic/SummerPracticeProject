import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { BaseService } from './base-service';
import { Inventory } from '../models/inventory';
import { Salon } from '../models/salon';
import { Model } from '../models/model';
import { UrlService } from './url.service';

@Injectable({
  providedIn: 'root'
})
export class InventoryService extends BaseService<Inventory> {
  constructor(public http: HttpClient, public urls: UrlService) {
    super(http, urls);
  }
}
