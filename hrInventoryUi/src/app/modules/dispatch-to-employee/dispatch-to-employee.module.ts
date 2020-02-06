import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {SharedModule} from '../../theme/shared/shared.module';
import { MatTableModule, MatSortModule, MatButtonModule, MatPaginatorModule, MatFormFieldModule,  MatDialogModule, MatIconModule, MatFormFieldControl, MatInputModule, MatDatepickerModule, MatNativeDateModule, MatSelectModule } from '@angular/material';


import { DispatchToEmployeeRoutingModule } from './dispatch-to-employee-routing.module';
import { DispatchToEmployeeComponent } from './dispatch-to-employee.component';


@NgModule({
  imports: [
    CommonModule,
    DispatchToEmployeeRoutingModule,
    MatPaginatorModule,
    MatTableModule,
    MatSortModule,
    MatButtonModule,
    SharedModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,        
    MatNativeDateModule,        
    MatSelectModule,
    
  ],
  declarations: [DispatchToEmployeeComponent],
  
})
export class DispatchToEmployeeModule { }