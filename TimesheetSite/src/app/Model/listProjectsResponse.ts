import { Project } from './project';
import { BaseResponse } from './baseResponse';

export interface ListProjectsResponse extends BaseResponse {
    projects: Array<Project>;
}
