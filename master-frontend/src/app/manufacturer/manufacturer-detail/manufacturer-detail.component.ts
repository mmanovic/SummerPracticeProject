import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router, UrlSegment } from '@angular/router';
import { Location } from '@angular/common';

import { ManufacturerService } from '../../services/manufacturer.service';
import { Model } from '../../models/model';
import { City } from '../../models/city';
import { Country } from '../../models/country';
import { Manufacturer } from '../../models/manufacturer';
import { CityService } from '../../services/city.service';
import { CountryService } from '../../services/country.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-manufacturer-detail',
  templateUrl: './manufacturer-detail.component.html',
  styleUrls: ['./manufacturer-detail.component.css']
})
export class ManufacturerDetailComponent implements OnInit {

  @ViewChild('f') form: any;
  manufacturer: Manufacturer;
  countries: Country[] = [];
  cities: City[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private manufacturerService: ManufacturerService,
    private cityService: CityService,
    private countryService: CountryService,
    private location: Location
  ) { }

  ngOnInit(): void {
    this.getManufacturer();
    this.getCountries();
    this.getCities();
  }

  getManufacturer(): void {
    const segments: UrlSegment[] = this.route.snapshot.url;
    const lastSegment = segments[segments.length - 1];
    if (lastSegment.path.toLowerCase() === 'new') {
      this.manufacturer = new Manufacturer();
      return;
    }

    const id = +this.route.snapshot.paramMap.get('id');
    this.manufacturerService.getById(this.manufacturerService.urls.manufacturerUrl, id)
      .subscribe((manufacturer: Manufacturer) => this.manufacturer = manufacturer);
  }

  getCities(): void {
    this.cityService.getAll(this.cityService.urls.cityUrl)
      .subscribe(cities => this.cities = cities);
  }

  getCountries(): void {
    this.countryService.getAll(this.countryService.urls.countryUrl)
      .subscribe(countries => this.countries = countries);
  }

  private updateManufacturer(): void {
    const url = `${this.manufacturerService.urls.manufacturerUrl}/${this.manufacturer.id}`;
    this.manufacturerService.update(url, this.manufacturer)
      .subscribe(_ => console.log('Updated!'));
  }

  private addManufacturer(): void {
    this.manufacturerService.add(this.manufacturerService.urls.manufacturerUrl, this.manufacturer)
      .subscribe((manufacturer) => {
        console.log('Added!');
        this.router.navigate(['/manufacturers']);
      });
  }

  getManufacturerModels(): () => Observable<Model[]> {
    return () => this.manufacturerService.getModelsOfManufacturer(this.manufacturer.id);
  }

  onSubmit(): void {
    if (this.form.invalid) {
      return;
    }

    if (this.manufacturer.id) {
      this.updateManufacturer();
    } else {
      this.addManufacturer();
    }
  }

  goBack(): void {
    this.location.back();
  }
}
