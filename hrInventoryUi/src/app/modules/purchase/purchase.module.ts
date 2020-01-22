import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {SharedModule} from '../../theme/shared/shared.module';
import { MatTableModule, MatSortModule, MatButtonModule, MatPaginatorModule, MatFormFieldModule,  MatDialogModule, MatIconModule, MatFormFieldControl } from '@angular/material';
import { PurchaseComponent } from './purchase.component';
import { PurchaseRoutingModule } from './purchase-routing.module';

@NgModule({
  imports: [
    CommonModule,
    PurchaseRoutingModule,
    MatPaginatorModule,
    MatTableModule,
    MatSortModule,
    MatButtonModule,
    SharedModule,
    MatIconModule,
    MatFormFieldModule
  ],
  declarations: [PurchaseComponent],
  
})
export class PurchaseModule { }