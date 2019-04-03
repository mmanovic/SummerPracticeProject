import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router, UrlSegment } from '@angular/router';
import { Location } from '@angular/common';

import { Model } from '../../models/model';
import { Manufacturer } from '../../models/manufacturer';
import { Inventory } from '../../models/inventory';
import { ModelService } from '../../services/model.service';
import { Equipment } from '../../models/equipment';
import { Engine } from '../../models/engine';
import { EngineService } from '../../services/engine.service';
import { EquipmentService } from '../../services/equipment.service';
import { ManufacturerService } from '../../services/manufacturer.service';
import { Observable } from 'rxjs';
import { AppAuthGuard } from '../../util/app-auth-guard';
import { PermissionActionService } from '../../services/permission-action.service';


@Component({
  selector: 'app-model-detail',
  templateUrl: './model-detail.component.html',
  styleUrls: ['./model-detail.component.css']
})
export class ModelDetailComponent implements OnInit {

  @ViewChild('f') form: any;
  model: Model;
  manufacturers: Manufacturer[] = [];
  equipments: Equipment[] = [];
  engines: Engine[] = [];
  thisYear: number;
  isAdmin: boolean;
  // modelManufacturer: Manufacturer;
  // modelEquipment: Equipment;
  // modelEngine: Engine;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private modelService: ModelService,
    private manufacturerService: ManufacturerService,
    private equipmentService: EquipmentService,
    private engineService: EngineService,
    private location: Location,
    private permissionService: PermissionActionService
  ) {
    this.thisYear = new Date().getFullYear();
  }

  ngOnInit(): void {
    this.getModel();
    this.permissionService.hasPermission(['admin']).subscribe(x => this.isAdmin = x);
    if (this.isAdmin === true) {
      this.getManufacturers();
      this.getEquipments();
      this.getEngines();
    }
  }

  getModelManufacturer(): void {
    this.manufacturerService.getById(this.manufacturerService.urls.manufacturerUrl, this.model.id)
      .subscribe(m => this.manufacturers = [m]);

  }
  getModelEquipment(): void {
    this.equipmentService.getById(this.equipmentService.urls.equipmentUrl, this.model.id)
      .subscribe(m => this.equipments = [m]);
  }
  getModelEngine(): void {
    this.engineService.getById(this.engineService.urls.engineUrl, this.model.id)
      .subscribe(m => this.engines = [m]);
  }

  getModel(): void {
    const segments: UrlSegment[] = this.route.snapshot.url;
    const lastSegment = segments[segments.length - 1];
    if (lastSegment.path.toLowerCase() === 'new') {
      this.model = new Model();
      return;
    }

    const id = +this.route.snapshot.paramMap.get('id');
    this.modelService.getById(this.modelService.urls.modelUrl, id)
      .subscribe((model: Model) => {
        this.model = model;
        this.getModelManufacturer();
        this.getModelEquipment();
        this.getModelEngine();
      });
  }

  getManufacturers(): void {
    this.manufacturerService.getAll(this.manufacturerService.urls.manufacturerUrl)
      .subscribe(m => this.manufacturers = m);
  }

  getEquipments(): void {
    this.equipmentService.getAll(this.equipmentService.urls.equipmentUrl)
      .subscribe(e => this.equipments = e);
  }

  getEngines(): void {
    this.engineService.getAll(this.engineService.urls.engineUrl)
      .subscribe(e => this.engines = e);
  }

  private updateModel(): void {
    const url = `${this.modelService.urls.modelUrl}/${this.model.id}`;
    this.modelService.update(url, this.model)
      .subscribe(_ => console.log('Updated!'));
  }

  private addModel(): void {
    this.modelService.add(this.modelService.urls.modelUrl, this.model)
      .subscribe((model) => {
        console.log('Added!');
        this.router.navigate(['/models']);
      });
  }

  getModelInventories(): () => Observable<Inventory[]> {
    return () => this.modelService.getInventoriesOfModel(this.model.id);
  }

  onSubmit(): void {
    if (this.form.invalid) {
      return;
    }

    if (this.model.id) {
      this.updateModel();
    } else {
      this.addModel();
    }
  }

  goBack(): void {
    this.location.back();
  }

}
