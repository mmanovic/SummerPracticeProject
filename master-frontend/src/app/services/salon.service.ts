import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { Salon } from '../models/salon';
import { BaseService } from './base-service';
import { City } from '../models/city';
import { Country } from '../models/country';
import { Inventory } from '../models/inventory';
import { UrlService } from './url.service';


@Injectable({
  providedIn: 'root'
})
export class SalonService extends BaseService<Salon> {

  constructor(public http: HttpClient, public urls: UrlService) {
    super(http, urls);
  }

  getInventoriesOfSalon(id: number): Observable<Inventory[]> {
    return this.http.get<Inventory[]>(`${this.urls.inventoryUrl}?salonId=` + id);
  }

}
