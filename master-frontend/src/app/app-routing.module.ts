import { NgModule } from '@angular/core';
import { RouterModule, Routes, UrlSegment } from '@angular/router';
import { CityGridComponent } from './city/city-grid/city-grid.component';
import { CityDetailComponent } from './city/city-detail/city-detail.component';
import { EnginesGridComponent } from './engine/engines-grid/engines-grid.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { EngineDetailComponent } from './engine/engine-detail/engine-detail.component';
import { CountryDetailComponent } from './country/country-detail/country-detail.component';
import { CountryGridComponent } from './country/country-grid/country-grid.component';
import { SalonDetailComponent } from './salon/salon-detail/salon-detail.component';
import { SalonGridComponent } from './salon/salon-grid/salon-grid.component';
import { InventoryDetailComponent } from './inventory/inventory-detail/inventory-detail.component';
import { InventoryGridComponent } from './inventory/inventory-grid/inventory-grid.component';
import { ModelDetailComponent } from './model/model-detail/model-detail.component';
import { ModelGridComponent } from './model/model-grid/model-grid.component';
import { ManufacturerDetailComponent } from './manufacturer/manufacturer-detail/manufacturer-detail.component';
import { ManufacturerGridComponent } from './manufacturer/manufacturer-grid/manufacturer-grid.component';
import { EngineTypeDetailComponent } from './engine-type/engine-type-detail/engine-type-detail.component';
import { EngineTypeGridComponent } from './engine-type/engine-type-grid/engine-type-grid.component';
import { EquipmentDetailComponent } from './equipment/equipment-detail/equipment-detail.component';
import { EquipmentGridComponent } from './equipment/equipment-grid/equipment-grid.component';
import { StatisticComponent } from './additional/statistic/statistic.component';
import { AppAuthGuard } from './util/app-auth-guard';
import { ErrorPageComponent } from './alert/error-page/error-page.component';

const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  {
    path: 'errorPage', component: ErrorPageComponent
  },

  // cities
  {
    path: 'cities/detail/:id', component: CityDetailComponent
  },
  {
    path: 'cities/new',
    component: CityDetailComponent,
    canActivate: [AppAuthGuard],
    data: {
      roles: ['admin']
    }
  },
  {
    path: 'cities',
    component: CityGridComponent,
    data: {
      roles: ['admin']
    }
  },

  // engines
  {
    path: 'engines/detail/:id', component: EngineDetailComponent,
    canActivate: [AppAuthGuard],
    data: {
      roles: ['admin']
    }
  },
  {
    path: 'engines/new', component: EngineDetailComponent,
    canActivate: [AppAuthGuard],
    data: {
      roles: ['admin']
    }
  },
  {
    path: 'engines', component: EnginesGridComponent,
    canActivate: [AppAuthGuard],
    data: {
      roles: ['admin']
    }
  },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'countries/detail/:id', component: CountryDetailComponent },
  {
    path: 'countries/new', component: CountryDetailComponent,
    canActivate: [AppAuthGuard],
    data: {
      roles: ['admin']
    }
  },
  { path: 'countries', component: CountryGridComponent },
  { path: 'salons/detail/:id', component: SalonDetailComponent },
  {
    path: 'salons/new', component: SalonDetailComponent,
    canActivate: [AppAuthGuard],
    data: {
      roles: ['admin']
    }
  },
  { path: 'salons', component: SalonGridComponent },
  { path: 'inventories/detail/:id', component: InventoryDetailComponent },
  {
    path: 'inventories/new', component: InventoryDetailComponent,
    canActivate: [AppAuthGuard],
    data: {
      roles: ['admin', 'employee']
    }
  },
  { path: 'inventories', component: InventoryGridComponent },
  { path: 'models/detail/:id', component: ModelDetailComponent },
  {
    path: 'models/new', component: ModelDetailComponent,
    canActivate: [AppAuthGuard],
    data: {
      roles: ['admin']
    }
  },
  { path: 'models', component: ModelGridComponent },
  { path: 'manufacturers/detail/:id', component: ManufacturerDetailComponent },
  {
    path: 'manufacturers/new', component: ManufacturerDetailComponent,
    canActivate: [AppAuthGuard],
    data: {
      roles: ['admin']
    }
  },
  { path: 'manufacturers', component: ManufacturerGridComponent },
  {
    path: 'engineTypes/detail/:id', component: EngineTypeDetailComponent,
    canActivate: [AppAuthGuard],
    data: {
      roles: ['admin']
    }
  },
  {
    path: 'engineTypes/new', component: EngineTypeDetailComponent,
    canActivate: [AppAuthGuard],
    data: {
      roles: ['admin']
    }
  },
  {
    path: 'engineTypes', component: EngineTypeGridComponent,
    canActivate: [AppAuthGuard],
    data: {
      roles: ['admin']
    }
  },
  {
    path: 'equipments/detail/:id', component: EquipmentDetailComponent,
    canActivate: [AppAuthGuard],
    data: {
      roles: ['admin', 'employee']
    }
  },
  {
    path: 'equipments/new', component: EquipmentDetailComponent,
    canActivate: [AppAuthGuard],
    data: {
      roles: ['admin']
    }
  },
  {
    path: 'equipments', component: EquipmentGridComponent,
    canActivate: [AppAuthGuard],
    data: {
      roles: ['admin', 'employee']
    }
  },
  { path: 'statistic', component: StatisticComponent },
  // { path: 'error', component: ErrorComponent}
  // { path: '**', redirectTo: '/dasboard' },
];

@NgModule({
  exports: [RouterModule],
  imports: [RouterModule.forRoot(routes)]
})
export class AppRoutingModule { }
