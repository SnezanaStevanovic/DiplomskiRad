import { Component, OnInit } from '@angular/core';
import { map, switchMap, mergeMap, toArray } from 'rxjs/operators';
import { Breakpoints, BreakpointState, BreakpointObserver } from '@angular/cdk/layout';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { CreateProjectDialogComponent } from '../Dialogs/CreateProjectDialog/create-project-dialog.component';
import { ProjectDPService } from 'src/app/DataProviders/Project/project-dp.service';
import { ProjectCard } from 'src/app/Model/projectCard';
import { BehaviorSubject, from } from 'rxjs';
import { UserService } from 'src/app/DataProviders/User/user.service';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit {

  // projects = this.breakpointObserver.observe(Breakpoints.Handset).pipe(
  //   map(({ matches }) => {
  //     if (matches) {
  //       return [
  //         { title: 'Card 1', cols: 1, rows: 1 },
  //         { title: 'Card 2', cols: 1, rows: 1 },
  //         { title: 'Card 3', cols: 1, rows: 1 },
  //         { title: 'Card 4', cols: 1, rows: 1 }
  //       ];
  //     }

  //     return [
  //       { title: 'Card 1', cols: 1, rows: 1 },
  //       { title: 'Card 2', cols: 1, rows: 1 },
  //       { title: 'Card 3', cols: 1, rows: 1 },
  //       { title: 'Card 4', cols: 1, rows: 1 }
  //     ];
  //   })
  // );


  private readonly refreshToken = new BehaviorSubject(undefined);

  projects =  this.refreshToken.pipe(
    switchMap(() => this._projectService.getAll().pipe(

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
    ))
  );

  constructor(
    private breakpointObserver: BreakpointObserver,
    public dialog: MatDialog,
    private _projectService: ProjectDPService,
    private _userService: UserService) { }


    ngOnInit(): void {
      this.breakpointObserver.observe(Breakpoints.Handset).subscribe(x => {
        if (x.matches) {
          this.projects = this.projects.pipe(
            map(c => { c.forEach(p => {
              p.cols = 3;
              p.rows = 1;
            });
            return c;
          })
          );
        } else {
          this.projects = this.projects.pipe(
            map(c => { c.forEach(p => {
              p.cols = 1;
              p.rows = 1;
            });
            return c;
          })
          );
        }
      });
    }


  public createProjectDialogOpen() {
    const dialogRef = this.dialog.open(CreateProjectDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.refreshToken.next(undefined);
      }
    });
  }

}
