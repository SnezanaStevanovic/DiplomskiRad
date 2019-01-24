import { TestBed } from '@angular/core/testing';

import { TimerService } from './timer-service.service';

describe('TimerServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TimerService = TestBed.get(TimerService);
    expect(service).toBeTruthy();
  });
});
