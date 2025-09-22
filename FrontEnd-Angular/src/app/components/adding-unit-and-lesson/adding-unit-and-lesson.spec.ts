import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddingUnitAndLesson } from './adding-unit-and-lesson';

describe('AddingUnitAndLesson', () => {
  let component: AddingUnitAndLesson;
  let fixture: ComponentFixture<AddingUnitAndLesson>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddingUnitAndLesson]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddingUnitAndLesson);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
