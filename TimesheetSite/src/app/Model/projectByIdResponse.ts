import { BaseResponse } from './baseResponse';
import { Project } from './project';

export interface GetProjectByIdResponse extends BaseResponse {
        project: Project;
    }