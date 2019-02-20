import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/DataProviders/User/user.service';
import { MatDialog } from '@angular/material';
import { RegistrationComponent } from '../Dialogs/Registration/registration.component';
import { BehaviorSubject, from, forkJoin } from 'rxjs';
import { switchMap, tap, mergeMap, map, toArray } from 'rxjs/operators';
import { Gender } from 'src/app/Model/gender.enum';
import { Role } from 'src/app/Model/role.enum';
import { ProjectDPService } from 'src/app/DataProviders/Project/project-dp.service';

@Component({
  selector: 'app-user-managment',
  templateUrl: './user-managment.component.html',
  styleUrls: ['./user-managment.component.css']
})
export class UserManagmentComponent implements OnInit {

  public Gender = Gender;
  public Role = Role;

  private readonly refreshToken = new BehaviorSubject(undefined);

  users = this.refreshToken.pipe(
    switchMap(() => this._userService.getAll().pipe(
      switchMap(employees => from(employees)),
      mergeMap(employee => this._projectService.getEmployeeProjects(employee.id)
                       .pipe(map(data => ({employee, projects: data })))),
          toArray()
      ))
    );





  constructor(
    private _userService: UserService,
    private _registerUserDialog: MatDialog,
    private _projectService: ProjectDPService) { }

  ngOnInit() {
  }

  public registerUserDialogOpen(): void {
    const dialogRef = this._registerUserDialog.open(RegistrationComponent);

    dialogRef.afterClosed().subscribe(dialogRes => {
        if (dialogRes) {
          this.refreshToken.next(undefined);
        }
    });

  }


}
