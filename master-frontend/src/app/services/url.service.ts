import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UrlService {
  public cityUrl = 'http://localhost:51135/api/City';
  public salonUrl = 'http://localhost:51135/api/Salon';
  public manufacturerUrl = 'http://localhost:51135/api/Manufacturer';
  public countryUrl = 'http://localhost:51135/api/Country';
  public inventoryUrl = 'http://localhost:51135/api/Inventory';
  public modelUrl = 'http://localhost:51135/api/Model';
  public equipmentUrl = 'http://localhost:51135/api/Equipment';
  public engineUrl = 'http://localhost:51135/api/Engine';
  public engineTypeUrl = 'http://localhost:51135/api/EngineType';
  public statisticUrl = 'http://localhost:51135/api/Statistic';

  constructor() { }
}
