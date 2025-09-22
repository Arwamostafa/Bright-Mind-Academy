import { TestBed } from '@angular/core/testing';

import { Chatbotservice } from './chatbotservice';

describe('Chatbotservice', () => {
  let service: Chatbotservice;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Chatbotservice);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
