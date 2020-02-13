import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductRoutingModule } from '../product/product-routing.module';
import { ProductComponent } from './product.component';
import {SharedModule} from '../../theme/shared/shared.module';
import { MatTableModule, MatPaginatorModule, MatIconModule, MatExpansionPanelTitle, MatExpansionModule} from '@angular/material';
import {MatSortModule} from '@angular/material/sort';
import {MatButtonModule} from '@angular/material/button';


@NgModule({
  imports: [
    CommonModule,
    ProductRoutingModule,
    MatPaginatorModule,
    MatTableModule,
    MatSortModule,
    MatButtonModule,
    SharedModule,
    MatIconModule,
    MatExpansionModule
    
  ],
  declarations: [ProductComponent],

})
export class ProductModule { }
