import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Assigment } from './assigment';

describe('Assigment', () => {
  let component: Assigment;
  let fixture: ComponentFixture<Assigment>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Assigment]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Assigment);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
