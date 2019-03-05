import { Task } from './task';
import { BaseResponse } from './baseResponse';

export interface TasksListResponse extends BaseResponse {
    projectTasks: Array<Task>;
}