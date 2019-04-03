import { Component, OnInit, OnDestroy } from '@angular/core';
import { Location } from '@angular/common';

import { CityService } from '../../services/city.service';
import { City } from '../../models/city';
import { PermissionActionService } from '../../services/permission-action.service';

@Component({
  selector: 'app-city-grid',
  templateUrl: './city-grid.component.html',
  styleUrls: ['./city-grid.component.css']
})
export class CityGridComponent implements OnInit, OnDestroy {
  public cities: City[];


  constructor(
    private location: Location,
    private cityService: CityService,
    public permissionService: PermissionActionService) {
  }

  ngOnInit() {
    this.getCities();
  }

  ngOnDestroy() {

  }

  getCities(): void {
    this.cityService.getAll(this.cityService.urls.cityUrl)
      .subscribe((data: City[]) => this.cities = data);
  }

  deleteCity(city: City): void {
    this.cityService.delete(this.cityService.urls.cityUrl, city)
      .subscribe(data => { if (data) { this.cities = this.cities.filter(c => c !== city); }});
  }

  goBack(): void {
    this.location.back();
  }
}
