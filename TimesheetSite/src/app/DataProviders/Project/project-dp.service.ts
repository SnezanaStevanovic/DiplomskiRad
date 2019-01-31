import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { HttpClient } from '@angular/common/http';
import { Project } from 'src/app/Model/project';
import { catchError } from 'rxjs/operators';
import { throwError, Observable } from 'rxjs';
import { BaseResponse } from 'src/app/Model/baseResponse';

@Injectable({
  providedIn: 'root'
})
export class ProjectDPService extends BaseService {

  constructor(protected _client: HttpClient) {
    super(_client);
  }


  public createProject(newProject: Project): Observable<BaseResponse> {
    const url = this._baseUrl + 'Project/AddNew';
    const postBudy = JSON.stringify(newProject);
    return this._http.post<BaseResponse>(url, postBudy, this.httpOptions)
      .pipe(
        catchError(err => throwError(err))
      );
  }


}
