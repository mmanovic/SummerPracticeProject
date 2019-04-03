import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EngineTypeDetailComponent } from './engine-type-detail.component';

describe('EngineTypeDetailComponent', () => {
  let component: EngineTypeDetailComponent;
  let fixture: ComponentFixture<EngineTypeDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EngineTypeDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EngineTypeDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
