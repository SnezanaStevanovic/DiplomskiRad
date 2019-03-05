import { Component, OnInit } from '@angular/core';
import { map, switchMap, mergeMap, toArray } from 'rxjs/operators';
import { Breakpoints, BreakpointState, BreakpointObserver } from '@angular/cdk/layout';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { CreateProjectDialogComponent } from '../Dialogs/CreateProjectDialog/create-project-dialog.component';
import { ProjectCard } from 'src/app/Model/projectCard';
import { BehaviorSubject, from } from 'rxjs';
import { UserService } from 'src/app/DataProviders/User/user.service';
import { CreateTaskDialogComponent } from '../Dialogs/create-task-dialog/create-task-dialog.component';
import { ProjectWithEmployees } from 'src/app/Model/projectWithEmploees';
import { ProjectService } from 'src/app/Services/Project/project.service';
import { Router, NavigationExtras } from '@angular/router';
import { JsonPipe } from '@angular/common';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit {


  private readonly refreshToken = new BehaviorSubject(undefined);

  projects =  this.refreshToken.pipe(
    switchMap(() => this._projectService.getProjects().pipe(
      switchMap(projects => from(projects)),
      mergeMap(project => this._userService.getAllForProject(project.id).pipe(
        map(employees => {
               const projectCard = new ProjectCard();
               projectCard.project = project;
               projectCard.employees = employees;
               projectCard.cols = 1;
               projectCard.rows = 1;
               return projectCard;
        }))),
        toArray()
    )),
    map(projects =>  projects.sort(this.sortById))
  );

  constructor(
    private breakpointObserver: BreakpointObserver,
    public dialog: MatDialog,
    private _projectService: ProjectService,
    private _userService: UserService,
    private _router: Router) { }


    ngOnInit(): void {

    }


   public sortById(a: ProjectCard, b: ProjectCard) {
      if (a.project.id < b.project.id) {
        return -1;
      }
      if (a.project.id > b.project.id) {
        return 1;
      }
      return 0;
    }

  public createProjectDialogOpen() {
    const dialogRef = this.dialog.open(CreateProjectDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.refreshToken.next(undefined);
      }
    });
  }


  public AddTask(projectCard: ProjectCard) {

    const dialogData: ProjectWithEmployees = new ProjectWithEmployees();
    dialogData.project = projectCard.project;
    dialogData.employees = projectCard.employees;

    const dialogRef = this.dialog.open(CreateTaskDialogComponent, {
      data: dialogData
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
      }
    });
  }



  public goToProjectDetails(projectCard: ProjectCard) {

    const dataToPass: ProjectWithEmployees = new ProjectWithEmployees();
    dataToPass.project = projectCard.project;
    dataToPass.employees = projectCard.employees;

    const navigationExtras: NavigationExtras = {state: {project: JSON.stringify(dataToPass)}};

    this._router.navigate(['/main/projectDetails'], navigationExtras);
  }

}
