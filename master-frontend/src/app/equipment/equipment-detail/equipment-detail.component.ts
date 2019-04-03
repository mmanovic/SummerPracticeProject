import { Component, OnInit, ViewChild } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute, UrlSegment, Router } from '@angular/router';

import { Equipment } from '../../models/equipment';
import { Model } from '../../models/model';
import { EquipmentService } from '../../services/equipment.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-equipment-detail',
  templateUrl: './equipment-detail.component.html',
  styleUrls: ['./equipment-detail.component.css']
})
export class EquipmentDetailComponent implements OnInit {

  @ViewChild('f') form: any;
  public equipment: Equipment;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private equipmentService: EquipmentService,
    private location: Location
  ) { }

  ngOnInit(): void {
    this.getEquipment();
  }

  getEquipment(): void {
    const segments: UrlSegment[] = this.route.snapshot.url;
    const lastSegment = segments[segments.length - 1];
    if (lastSegment.path.toLowerCase() === 'new') {
      this.equipment = new Equipment();
      return;
    }

    const id = +this.route.snapshot.paramMap.get('id');
    this.equipmentService.getById(this.equipmentService.urls.equipmentUrl, id)
      .subscribe((equipment: Equipment) => this.equipment = equipment);
  }

  private updateEquipment(): void {
    const url = `${this.equipmentService.urls.equipmentUrl}/${this.equipment.id}`;
    this.equipmentService.update(url, this.equipment)
      .subscribe(_ => console.log('Updated!'));
  }

  private addEquipment(): void {
    this.equipmentService.add(this.equipmentService.urls.equipmentUrl, this.equipment)
      .subscribe((equipment) => {
        console.log('Added!');
        this.router.navigate(['/equipments']);
      });
  }

  getEquipmentModels(): () => Observable<Model[]> {
    return () => this.equipmentService.getModelsOfEquipment(this.equipment.id);
  }

  onSubmit(): void {
    if (this.form.invalid) {
      return;
    }

    if (this.equipment.id) {
      this.updateEquipment();
    } else {
      this.addEquipment();
    }
  }

  goBack(): void {
    this.location.back();
  }
}
