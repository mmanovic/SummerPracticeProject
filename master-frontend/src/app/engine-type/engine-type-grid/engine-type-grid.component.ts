import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

import { EngineType } from '../../models/engine-type';
import { EngineTypeService } from '../../services/engine-type.service';

@Component({
  selector: 'app-engine-type-grid',
  templateUrl: './engine-type-grid.component.html',
  styleUrls: ['./engine-type-grid.component.css']
})
export class EngineTypeGridComponent implements OnInit {
  public engineTypes: EngineType[];

  constructor(
    private location: Location,
    private engineTypeService: EngineTypeService) {
  }

  ngOnInit() {
    this.getCities();
  }

  getCities() {
    this.engineTypeService.getAll(this.engineTypeService.urls.engineTypeUrl)
      .subscribe((data: EngineType[]) => this.engineTypes = data);
  }

  deleteEngineType(engineType: EngineType): void {
    this.engineTypeService.delete(this.engineTypeService.urls.engineTypeUrl, engineType)
      .subscribe(data => { if (data) { this.engineTypes = this.engineTypes.filter(c => c !== engineType); }});
  }

  goBack(): void {
    this.location.back();
  }
}
