import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-your-tasks',
  templateUrl: './your-tasks.component.html',
  styleUrls: ['./your-tasks.component.css']
})
export class YourTasksComponent implements OnInit {


  constructor() { }

  tasks = [{ name: 'task1', estimatedTime: 8, type: "bug" },
  { name: 'task2', estimatedTime: 7, type: "f" },
  { name: 'task3', estimatedTime: 6, type: "f" },
  { name: 'task4', estimatedTime: 9, type: "bug" }];


  ngOnInit() {

  }

}
