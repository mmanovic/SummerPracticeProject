import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, UrlSegment, Router } from '@angular/router';
import { Location } from '@angular/common';
import { Salon } from '../../models/salon';
import { SalonService } from '../../services/salon.service';
import { Country } from '../../models/country';
import { City } from '../../models/city';
import { Inventory } from '../../models/inventory';
import { Observable } from 'rxjs';
import { CityService } from '../../services/city.service';
import { CountryService } from '../../services/country.service';

@Component({
  selector: 'app-salon-detail',
  templateUrl: './salon-detail.component.html',
  styleUrls: ['./salon-detail.component.css']
})
export class SalonDetailComponent implements OnInit {

  @ViewChild('f') form: any;
  salon: Salon;
  countries: Country[] = [];
  cities: City[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private salonService: SalonService,
    private cityService: CityService,
    private countryService: CountryService,
    private location: Location
  ) { }

  ngOnInit(): void {
    this.getSalon();
    this.getCities();
    this.getCountries();
  }

  getSalon(): void {
    const segments: UrlSegment[] = this.route.snapshot.url;
    const lastSegment = segments[segments.length - 1];
    if (lastSegment.path.toLowerCase() === 'new') {
      this.salon = new Salon();
      return;
    }

    const id = +this.route.snapshot.paramMap.get('id');
    this.salonService.getById(this.salonService.urls.salonUrl, id)
      .subscribe((salon: Salon) => this.salon = salon);
  }

  getCities(): void {
    this.cityService.getAll(this.cityService.urls.cityUrl)
      .subscribe(cities => this.cities = cities);
  }

  getCountries(): void {
    this.countryService.getAll(this.countryService.urls.countryUrl)
      .subscribe(countries => this.countries = countries);
  }

  private updateSalon(): void {
    const url = `${this.salonService.urls.salonUrl}/${this.salon.id}`;
    this.salonService.update(url, this.salon)
      .subscribe(_ => console.log('Updated!'));
  }

  private addSalon(): void {
    this.salonService.add(this.salonService.urls.salonUrl, this.salon)
      .subscribe((salon) => {
        console.log('Added!');
        this.router.navigate(['/salons']);
      });
  }

  getSalonInventories(): () => Observable<Inventory[]> {
    return () => this.salonService.getInventoriesOfSalon(this.salon.id);
  }

  onSubmit(): void {
    if (this.form.invalid) {
      return;
    }

    if (this.salon.id) {
      this.updateSalon();
    } else {
      this.addSalon();
    }
  }

  goBack(): void {
    this.location.back();
  }

}
