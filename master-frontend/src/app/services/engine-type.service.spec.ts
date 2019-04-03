import { TestBed, inject } from '@angular/core/testing';

import { EngineTypeService } from './engine-type.service';

describe('EngineTypeService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [EngineTypeService]
    });
  });

  it('should be created', inject([EngineTypeService], (service: EngineTypeService) => {
    expect(service).toBeTruthy();
  }));
});
