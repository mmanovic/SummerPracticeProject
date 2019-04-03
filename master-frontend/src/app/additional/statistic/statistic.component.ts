import { Component, OnInit } from '@angular/core';
import { AverageModelPrice } from '../models/average-model-price';
import { StatisticService } from '../../services/statistic.service';
import { Location } from '@angular/common';
import { ManufacturerEtParts } from '../models/manufacturer-et-parts';

@Component({
  selector: 'app-statistic',
  templateUrl: './statistic.component.html',
  styleUrls: ['./statistic.component.css']
})
export class StatisticComponent implements OnInit {

  models: AverageModelPrice[];
  parts: ManufacturerEtParts;
  showPrices = true;

  constructor(
    private statisticService: StatisticService,
    private location: Location
  ) { }

  ngOnInit() {
    this.loadData();
  }

  private loadData() {
    if (this.showPrices && !this.models) {
      this.getAveragePricePerModels();
    } else if (!this.parts) {
      this.getTypesPerManufacturers();
    }
  }

  getAveragePricePerModels(): void {
    this.statisticService.getAveragePricePerModel()
      .subscribe(models => this.models = models);
  }

  getTypesPerManufacturers(): void {
    this.statisticService.getEngineTypePartsPerManufacturers()
      .subscribe(parts => {this.parts = parts; console.log(parts); });
  }

  showPricesView(value: boolean): void {
    this.showPrices = value;
    this.loadData();
  }

  hasElements(map: any): boolean {
    // cast to Map<number, number> dos not work, but this works
    return Object.keys(map).length > 0;
  }

  goBack() {
    this.location.back();
  }

}
