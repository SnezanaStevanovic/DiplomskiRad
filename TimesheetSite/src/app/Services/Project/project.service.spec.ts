import { TestBed } from '@angular/core/testing';

import { ProjectService } from './project.service';

describe('ProjectServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ProjectService = TestBed.get(ProjectService);
    expect(service).toBeTruthy();
  });
});
