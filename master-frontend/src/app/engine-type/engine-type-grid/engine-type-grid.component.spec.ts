import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EngineTypeGridComponent } from './engine-type-grid.component';

describe('EngineTypeGridComponent', () => {
  let component: EngineTypeGridComponent;
  let fixture: ComponentFixture<EngineTypeGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EngineTypeGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EngineTypeGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
