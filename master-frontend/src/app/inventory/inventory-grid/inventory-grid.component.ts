import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

import { Inventory } from '../../models/inventory';
import { InventoryService } from '../../services/inventory.service';

@Component({
  selector: 'app-inventory-grid',
  templateUrl: './inventory-grid.component.html',
  styleUrls: ['./inventory-grid.component.css']
})
export class InventoryGridComponent implements OnInit {
  public inventories: Inventory[];

  constructor(
    private location: Location,
    private inventoryService: InventoryService) {
  }

  ngOnInit() {
    this.getInventories();
  }

  getInventories() {
    this.inventoryService.getAll(this.inventoryService.urls.inventoryUrl)
      .subscribe((data: Inventory[]) => this.inventories = data);
  }

  deleteInventory(inventory: Inventory): void {
    this.inventoryService.delete(this.inventoryService.urls.inventoryUrl, inventory)
      .subscribe(data => { if (data) {this.inventories = this.inventories.filter(e => e !== inventory); }});
  }

  goBack(): void {
    this.location.back();
  }

}
