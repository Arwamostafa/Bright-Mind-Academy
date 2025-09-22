import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddingInstructor } from './adding-instructor';

describe('AddingInstructor', () => {
  let component: AddingInstructor;
  let fixture: ComponentFixture<AddingInstructor>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddingInstructor]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddingInstructor);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
