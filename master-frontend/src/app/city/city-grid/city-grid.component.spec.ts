import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CityGridComponent } from './city-grid.component';

describe('CityGridComponent', () => {
  let component: CityGridComponent;
  let fixture: ComponentFixture<CityGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CityGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CityGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
