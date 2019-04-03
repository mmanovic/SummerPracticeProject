import { Component, OnInit, Input } from '@angular/core';
import { Location } from '@angular/common';

import { ModelService } from '../../services/model.service';
import { Model } from '../../models/model';

@Component({
  selector: 'app-model-grid',
  templateUrl: './model-grid.component.html',
  styleUrls: ['./model-grid.component.css']
})
export class ModelGridComponent implements OnInit {

  models: Model[];

  constructor(
    private location: Location,
    public modelService: ModelService) { }


  ngOnInit() {
    this.getModels();
  }

  getModels(): void {
    this.modelService.getAll(this.modelService.urls.modelUrl)
      .subscribe(data => this.models = data);
  }

  deleteModel(engine: Model): void {
    this.modelService.delete(this.modelService.urls.modelUrl, engine)
      .subscribe(data => { if (data) {this.models = this.models.filter(e => e !== engine); }});
  }

  goBack(): void {
    this.location.back();
  }
}
