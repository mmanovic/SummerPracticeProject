import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnginesGridComponent } from './engines-grid.component';

describe('EnginesGridComponent', () => {
  let component: EnginesGridComponent;
  let fixture: ComponentFixture<EnginesGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EnginesGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnginesGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
