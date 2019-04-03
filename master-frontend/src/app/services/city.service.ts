import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseService } from './base-service';
import { City } from '../models/city';
import { Salon } from '../models/salon';
import { Manufacturer } from '../models/manufacturer';
import { UrlService } from './url.service';
import { AlertService } from './alert.service';

@Injectable({
  providedIn: 'root'
})
export class CityService extends BaseService<City> {
  constructor(
    public http: HttpClient,
    public urls: UrlService,
    public alertService: AlertService) {
     super(http, urls);
    }

  getSalonsOfCity(id: number): Observable<Salon[]> {
    return this.http.get<Salon[]>(`${this.urls.salonUrl}?cityId=` + id);
  }

  getManufacturersOfCity(id: number): Observable<Manufacturer[]> {
    return this.http.get<Manufacturer[]>(`${this.urls.manufacturerUrl}?cityId=` + id);
  }



}
