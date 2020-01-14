import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CategoryRoutingModule } from '../category/category-routing.module';
import { CategoryComponent } from './category.component';
import {SharedModule} from '../../theme/shared/shared.module';
import { EditCategoryComponent } from './edit-category/edit-category.component';
import { MatTableModule, MatSortModule, MatButtonModule, MatPaginatorModule, MatFormFieldModule, MatIconModule } from '@angular/material';

@NgModule({
  imports: [
    CommonModule,
    CategoryRoutingModule,
    SharedModule,
    MatPaginatorModule,
    MatTableModule,
    MatSortModule,
    MatButtonModule,
    MatFormFieldModule,
    MatIconModule
  ],
  declarations: [CategoryComponent, EditCategoryComponent]
})
export class CategoryModule { }
