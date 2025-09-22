import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { instructorOrStudentAuthGuardGuard } from './instructor-or-student-auth-guard-guard';

describe('instructorOrStudentAuthGuardGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => instructorOrStudentAuthGuardGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
