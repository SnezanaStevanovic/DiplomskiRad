import { TestBed } from '@angular/core/testing';

import { WorkTimerService } from './work-timer.service';

describe('WorkTimerService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: WorkTimerService = TestBed.get(WorkTimerService);
    expect(service).toBeTruthy();
  });
});
