import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { studentAuthGuardGuard } from './student-auth-guard-guard';

describe('studentAuthGuardGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => studentAuthGuardGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
