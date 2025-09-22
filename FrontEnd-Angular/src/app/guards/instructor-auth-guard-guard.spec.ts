import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { instructorAuthGuardGuard } from './instructor-auth-guard-guard';

describe('instructorAuthGuardGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => instructorAuthGuardGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
