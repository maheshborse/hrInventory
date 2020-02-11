import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {SharedModule} from '../../theme/shared/shared.module';
import { MatTableModule, MatSortModule, MatButtonModule, MatPaginatorModule, MatFormFieldModule,  MatDialogModule, MatIconModule, MatFormFieldControl, MatInputModule, MatDatepickerModule, MatNativeDateModule, MatSelectModule } from '@angular/material';
import { RequestComponent } from './request.component';
import { RequestRoutingModule } from './request-routing.module';
import { EditRequestComponent } from './edit-request/edit-request.component';


@NgModule({
  imports: [
    CommonModule,
    MatPaginatorModule,
    RequestRoutingModule,
    MatTableModule,
    MatSortModule,
    MatButtonModule,
    SharedModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,        
    MatNativeDateModule,        
    MatSelectModule
  ],
  declarations: [RequestComponent],
})
export class RequestModule { }