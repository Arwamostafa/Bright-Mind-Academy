import { TestBed } from '@angular/core/testing';

import { ApiSubjectSrevice } from './api-subject-srevice';

describe('ApiSubjectSrevice', () => {
  let service: ApiSubjectSrevice;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApiSubjectSrevice);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
