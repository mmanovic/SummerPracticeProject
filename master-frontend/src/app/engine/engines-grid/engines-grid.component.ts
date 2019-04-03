import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { EngineService } from '../../services/engine.service';
import { Engine } from '../../models/engine';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-engines-grid',
  templateUrl: './engines-grid.component.html',
  styleUrls: ['./engines-grid.component.css']
})
export class EnginesGridComponent implements OnInit {
  public selectedListView = true;
  public engines: Engine[];

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private engineService: EngineService
  ) { }

  ngOnInit() {
    this.getEngines();
  }

  public setListView(value: boolean): void {
    this.selectedListView = value;
  }

  getEngines(): void {
    this.engineService.getAll(this.engineService.urls.engineUrl)
      .subscribe(engines => this.engines = engines);
  }

  deleteEngine(engine: Engine): void {
    this.engineService.delete(this.engineService.urls.engineUrl, engine)
      .subscribe(data => { if (data) {this.engines = this.engines.filter(e => e !== engine); }});
  }

  goBack(): void {
    this.location.back();
  }
}
