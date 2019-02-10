import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { HttpClient } from '@angular/common/http';
import { Project } from '../../Model/project';
import { catchError, map } from 'rxjs/operators';
import { throwError, Observable } from 'rxjs';
import { BaseResponse } from 'src/app/Model/baseResponse';
import { ListProjectsResponse } from 'src/app/Model/listProjectsResponse';

@Injectable({
  providedIn: 'root'
})
export class ProjectDPService extends BaseService {

  constructor(protected _client: HttpClient) {
    super(_client);
  }


  public createProject(newProject: Project): Observable<BaseResponse> {
    const url = this._baseUrl + 'Project/AddNew';
    const postBody = JSON.stringify(newProject);
    return this._http.post<BaseResponse>(url, postBody, this.httpOptions)
      .pipe(
        catchError(err => throwError(err))
      );
  }


  public getAll(): Observable<Array<Project>> {
    const url = this._baseUrl + 'Project/GetAll';
    return this._http.get<ListProjectsResponse>(url, this.httpOptions)
      .pipe(
        map(res => {
            if (res.success) {
              return res.projects;
            } else {
              return new Array<Project>();
            }
        }),
        catchError(err => throwError(err))
      );
  }



}
