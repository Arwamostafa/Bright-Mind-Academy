import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LiveMeeting } from './live-meeting';

describe('LiveMeeting', () => {
  let component: LiveMeeting;
  let fixture: ComponentFixture<LiveMeeting>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LiveMeeting]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LiveMeeting);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
