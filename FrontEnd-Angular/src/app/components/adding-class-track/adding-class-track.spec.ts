import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddingClassTrack } from './adding-class-track';

describe('AddingClassTrack', () => {
  let component: AddingClassTrack;
  let fixture: ComponentFixture<AddingClassTrack>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddingClassTrack]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddingClassTrack);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
