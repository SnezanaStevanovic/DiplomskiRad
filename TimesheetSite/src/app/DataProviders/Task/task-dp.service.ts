import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { HttpClient } from '@angular/common/http';
import { Task } from 'src/app/Model/task';
import { Observable } from 'rxjs/internal/Observable';
import { BaseResponse } from 'src/app/Model/baseResponse';
import { catchError, map } from 'rxjs/operators';
import { throwError } from 'rxjs/internal/observable/throwError';
import { TasksListResponse } from 'src/app/Model/taskListResponse';

@Injectable({
  providedIn: 'root'
})
export class TaskDPService extends BaseService {

  constructor(protected _client: HttpClient) {
    super(_client);
  }

  public createTask(newTask: Task): Observable<BaseResponse> {
    const url = this._baseUrl + 'Task/AddNew';
    const postBody = JSON.stringify(newTask);
    return this._http.post<BaseResponse>(url, postBody, this.httpOptions)
      .pipe(
        catchError(err => throwError(err))
      );
  }


  public getLastNTasksForEmployee(employeeId: number, n: number): Observable<Array<Task>> {
    const url = this._baseUrl + `Task/GetNTasksForEmployee?employeeId=${employeeId}&n=${n}`;
    return this._http.get<TasksListResponse>(url, this.httpOptions)
      .pipe(
        map(res => {
            if (res.success) {
              return res.projectTasks;
            } else {
              return new Array<Task>();
            }
        }),
        catchError(err => throwError(err))
      );
  }


  public getTasksForProject(projectId: number): Observable<Array<Task>> {
    const url = this._baseUrl + `Task/GetAllTasksPerProject/${projectId}`;
    return this._http.get<TasksListResponse>(url, this.httpOptions)
      .pipe(
        map(res => {
            if (res.success) {
              return res.projectTasks;
            } else {
              return new Array<Task>();
            }
        }),
        catchError(err => throwError(err))
      );
  }


  public getAllTasksForEmployee(employeeId: number): Observable<Array<Task>> {
    const url = this._baseUrl + `Task/GetAllEmployeeTasks/${employeeId}`;
    return this._http.get<TasksListResponse>(url, this.httpOptions)
      .pipe(
        map(res => {
            if (res.success) {
              return res.projectTasks;
            } else {
              return new Array<Task>();
            }
        }),
        catchError(err => throwError(err))
      );
  }

}
