import { Component, OnInit } from '@angular/core';
import { ManufacturerService } from '../../services/manufacturer.service';
import { Manufacturer } from '../../models/manufacturer';
import { Location } from '@angular/common';

@Component({
  selector: 'app-manufacturer-grid',
  templateUrl: './manufacturer-grid.component.html',
  styleUrls: ['./manufacturer-grid.component.css']
})
export class ManufacturerGridComponent implements OnInit {

  public manufacturers: Manufacturer[];

  constructor(
    private location: Location,
    private manufacturerService: ManufacturerService) { }

  ngOnInit() {
    this.getSalons();
  }

  getSalons(): void {
    this.manufacturerService.getAll(this.manufacturerService.urls.manufacturerUrl)
      .subscribe(data => this.manufacturers = data);
  }

  deleteManufacturer(manufacturer: Manufacturer): void {
    this.manufacturerService.delete(this.manufacturerService.urls.manufacturerUrl, manufacturer)
      .subscribe(data => { if (data) {this.manufacturers = this.manufacturers.filter(e => e !== manufacturer); }});
  }

  goBack(): void {
    this.location.back();
  }

}
