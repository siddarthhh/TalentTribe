import { TestBed } from '@angular/core/testing';

import { EmployerProfileService } from './employer-profile.service';

describe('EmployerProfileService', () => {
  let service: EmployerProfileService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmployerProfileService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
