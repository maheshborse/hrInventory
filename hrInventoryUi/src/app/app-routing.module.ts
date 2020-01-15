import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminComponent } from './theme/layout/admin/admin.component';
import {AuthComponent} from './theme/layout/auth/auth.component';

const routes: Routes = [
  {
    path: '',
    component: AdminComponent,
    children: [
      {
        path: '',
        redirectTo: 'dashboard/analytics',
        pathMatch: 'full'
      },
      {
        path: 'dashboard',
        loadChildren: () => import('./demo/dashboard/dashboard.module').then(module => module.DashboardModule)
      },
      // {
      //   path: 'layout',
      //   loadChildren: () => import('./demo/pages/layout/layout.module').then(module => module.LayoutModule)
      // },
      {
        path: 'category',
        loadChildren: () => import('./modules/category/category.module').then(module => module.CategoryModule)
      },
      {
        path: 'product',
        loadChildren: () => import('./modules/product/product.module').then(module => module.ProductModule)
      },
      // {
      //   path: 'forms',
      //   loadChildren: () => import('./demo/pages/form-elements/form-elements.module').then(module => module.FormElementsModule)
      // },
      // {
      //   path: 'tbl-bootstrap',
      //   loadChildren: () => import('./demo/pages/tables/tbl-bootstrap/tbl-bootstrap.module').then(module => module.TblBootstrapModule)
      // },
      // {
      //   path: 'charts',
      //   loadChildren: () => import('./demo/pages/core-chart/core-chart.module').then(module => module.CoreChartModule)
      // },
      // {
      //   path: 'maps',
      //   loadChildren: () => import('./demo/pages/core-maps/core-maps.module').then(module => module.CoreMapsModule)
      // },
      // {
      //   path: 'sample-page',
      //   loadChildren: () => import('./demo/pages/sample-page/sample-page.module').then(module => module.SamplePageModule)
      // }
    ]
  },
  {
    path: '',
    component: AuthComponent,
    children: [
      {
        path: 'auth',
        loadChildren: () => import('./modules/authentication/authentication.module').then(module => module.AuthenticationModule)
      },
      {
        path: 'login',
        loadChildren: () => import('./modules/authentication/auth-signin/auth-signin.module').then(module => module.AuthSigninModule)
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
