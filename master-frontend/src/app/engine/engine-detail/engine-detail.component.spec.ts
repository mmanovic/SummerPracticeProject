import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EngineDetailComponent } from './engine-detail.component';

describe('EngineDetailComponent', () => {
  let component: EngineDetailComponent;
  let fixture: ComponentFixture<EngineDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EngineDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EngineDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
