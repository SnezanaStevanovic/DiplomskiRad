import { TestBed } from '@angular/core/testing';

import { TimesheetDataProviderService } from './timesheet-data-provider.service';

describe('TimesheetDataProviderService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TimesheetDataProviderService = TestBed.get(TimesheetDataProviderService);
    expect(service).toBeTruthy();
  });
});
