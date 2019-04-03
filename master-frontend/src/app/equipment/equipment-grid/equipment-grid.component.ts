import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

import { Equipment } from '../../models/equipment';
import { EquipmentService } from '../../services/equipment.service';

@Component({
  selector: 'app-equipment-grid',
  templateUrl: './equipment-grid.component.html',
  styleUrls: ['./equipment-grid.component.css']
})
export class EquipmentGridComponent implements OnInit {
  public equipments: Equipment[];

  constructor(
    private location: Location,
    private equipmentService: EquipmentService) {
  }

  ngOnInit() {
    this.getEquipments();
  }

  getEquipments() {
    this.equipmentService.getAll(this.equipmentService.urls.equipmentUrl)
      .subscribe(equipments => this.equipments = equipments);
  }

  deleteEquipment(equipment: Equipment): void {
    this.equipmentService.delete(this.equipmentService.urls.equipmentUrl, equipment)
      .subscribe(data => { if (data) {this.equipments = this.equipments.filter(e => e !== equipment); }});
  }

  goBack(): void {
    this.location.back();
  }

}
