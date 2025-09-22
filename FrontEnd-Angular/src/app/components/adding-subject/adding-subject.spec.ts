import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddingSubject } from './adding-subject';

describe('AddingSubject', () => {
  let component: AddingSubject;
  let fixture: ComponentFixture<AddingSubject>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddingSubject]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddingSubject);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
