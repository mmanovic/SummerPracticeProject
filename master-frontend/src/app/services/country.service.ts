import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { Country } from '../models/country';
import { Salon } from '../models/salon';
import { Manufacturer } from '../models/manufacturer';
import { BaseService } from './base-service';
import { UrlService } from './url.service';

@Injectable({
  providedIn: 'root'
})
export class CountryService extends BaseService<Country> {

  constructor(public http: HttpClient, public urls: UrlService) { super(http, urls); }

  getSalonsOfCountry(id: number): Observable<Salon[]> {
    return this.http.get<Salon[]>(`${this.urls.salonUrl}?countryId=` + id);
  }

  getManufacturersOfCountry(id: number): Observable<Manufacturer[]> {
    return this.http.get<Manufacturer[]>(`${this.urls.manufacturerUrl}?countryId=` + id);
  }

}

