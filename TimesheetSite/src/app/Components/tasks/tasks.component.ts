import { Component, OnInit } from '@angular/core';
import { TaskDPService } from 'src/app/DataProviders/Task/task-dp.service';
import { AuthService } from 'src/app/Services/auth.service';
import { switchMap, mergeMap, map, toArray } from 'rxjs/operators';
import { from } from 'rxjs';
import { ProjectDPService } from 'src/app/DataProviders/Project/project-dp.service';
import { TaskWithDetails } from 'src/app/Model/taskWithDetails';
import { Employee } from 'src/app/Model/employee';
import { NavigationExtras, Router } from '@angular/router';
import { TaskType } from 'src/app/Model/taskType';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {

  constructor(
    private _taskService: TaskDPService,
    private _authService: AuthService,
    private _projectDP: ProjectDPService,
    private _router: Router) { }

  taskType = TaskType;
  tasks = this._taskService.getAllTasksForEmployee(this._authService.getLoggedUser().employeeId).pipe(
    switchMap(tasks => from(tasks)),
    mergeMap(task =>  this._projectDP.getById(task.projectId).pipe(
      map(project => {
        const taskWithDetails: TaskWithDetails = new TaskWithDetails();
        taskWithDetails.task = task;
        taskWithDetails.project = project;
        return taskWithDetails;
      })
    )),
    toArray(),
  );


  ngOnInit() {
  }


  goToTaskDetails(task: TaskWithDetails) {
    const navigationExtras: NavigationExtras = {state: {project: JSON.stringify(task)}};

    this._router.navigate(['/main/taskDetails'], navigationExtras);
  }


}
