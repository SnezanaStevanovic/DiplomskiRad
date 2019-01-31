import { TestBed } from '@angular/core/testing';

import { ProjectDPService } from './project-dp.service';

describe('ProjectDPService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ProjectDPService = TestBed.get(ProjectDPService);
    expect(service).toBeTruthy();
  });
});
