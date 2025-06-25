import { Component, Input, OnChanges } from '@angular/core';
import { NpvResult } from '../../models/npv-result.model';
import { ChartDataset, ChartOptions } from 'chart.js';
import { NgChartsModule } from 'ng2-charts';

@Component({
  selector: 'app-npv-chart',
  standalone: true,
  imports: [NgChartsModule],
  templateUrl: './npv-chart.component.html',
  styleUrl: './npv-chart.component.scss'
})
export class NpvChartComponent implements OnChanges {
  
  @Input() results: NpvResult[] = [];

  chartLabels: string[] = [];
  chartData: ChartDataset [] = [{ data: [], label: 'NPV' }];
  chartOptions: ChartOptions = {
    responsive: true,
    maintainAspectRatio: false,
    scales: {
      y: { beginAtZero: true },      
    },
    plugins: {
      legend: {
        display: true,
        position: 'top',
      }
    }
  };

  ngOnChanges(): void {
    this.chartLabels = this.results.map(r => (r.rate * 100).toFixed(2) + '%');
    this.chartData = [{ data: this.results.map(r => r.npv), label: 'Net Present Value' }];
  }

}
