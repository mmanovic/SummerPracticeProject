import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router, UrlSegment } from '@angular/router';
import { Location } from '@angular/common';

import { CityService } from '../../services/city.service';
import { City } from '../../models/city';
import { Salon } from '../../models/salon';
import { Manufacturer } from '../../models/manufacturer';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-city-detail',
  templateUrl: './city-detail.component.html',
  styleUrls: ['./city-detail.component.css']
})
export class CityDetailComponent implements OnInit {

  @ViewChild('f') form: any;
  city: City;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private cityService: CityService,
    private location: Location
  ) { }

  ngOnInit(): void {
    this.getCity();
  }

  getCity(): void {
    const segments: UrlSegment[] = this.route.snapshot.url;
    const lastSegment = segments[segments.length - 1];
    if (lastSegment.path.toLowerCase() === 'new') {
      this.city = new City();
      return;
    }

    const id = +this.route.snapshot.paramMap.get('id');
    this.cityService.getById(this.cityService.urls.cityUrl, id)
      .subscribe((city: City) => this.city = city);
  }

  private updateCity(): void {
    const url = `${this.cityService.urls.cityUrl}/${this.city.id}`;
    this.cityService.update(url, this.city)
      .subscribe(_ => console.log('Updated!'));
  }

  private addCity(): void {
    this.cityService.add(this.cityService.urls.cityUrl, this.city)
      .subscribe((city) => {
        console.log('Added!');
        this.router.navigate(['/cities']);
      });
  }

  getCitySalons(): () => Observable<Salon[]> {
    return () => this.cityService.getSalonsOfCity(this.city.id);
  }

  getCityManufacturers(): () => Observable<Manufacturer[]> {
    return () => this.cityService.getManufacturersOfCity(this.city.id);
  }

  onSubmit(): void {
    if (this.form.invalid) {
      return;
    }

    if (this.city.id) {
      this.updateCity();
    } else {
      this.addCity();
    }
  }

  goBack(): void {
    this.location.back();
  }
}
