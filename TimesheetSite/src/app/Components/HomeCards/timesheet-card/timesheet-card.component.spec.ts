import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TimesheetCardComponent } from './timesheet-card.component';

describe('TimesheetCardComponent', () => {
  let component: TimesheetCardComponent;
  let fixture: ComponentFixture<TimesheetCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TimesheetCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TimesheetCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
