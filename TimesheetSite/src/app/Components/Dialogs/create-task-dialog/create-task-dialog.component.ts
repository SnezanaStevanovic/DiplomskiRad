import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { TaskType } from 'src/app/Model/taskType';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ProjectWithEmployees } from 'src/app/Model/projectWithEmploees';
import { Task } from 'src/app/Model/task';
import { TaskDPService } from 'src/app/DataProviders/Task/task-dp.service';

@Component({
  selector: 'app-create-task-dialog',
  templateUrl: './create-task-dialog.component.html',
  styleUrls: ['./create-task-dialog.component.css']
})
export class CreateTaskDialogComponent implements OnInit {


  createTaskForm: FormGroup;
  taskTypes = TaskType;
  taskTypesKeys: any[];
  constructor(
    @Inject(MAT_DIALOG_DATA) public projectWithEmployees: ProjectWithEmployees,
    public dialogRef: MatDialogRef<CreateTaskDialogComponent>,
    private _formBuilder: FormBuilder,
    private _taskDP: TaskDPService) {

      this.createTaskForm = this._formBuilder.group({
        'Name': new FormControl('', Validators.required),
        'Description': new FormControl(''),
        'Type': new FormControl('', [Validators.required]),
        'EstimatedTime': new FormControl(''),
        'Employee': new FormControl('', [Validators.required]),
        'Project': new FormControl({value : projectWithEmployees.project.name, disabled : true})
      });

      this.taskTypesKeys = Object.keys(this.taskTypes).filter(Number);
     }

     get f() { return this.createTaskForm.controls; }


  ngOnInit() {
  }


  public createTask(): void {
    if (!this.createTaskForm.valid) {
      return;
    }

    const task: Task = new Task();
    task.name = this.createTaskForm.get('Name').value;
    task.description = this.createTaskForm.get('Description').value;
    task.type = this.createTaskForm.get('Type').value;
    task.estimatedTime = this.createTaskForm.get('EstimatedTime').value;
    task.projectId = this.projectWithEmployees.project.id;
    task.employeeId = this.createTaskForm.get('Employee').value;

    this._taskDP.createTask(task).subscribe(x => {
      if (x.success) {
        this.dialogRef.close(true);
      } else {
        // show error
      }
  });


  }

}
