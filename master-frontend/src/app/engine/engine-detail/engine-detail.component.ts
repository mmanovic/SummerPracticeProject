import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, UrlSegment, Router } from '@angular/router';
import { Location } from '@angular/common';

import { Engine } from '../../models/engine';
import { EngineService } from '../../services/engine.service';
import { EngineType } from '../../models/engine-type';
import { Model } from '../../models/model';
import { Observable } from 'rxjs';
import { EngineTypeService } from '../../services/engine-type.service';

@Component({
  selector: 'app-engine-detail',
  templateUrl: './engine-detail.component.html',
  styleUrls: ['./engine-detail.component.css']
})
export class EngineDetailComponent implements OnInit {

  @ViewChild('f') form: any;
  engine: Engine;
  engineTypes: EngineType[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private engineService: EngineService,
    private engineTypeService: EngineTypeService,
    private location: Location
  ) { }

  ngOnInit() {
    this.getEngine();
    this.getEngineTypes();
  }

  getEngine(): void {
    const segments: UrlSegment[] = this.route.snapshot.url;
    const lastSegment = segments[segments.length - 1];
    if (lastSegment.path.toLowerCase() === 'new') {
      this.engine = new Engine();
      return;
    }

    const id = +this.route.snapshot.paramMap.get('id');
    this.engineService.getById(this.engineService.urls.engineUrl, id)
      .subscribe(e =>  this.engine = e);
  }

  getEngineTypes(): void {
    this.engineTypeService.getAll(this.engineTypeService.urls.engineTypeUrl)
      .subscribe(et => this.engineTypes = et);
  }

  private updateEngine(): void {
    const url = `${this.engineService.urls.engineUrl}/${this.engine.id}`;
    this.engineService.update(url, this.engine)
      .subscribe(_ => console.log('Updated!'));
  }

  private addEngine(): void {
    this.engineService.add(this.engineService.urls.engineUrl, this.engine)
      .subscribe((engine) => {
        console.log('Added!');
        this.router.navigate(['/engines']);
      });
  }

  getModels(): () => Observable<any[]> {
    return () => this.engineService.getModelsOfEngine(this.engine.id);
  }

  onSubmit(): void {
    if (this.form.invalid) {
      return;
    }

    if (this.engine.id) {
      this.updateEngine();
    } else {
      this.addEngine();
    }
  }

  goBack(): void {
    this.location.back();
  }
}
