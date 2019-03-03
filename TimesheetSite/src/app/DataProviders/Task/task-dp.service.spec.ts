import { TestBed } from '@angular/core/testing';

import { TaskDPService } from './task-dp.service';

describe('TaskDPService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TaskDPService = TestBed.get(TaskDPService);
    expect(service).toBeTruthy();
  });
});
