import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { NpvInputComponent } from './components/npv-input/npv-input.component';
import { NpvChartComponent } from './components/npv-chart/npv-chart.component';
import { NgChartsModule } from 'ng2-charts';

@NgModule({
  declarations: [AppComponent, NpvInputComponent, NpvChartComponent],
  imports: [BrowserModule, HttpClientModule, FormsModule, NgChartsModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}

