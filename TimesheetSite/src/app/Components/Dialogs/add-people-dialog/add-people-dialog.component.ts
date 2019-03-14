import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { UserService } from 'src/app/DataProviders/User/user.service';
import { Project } from 'src/app/Model/project';
import { AddEmployeesToProjectRequest } from 'src/app/Model/addEmployeesToProjectRequest';
import { Employee } from 'src/app/Model/employee';
import { ProjectDPService } from 'src/app/DataProviders/Project/project-dp.service';
import { ProjectWithEmployees } from 'src/app/Model/projectWithEmploees';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-add-people-dialog',
  templateUrl: './add-people-dialog.component.html',
  styleUrls: ['./add-people-dialog.component.css']
})
export class AddPeopleDialogComponent implements OnInit {

  selectedEmployees: Array<Employee>;
  users = this._userService.getAll().pipe(
    map(x => x.filter(c => !this.projectWithEmployees.employees.map(em => em.id).includes(c.id)))
  );

  constructor(
    @Inject(MAT_DIALOG_DATA) public projectWithEmployees: ProjectWithEmployees,
    public dialogRef: MatDialogRef<AddPeopleDialogComponent>,
    private _userService: UserService,
    private _projectDP: ProjectDPService) { }

  ngOnInit() {

  }


  public addEmployees(): void {

    const req: AddEmployeesToProjectRequest = new AddEmployeesToProjectRequest();
    req.projectId = this.projectWithEmployees.project.id;
    req.employeesIds = this.selectedEmployees.map(x => x.id);

    this._projectDP.addEmployeesToProject(req).subscribe(x => {
      if (x) {
        this.dialogRef.close(true);
      } else {
        // show error
      }
    });
  }

}
