import { Component, OnInit, OnDestroy, Input, ViewEncapsulation } from '@angular/core';
import { Router, NavigationStart } from '@angular/router';
import { Subscription } from 'rxjs';
import { Alert, AlertService } from 'src/app/_service/alert.service';
import { MatSnackBarComponent } from '../snack-bar/mat-snack-bar.component';

@Component({
    selector: 'alert',
    templateUrl: 'alert.component.html',
    styleUrls: ['./alert.component.css'],
    encapsulation: ViewEncapsulation.None
})
export class AlertComponent implements OnInit, OnDestroy {
    @Input() id = 'default-alert'; 

    alerts: Alert[] = [];
    alertSubscription: Subscription;
    routeSubscription: Subscription;
    timeout = 4000;
    constructor(private router: Router, private alertService: AlertService, private snackBarComponent: MatSnackBarComponent) { }

    ngOnInit() {
        this.alertSubscription = this.alertService.onAlert(this.id)
            .subscribe(alert => {
                if (!alert.message) {
                    this.alerts = [];
                    return;
                }

                this.alerts.push(alert);
                let timeout = this.alerts.length > 1 ? this.timeout * (this.alerts.length - 1) : 0;
                // console.log('alertSubscription.alerts.leng', { count: this.alerts.length, timeout });
                setTimeout(() => this.removeAlert(alert), timeout);
            });

        // clear alerts on location change
        this.routeSubscription = this.router.events.subscribe(event => {
            if (event instanceof NavigationStart) {
                this.alertService.clear(this.id);
            }
        });
    }

    ngOnDestroy() {
        // unsubscribe to avoid memory leaks
        this.alertSubscription.unsubscribe();
        this.routeSubscription.unsubscribe();
    }

    removeAlert(alert: Alert) {
        if (!this.alerts.includes(alert)) return;

        if (alert.fade) {
            this.alerts = this.alerts.filter(x => x !== alert);
            this.openSnackBar(alert, this.timeout);
        } else {
            this.alerts = this.alerts.filter(x => x !== alert);
            this.openSnackBar(alert);
        }
    }

    openSnackBar(alert: Alert, duration: number | undefined = undefined) {         
        this.snackBarComponent.openSnackBar(alert, duration);
    }
}