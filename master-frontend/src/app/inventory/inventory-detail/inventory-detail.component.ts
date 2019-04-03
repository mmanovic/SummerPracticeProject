import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router, UrlSegment } from '@angular/router';
import { Location } from '@angular/common';
import { InventoryService } from '../../services/inventory.service';
import { Inventory } from '../../models/inventory';
import { Model } from '../../models/model';
import { Salon } from '../../models/salon';
import { ModelService } from '../../services/model.service';
import { SalonService } from '../../services/salon.service';

@Component({
  selector: 'app-inventory-detail',
  templateUrl: './inventory-detail.component.html',
  styleUrls: ['./inventory-detail.component.css']
})
export class InventoryDetailComponent implements OnInit {

  @ViewChild('f') form: any;
  inventory: Inventory;
  salons: Salon[] = [];
  models: Model[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private inventoryService: InventoryService,
    private salonService: SalonService,
    private modelService: ModelService,
    private location: Location
  ) { }

  ngOnInit(): void {
    this.getInventory();
    this.getModels();
    this.getSalons();
  }

  getInventory(): void {
    const segments: UrlSegment[] = this.route.snapshot.url;
    const lastSegment = segments[segments.length - 1];
    if (lastSegment.path.toLowerCase() === 'new') {
      this.inventory = new Inventory();
      return;
    }

    const id = +this.route.snapshot.paramMap.get('id');
    this.inventoryService.getById(this.inventoryService.urls.inventoryUrl, id)
      .subscribe((inventory: Inventory) => this.inventory = inventory);
  }

  getModels(): void  {
    this.modelService.getAll(this.modelService.urls.modelUrl)
      .subscribe(models => this.models = models);
  }

  getSalons(): void  {
    this.salonService.getAll(this.salonService.urls.salonUrl)
      .subscribe(salons => this.salons = salons);
  }

  private updateInventory(): void {
    const url = `${this.inventoryService.urls.inventoryUrl}/${this.inventory.id}`;
    this.inventoryService.update(url, this.inventory)
      .subscribe(_ => console.log('Updated!'));
  }

  private addInventory(): void {
    this.inventoryService.add(this.inventoryService.urls.inventoryUrl, this.inventory)
      .subscribe((inventory) => {
        console.log('Added!');
        this.router.navigate(['/inventories']);
      });
  }

  onSubmit(): void {
    if (this.form.invalid) {
      return;
    }

    if (this.inventory.id) {
      this.updateInventory();
    } else {
      this.addInventory();
    }
  }

  goBack(): void {
    this.location.back();
  }

}
