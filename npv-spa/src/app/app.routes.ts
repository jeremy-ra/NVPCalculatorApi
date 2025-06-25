import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NpvInputComponent } from './components/npv-input/npv-input.component';
import { NpvChartComponent } from './components/npv-chart/npv-chart.component';

export const routes: Routes = [
    { path: 'app-npv-input', component: NpvInputComponent },    
    { path: '', redirectTo: '/app-npv-input', pathMatch: 'full' },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
  })
  export class AppRoutingModule {}