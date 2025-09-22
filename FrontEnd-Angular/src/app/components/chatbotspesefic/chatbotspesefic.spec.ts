import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Chatbotspesefic } from './chatbotspesefic';

describe('Chatbotspesefic', () => {
  let component: Chatbotspesefic;
  let fixture: ComponentFixture<Chatbotspesefic>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Chatbotspesefic]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Chatbotspesefic);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
