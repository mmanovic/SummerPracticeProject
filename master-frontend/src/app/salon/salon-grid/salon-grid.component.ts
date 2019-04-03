import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { Location } from '@angular/common';

import { Salon } from '../../models/salon';
import { SalonService } from '../../services/salon.service';

@Component({
  selector: 'app-salon-grid',
  templateUrl: './salon-grid.component.html',
  styleUrls: ['./salon-grid.component.css']
})
export class SalonGridComponent implements OnInit {

  @Input()
  public salons: Salon[];

  constructor(
    private location: Location,
    private salonService: SalonService) { }

  ngOnInit() {
    this.getSalons();
  }

  getSalons(): void {
    this.salonService.getAll(this.salonService.urls.salonUrl).subscribe(data => this.salons = data);
  }

  deleteSalon(salon: Salon): void {
    this.salonService.delete(this.salonService.urls.salonUrl, salon)
      .subscribe(data => { if (data) {this.salons = this.salons.filter(e => e !== salon); }});
  }

  goBack(): void {
    this.location.back();
  }
}
