import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CategoryRoutingModule } from '../category/category-routing.module';
import { CategoryComponent } from './category.component';
import {SharedModule} from '../../theme/shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    CategoryRoutingModule,
    SharedModule
  ],
  declarations: [CategoryComponent]
})
export class CategoryModule { }
