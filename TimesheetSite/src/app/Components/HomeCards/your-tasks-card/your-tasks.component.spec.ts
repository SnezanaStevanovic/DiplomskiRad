import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { YourTasksComponent } from './your-tasks.component';

describe('YourTasksComponent', () => {
  let component: YourTasksComponent;
  let fixture: ComponentFixture<YourTasksComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ YourTasksComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(YourTasksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
