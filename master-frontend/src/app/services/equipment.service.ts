import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseService } from './base-service';
import { Equipment } from '../models/equipment';
import { Model } from '../models/model';
import { UrlService } from './url.service';

@Injectable({
  providedIn: 'root'
})
export class EquipmentService extends BaseService<Equipment> {

  constructor(public http: HttpClient, public urls: UrlService) { super(http, urls); }

  getModelsOfEquipment(id: number): Observable<Model[]> {
    return this.http.get<Model[]>(`${this.urls.modelUrl}?equipmentId=` + id);
  }
}
