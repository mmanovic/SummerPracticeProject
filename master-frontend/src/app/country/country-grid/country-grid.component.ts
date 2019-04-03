import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

import { Country } from '../../models/country';
import { CountryService } from '../../services/country.service';

@Component({
  selector: 'app-country-grid',
  templateUrl: './country-grid.component.html',
  styleUrls: ['./country-grid.component.css']
})
export class CountryGridComponent implements OnInit {
  public countries: Country[];

  constructor(
    private location: Location,
    private countryService: CountryService) { }

   ngOnInit() {
    this.getCountries();
  }

  getCountries(): void {
    this.countryService.getAll(this.countryService.urls.countryUrl)
      .subscribe((data: Country[]) => this.countries = data);
  }

  deleteCountry(country: Country): void {
    this.countryService.delete(this.countryService.urls.countryUrl, country)
      .subscribe(data => { if (data) { this.countries = this.countries.filter(c => c !== country); }});
  }

  goBack(): void {
    this.location.back();
  }
}
