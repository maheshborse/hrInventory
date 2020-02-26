import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { Subscription } from 'rxjs';
import { MatSnackBar } from '@angular/material';
import { Alert, AlertType } from 'src/app/shared/models/alert';
import { NotificationService } from 'src/app/shared/services/notification.service';

@Component({ selector: 'alert', templateUrl: 'alert.component.html' })
export class AlertComponent implements OnInit, OnDestroy {
    @Input() id: string;

    alerts: Alert[] = [];
    subscription: Subscription;

    constructor(private alertService: NotificationService,public snackbar:MatSnackBar) { }

    ngOnInit() {
        this.subscription = this.alertService.onAlert(this.id)
            .subscribe(alert => {
                if (!alert.message) {
                    // clear alerts when an empty alert is received
                    this.alerts = [];
                    return;
                }

                let panelclass=this.cssClass(alert)
                // add alert to array
                this.alerts.push(alert);
                this.snackbar.open(alert.message,"",{
                    panelClass: panelclass,
                    duration:2000,
                    verticalPosition:"top",
                    horizontalPosition:"center"
                })
            });
    }

    cssClass(alert: Alert) {
        if (!alert) {
            return;
        }

        // return css class based on alert type
        switch (alert.type) {
            case AlertType.Success:
                return 'snackbar-alert-success';
            case AlertType.Error:
                return 'snackbar-alert-danger';
            case AlertType.Info:
                return 'snackbar-alert-info';
            case AlertType.Warning:
                return 'snackbar-alert-warning';
        }
    }

    ngOnDestroy() {
        // unsubscribe to avoid memory leaks
        this.subscription.unsubscribe();
    }

   
}

// @Component({
//     selector: 'snack-bar-component-example-snack',
//     template: '<div *ngFor="let alert of alerts" class="{cssClass(alert)}}">{{alert.message}}</div>',
//     styles: [`
//       .example-pizza-party {
//         color: hotpink;
//       }
//     `],
//   })
//   export class SnackbarComponent {

//   }