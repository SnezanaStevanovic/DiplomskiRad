import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormControl, Validators } from '@angular/forms';
import { ProjectDPService } from 'src/app/DataProviders/Project/project-dp.service';
import { Project } from 'src/app/Model/project';

@Component({
  selector: 'app-create-project-dialog',
  templateUrl: './create-project-dialog.component.html',
  styleUrls: ['./create-project-dialog.component.css']
})
export class CreateProjectDialogComponent implements OnInit {

   date: FormControl;
   projectNameControl: FormControl;


  constructor(
    public dialogRef: MatDialogRef<CreateProjectDialogComponent>,
    public _projectService: ProjectDPService) {
    this.date = new FormControl(new Date());
    this.projectNameControl = new FormControl('', Validators.required);
   }

  ngOnInit() {
  }

  public createProject(): void {
    if (!this.projectNameControl.valid) {
      return;
    }

    const newProject: Project = new Project();
    newProject.estimatedTime = this.date.value;
    newProject.name = this.projectNameControl.value;


    this._projectService.createProject(newProject).subscribe(x => {
        if (x.success) {
          this.dialogRef.close(true);
        } else {
          // show error
        }
    });

  }


}
