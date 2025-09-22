import { TestBed } from '@angular/core/testing';

import { UserAuthentication } from './user-authentication';

describe('UserAuthentication', () => {
  let service: UserAuthentication;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UserAuthentication);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
