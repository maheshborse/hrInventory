import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DispatchToEmployeeComponent } from './dispatch-to-employee.component';


const routes: Routes = [
  {
    path: '',
    component: DispatchToEmployeeComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DispatchToEmployeeRoutingModule { }