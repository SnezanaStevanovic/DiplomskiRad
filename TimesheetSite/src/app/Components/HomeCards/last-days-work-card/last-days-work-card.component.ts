import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Chart } from 'chart.js';



@Component({
  selector: 'app-last-days-work-card',
  templateUrl: './last-days-work-card.component.html',
  styleUrls: ['./last-days-work-card.component.css']
})
export class LastDaysWorkCardComponent implements OnInit, AfterViewInit {


  constructor() { }

  chart = [];

  testData = [{ day: 'ponedeljak', workTime: 8 }, { day: 'utorak', workTime: 7 }, { day: 'sreda', workTime: 6 }, { day: 'cetvrtak', workTime: 9 }]


  ngOnInit() {
  }

  ngAfterViewInit(): void {
    this.chart = new Chart('canvas', {
      type: 'line',
      data: {
        labels: ["Ponedeljak", "Utorak", "Sreda", "Cetvrtak", "Petak", "Supota"],
        datasets: [
          {
            data: [8, 9, 8, 7, 6, 8],
            borderColor: "#3cba9f",
            fill: false
          }
        ]
      },
      options: {
        legend: {
          display: false
        },
        scales: {
          xAxes: [{
            display: true,

          }],
          yAxes: [{
            display: true,
            ticks: {
              beginAtZero: true
            }
          }],
        },
        maintainAspectRatio: false
      }
    });
  }

}
