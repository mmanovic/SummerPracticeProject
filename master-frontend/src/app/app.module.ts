import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { KeycloakService, KeycloakAngularModule } from 'keycloak-angular';

import { AppComponent } from './app.component';
import { CityGridComponent } from './city/city-grid/city-grid.component';
import { AppRoutingModule } from './app-routing.module';
import { CityDetailComponent } from './city/city-detail/city-detail.component';
import { EnginesGridComponent } from './engine/engines-grid/engines-grid.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { UiModule } from './ui/ui.module';
import { EngineDetailComponent } from './engine/engine-detail/engine-detail.component';
import { CountryGridComponent } from './country/country-grid/country-grid.component';
import { CountryDetailComponent } from './country/country-detail/country-detail.component';
import { SalonGridComponent } from './salon/salon-grid/salon-grid.component';
import { SalonDetailComponent } from './salon/salon-detail/salon-detail.component';
import { InventoryGridComponent } from './inventory/inventory-grid/inventory-grid.component';
import { InventoryDetailComponent } from './inventory/inventory-detail/inventory-detail.component';
import { ModelGridComponent } from './model/model-grid/model-grid.component';
import { ModelDetailComponent } from './model/model-detail/model-detail.component';
import { ManufacturerGridComponent } from './manufacturer/manufacturer-grid/manufacturer-grid.component';
import { ManufacturerDetailComponent } from './manufacturer/manufacturer-detail/manufacturer-detail.component';
import { EngineTypeGridComponent } from './engine-type/engine-type-grid/engine-type-grid.component';
import { EngineTypeDetailComponent } from './engine-type/engine-type-detail/engine-type-detail.component';
import { EquipmentGridComponent } from './equipment/equipment-grid/equipment-grid.component';
import { EquipmentDetailComponent } from './equipment/equipment-detail/equipment-detail.component';
import { MinValidatorDirective } from './validators/min-validator.directive';
import { ModelListingComponent } from './model-listing/model-listing.component';
import { MaxValidatorDirective } from './validators/max-validator.directive';
import { ErrorInterceptor } from './interceptors/error.interceptor';
import { StatisticComponent } from './additional/statistic/statistic.component';
import { initializer } from './util/app-init';
import { AppAuthGuard } from './util/app-auth-guard';
import { ErrorPageComponent } from './alert/error-page/error-page.component';
import { CanAccessDirective } from './permission/can-access.directive';
import { ReadonlyDirective } from './permission/readonly.directive';


@NgModule({
  declarations: [
    AppComponent,
    CityGridComponent,
    CityDetailComponent,
    EnginesGridComponent,
    DashboardComponent,
    EngineDetailComponent,
    CountryGridComponent,
    CountryDetailComponent,
    SalonGridComponent,
    SalonDetailComponent,
    InventoryGridComponent,
    InventoryDetailComponent,
    ModelGridComponent,
    ModelDetailComponent,
    ManufacturerGridComponent,
    ManufacturerDetailComponent,
    EngineTypeGridComponent,
    EngineTypeDetailComponent,
    EquipmentGridComponent,
    EquipmentDetailComponent,
    MinValidatorDirective,
    ModelListingComponent,
    MaxValidatorDirective,
    StatisticComponent,
    ErrorPageComponent,
    CanAccessDirective,
    ReadonlyDirective
  ],
  imports: [
    KeycloakAngularModule,
    FormsModule,
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    UiModule
  ],
  providers: [
    AppAuthGuard,
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: APP_INITIALIZER, useFactory: initializer, multi: true, deps: [KeycloakService] }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
