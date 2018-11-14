import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LastDaysWorkCardComponent } from './last-days-work-card.component';

describe('LastDaysWorkCardComponent', () => {
  let component: LastDaysWorkCardComponent;
  let fixture: ComponentFixture<LastDaysWorkCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LastDaysWorkCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LastDaysWorkCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
