import { TestBed, inject } from '@angular/core/testing';

import { SalonService } from './salon.service';

describe('SalonService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SalonService]
    });
  });

  it('should be created', inject([SalonService], (service: SalonService) => {
    expect(service).toBeTruthy();
  }));
});
