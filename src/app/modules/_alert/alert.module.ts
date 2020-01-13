import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AlertComponent, SnackbarComponent } from './alert.component';

@NgModule({
    imports: [CommonModule],
    declarations: [AlertComponent,SnackbarComponent],
    exports: [AlertComponent,SnackbarComponent],
    entryComponents:[]

})
export class AlertModule { }