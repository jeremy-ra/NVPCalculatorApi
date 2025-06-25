import { Component, ViewChild } from '@angular/core';
import { NpvResult } from '../../models/npv-result.model';
import { NpvService } from '../../services/npv.service';
import { NpvInput } from '../../models/npv-input.model';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NpvChartComponent } from '../npv-chart/npv-chart.component';

@Component({
  selector: 'app-npv-input',
  standalone: true,
  imports: [ FormsModule, CommonModule, NpvChartComponent ],
  templateUrl: './npv-input.component.html',
  styleUrl: './npv-input.component.scss'
})
export class NpvInputComponent {
  cashFlows = '';
  lowerRate = 1.0;
  upperRate = 15.0;
  increment = 0.25;

  results: NpvResult[] = [];
  loading = false;
  errorMessage = '';
  @ViewChild('ngForm') ngForm!: NgForm;

  constructor(private npvService: NpvService) {}

  onSubmit(): void {
    this.errorMessage = '';
    this.results = [];

    // Validate inputs
    if (!this.cashFlows.trim()) {
      this.errorMessage = 'Cash flows are required.';
      return;
    }

    const cashFlows = this.cashFlows
      .split(',')
      .map(x => parseFloat(x.trim()))
      .filter(x => !isNaN(x));

    if (cashFlows.length === 0) {
      this.errorMessage = 'Please enter valid cash flows.';
      return;
    }

    if (this.lowerRate < 0 || this.upperRate < 0 || this.increment <= 0) {
      this.errorMessage = 'Discount rates must be positive and increment > 0.';
      return;
    }

    if (this.lowerRate > this.upperRate) {
      this.errorMessage = 'Lower rate cannot be greater than upper rate.';
      return;
    }

    const input: NpvInput = {
      cashFlows,
      lowerBoundDiscountRate: this.lowerRate / 100,
      upperBoundDiscountRate: this.upperRate / 100,
      discountRateIncrement: this.increment / 100
    };

    this.loading = true;

    this.npvService.calculate(input).subscribe({
      next: results => {
        this.results = results;
        this.loading = false;
      },
      error: err => {
        this.errorMessage = 'Failed to calculate NPV.';
        this.loading = false;
      }
    });
  }
}
