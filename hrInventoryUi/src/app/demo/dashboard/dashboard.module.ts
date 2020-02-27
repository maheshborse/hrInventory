import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardRoutingModule } from './dashboard-routing.module';
import {SharedModule} from '../../theme/shared/shared.module';
import { MatProgressBarModule } from '@angular/material/progress-bar';
@NgModule({
  imports: [
    CommonModule,
    DashboardRoutingModule,
    SharedModule,
    MatProgressBarModule
  ]
})
export class DashboardModule { }
