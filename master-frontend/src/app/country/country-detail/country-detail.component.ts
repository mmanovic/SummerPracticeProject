import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router, UrlSegment } from '@angular/router';
import { Location } from '@angular/common';

import { Country } from '../../models/country';
import { CountryService } from '../../services/country.service';
import { Salon } from '../../models/salon';
import { Manufacturer } from '../../models/manufacturer';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-country-detail',
  templateUrl: './country-detail.component.html',
  styleUrls: ['./country-detail.component.css'],
})
export class CountryDetailComponent implements OnInit {

  @ViewChild('f') form: any;
  country: Country;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private countryService: CountryService,
    private location: Location
  ) { }

  ngOnInit(): void {
    this.getCountry();
  }

  getCountry(): void {
    const segments: UrlSegment[] = this.route.snapshot.url;
    const lastSegment = segments[segments.length - 1];
    if (lastSegment.path.toLowerCase() === 'new') {
      this.country = new Country();
      return;
    }

    const id = +this.route.snapshot.paramMap.get('id');
    this.countryService.getById(this.countryService.urls.countryUrl, id)
      .subscribe((country: Country) => this.country = country);
  }

  private updateCountry(): void {
    const url = `${this.countryService.urls.countryUrl}/${this.country.id}`;
    this.countryService.update(url, this.country)
      .subscribe(_ => console.log('Updated!'));
  }

  private addCountry(): void {
    this.countryService.add(this.countryService.urls.countryUrl, this.country)
      .subscribe((country) => {
        console.log('Added!');
        this.router.navigate(['/countries']);
      });
  }

  getCountrySalons(): () => Observable<Salon[]> {
    return () => this.countryService.getSalonsOfCountry(this.country.id);
  }

  getCountryManufacturers(): () => Observable<Manufacturer[]> {
    return () => this.countryService.getManufacturersOfCountry(this.country.id);
  }

  onSubmit(): void {
    if (this.form.invalid) {
      return;
    }

    if (this.country.id) {
      this.updateCountry();
    } else {
      this.addCountry();
    }
  }

  goBack(): void {
    this.location.back();
  }
}
