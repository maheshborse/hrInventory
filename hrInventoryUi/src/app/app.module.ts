import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { SharedModule } from './theme/shared/shared.module';

import { AppComponent } from './app.component';
import { AdminComponent } from './theme/layout/admin/admin.component';
import { AuthComponent } from './theme/layout/auth/auth.component';
import { NavigationComponent } from './theme/layout/admin/navigation/navigation.component';
import { NavContentComponent } from './theme/layout/admin/navigation/nav-content/nav-content.component';
import { NavGroupComponent } from './theme/layout/admin/navigation/nav-content/nav-group/nav-group.component';
import { NavCollapseComponent } from './theme/layout/admin/navigation/nav-content/nav-collapse/nav-collapse.component';
import { NavItemComponent } from './theme/layout/admin/navigation/nav-content/nav-item/nav-item.component';
import { NavBarComponent } from './theme/layout/admin/nav-bar/nav-bar.component';
import { NavLeftComponent } from './theme/layout/admin/nav-bar/nav-left/nav-left.component';
import { NavSearchComponent } from './theme/layout/admin/nav-bar/nav-left/nav-search/nav-search.component';
import { NavRightComponent } from './theme/layout/admin/nav-bar/nav-right/nav-right.component';
import {ChatUserListComponent} from './theme/layout/admin/nav-bar/nav-right/chat-user-list/chat-user-list.component';
import {FriendComponent} from './theme/layout/admin/nav-bar/nav-right/chat-user-list/friend/friend.component';
import {ChatMsgComponent} from './theme/layout/admin/nav-bar/nav-right/chat-msg/chat-msg.component';
import { ConfigurationComponent } from './theme/layout/admin/configuration/configuration.component';

import { ToggleFullScreenDirective } from './theme/shared/full-screen/toggle-full-screen';

/* Menu Items */
import { NavigationItem } from './theme/layout/admin/navigation/navigation';
import { NgbButtonsModule, NgbDropdownModule, NgbTabsetModule, NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AlertModule } from './modules/_alert/alert.module';
import { SnackbarComponent } from './modules/_alert/alert.component';
import { DemoMaterialModule } from './demo-material-module';
import { EditProductComponent } from './modules/product/edit-product/edit-product/edit-product.component';
import { EditCategoryComponent } from './modules/category/edit-category/edit-category.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ProductCategoryService } from './shared/services/product-category.service';
import { JwtInterceptor } from './shared/helpers/jwt.interceptor';
import { AuthenticationService } from './shared/services/authentication.service';
import { ProductService } from './shared/services/product.service';
import { EditRequestComponent } from './modules/request/edit-request/edit-request.component';
import { MatSlideToggleModule, MatRadioModule } from '@angular/material';
import { ShowDispatchInfoComponent } from './modules/dispatch-to-employee/show-dispatch-info/show-dispatch-info.component';

@NgModule({
  declarations: [
    AppComponent,
    AdminComponent,
    AuthComponent,
    NavigationComponent,
    NavContentComponent,
    NavGroupComponent,
    NavCollapseComponent,
    NavItemComponent,
    NavBarComponent,
    NavLeftComponent,
    NavSearchComponent,
    NavRightComponent,
    ChatUserListComponent,
    FriendComponent,
    ChatMsgComponent,
    ConfigurationComponent,
    ToggleFullScreenDirective,
    EditProductComponent,EditCategoryComponent, EditRequestComponent, ShowDispatchInfoComponent
   
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    SharedModule,
    NgbDropdownModule,
    NgbTooltipModule,
    NgbButtonsModule,
    NgbTabsetModule,
    FormsModule,
    DemoMaterialModule,
    AlertModule,
    HttpClientModule,
    ReactiveFormsModule,
    MatRadioModule,
    MatSlideToggleModule,
  ],
  providers: [NavigationItem,ProductCategoryService,ProductService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    },
    AuthenticationService
  ],
  
  bootstrap: [AppComponent],
  entryComponents:[SnackbarComponent,EditProductComponent,EditCategoryComponent,EditRequestComponent, ShowDispatchInfoComponent]
  
})
export class AppModule { }
