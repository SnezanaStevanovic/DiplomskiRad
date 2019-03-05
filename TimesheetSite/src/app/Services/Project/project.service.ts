import { Injectable } from '@angular/core';
import { AuthService } from '../auth.service';
import { ProjectDPService } from 'src/app/DataProviders/Project/project-dp.service';
import { Observable } from 'rxjs';
import { Project } from 'src/app/Model/project';
import { LoggedUser } from 'src/app/Model/loggedUser';
import { Role } from 'src/app/Model/role.enum';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private _authService: AuthService, private _projectDP: ProjectDPService) { }



  public getProjects(): Observable<Array<Project>> {

    const loggedUser: LoggedUser =  this._authService.getLoggedUser();

    if (loggedUser.role === Role.Admin) {
      return this._projectDP.getAll();
    } else {
      return this._projectDP.getEmployeeProjects(loggedUser.employeeId);
    }

  }

}
