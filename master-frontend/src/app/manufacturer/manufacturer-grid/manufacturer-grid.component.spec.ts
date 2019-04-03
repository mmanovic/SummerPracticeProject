import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManufacturerGridComponent } from './manufacturer-grid.component';

describe('ManufacturerGridComponent', () => {
  let component: ManufacturerGridComponent;
  let fixture: ComponentFixture<ManufacturerGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManufacturerGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManufacturerGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
