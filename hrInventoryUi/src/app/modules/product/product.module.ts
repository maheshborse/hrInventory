import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductRoutingModule } from '../product/product-routing.module';
import { ProductComponent } from './product.component';
import {SharedModule} from '../../theme/shared/shared.module';
import { MatTableModule, MatPaginatorModule} from '@angular/material';
import {MatSortModule} from '@angular/material/sort';
import {MatButtonModule} from '@angular/material/button';
import { EditProductComponent } from './edit-product/edit-product/edit-product.component';

@NgModule({
  imports: [
    CommonModule,
    ProductRoutingModule,
    MatPaginatorModule,
    MatTableModule,
    MatSortModule,
    MatButtonModule,
    SharedModule
  ],
  declarations: [ProductComponent],

})
export class ProductModule { }
