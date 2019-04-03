import { TestBed, inject } from '@angular/core/testing';

import { PermissionActionService } from './permission-action.service';

describe('PermissionActionService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PermissionActionService]
    });
  });

  it('should be created', inject([PermissionActionService], (service: PermissionActionService) => {
    expect(service).toBeTruthy();
  }));
});
