import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { HttpClient } from '@angular/common/http';
import { Task } from 'src/app/Model/task';
import { Observable } from 'rxjs/internal/Observable';
import { BaseResponse } from 'src/app/Model/baseResponse';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs/internal/observable/throwError';

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



}
