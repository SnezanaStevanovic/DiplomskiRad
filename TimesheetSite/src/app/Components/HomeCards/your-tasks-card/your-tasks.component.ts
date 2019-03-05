import { Component, OnInit } from '@angular/core';
import { TaskDPService } from 'src/app/DataProviders/Task/task-dp.service';
import { AuthService } from 'src/app/Services/auth.service';
import { LoggedUser } from 'src/app/Model/loggedUser';
import { TaskType } from 'src/app/Model/taskType';

@Component({
  selector: 'app-your-tasks',
  templateUrl: './your-tasks.component.html',
  styleUrls: ['./your-tasks.component.css']
})
export class YourTasksComponent implements OnInit {

  private loggedUser: LoggedUser;
  taskType = TaskType;
  constructor(private _taskService: TaskDPService, private _authService: AuthService) {
   this.loggedUser = _authService.getLoggedUser();
  }

  tasks = this._taskService.getLastNTasksForEmployee(this._authService.getLoggedUser().employeeId, 5);


  ngOnInit() {

  }

}
