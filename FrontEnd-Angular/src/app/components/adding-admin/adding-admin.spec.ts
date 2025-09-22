import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddingAdmin } from './adding-admin';

describe('AddingAdmin', () => {
  let component: AddingAdmin;
  let fixture: ComponentFixture<AddingAdmin>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddingAdmin]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddingAdmin);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
