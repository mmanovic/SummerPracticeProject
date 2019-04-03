import { Component, OnInit, ViewChild } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute, UrlSegment, Router } from '@angular/router';

import { EngineType } from '../../models/engine-type';
import { Engine } from '../../models/engine';
import { EngineTypeService } from '../../services/engine-type.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-engine-type-detail',
  templateUrl: './engine-type-detail.component.html',
  styleUrls: ['./engine-type-detail.component.css']
})
export class EngineTypeDetailComponent implements OnInit {

  @ViewChild('f') form: any;
  public engineType: EngineType;
  public engines: Engine[];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private engineTypeService: EngineTypeService,
    private location: Location
  ) { }

  ngOnInit(): void {
    this.getEngineType();
  }

  getEngineType(): void {
    const segments: UrlSegment[] = this.route.snapshot.url;
    const lastSegment = segments[segments.length - 1];
    if (lastSegment.path.toLowerCase() === 'new') {
      this.engineType = new EngineType();
      return;
    }

    const id = +this.route.snapshot.paramMap.get('id');
    this.engineTypeService.getById(this.engineTypeService.urls.engineTypeUrl, id)
      .subscribe((engineType: EngineType) => this.engineType = engineType);
  }

  private updateEngineType(): void {
    const url = `${this.engineTypeService.urls.engineTypeUrl}/${this.engineType.id}`;
    this.engineTypeService.update(url, this.engineType)
      .subscribe(_ => console.log('Updated!'));
  }

  private addEngineType(): void {
    this.engineTypeService.add(this.engineTypeService.urls.engineTypeUrl, this.engineType)
      .subscribe((engineType) => {
        console.log('Added!');
        this.router.navigate(['/engineTypes']);
      });
  }

  getEngineTypeEngines(): () => Observable<EngineType[]> {
    return () => this.engineTypeService.getEnginesOfEngineType(this.engineType.id);
  }

  onSubmit(): void {
    if (this.form.invalid) {
      return;
    }

    if (this.engineType.id) {
      this.updateEngineType();
    } else {
      this.addEngineType();
    }
  }

  goBack(): void {
    this.location.back();
  }
}
