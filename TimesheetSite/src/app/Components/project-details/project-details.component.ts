import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ProjectWithEmployees } from 'src/app/Model/projectWithEmploees';
import { Observable } from 'rxjs';
import { Task } from 'src/app/Model/task';
import { TaskDPService } from 'src/app/DataProviders/Task/task-dp.service';
import { TaskType } from 'src/app/Model/taskType';

@Component({
  selector: 'app-project-details',
  templateUrl: './project-details.component.html',
  styleUrls: ['./project-details.component.css']
})
export class ProjectDetailsComponent implements OnInit {

  projectWithEmployees: ProjectWithEmployees;
  progress: number;

  tasks: Observable<Array<Task>>;
  taskType = TaskType;
  constructor(private _router: Router, private _taskService: TaskDPService) {
    const navigation = this._router.getCurrentNavigation();
    const state = navigation.extras.state as {project: string};
    this.projectWithEmployees = JSON.parse(state.project);
    this.progress = this.projectWithEmployees.project.progress;
  }

  get isDirty()  {  return this.progress !== this.projectWithEmployees.project.progress; }

  ngOnInit() {
    this.tasks = this._taskService.getTasksForProject(this.projectWithEmployees.project.id);
  }

}
