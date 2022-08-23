import { Component, OnInit, OnDestroy, Input, ViewEncapsulation } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router, NavigationStart } from '@angular/router';
import { Subscription } from 'rxjs';
import { Alert, AlertService, AlertType } from 'src/app/_service/alert.service';
import { SnackBarComponent } from '../snack-bar/snack-bar';

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
    timeout = 2000;
    constructor(private router: Router, private alertService: AlertService, public snackBar: MatSnackBar) { }

    ngOnInit() {
        this.alertSubscription = this.alertService.onAlert(this.id)
            .subscribe(alert => {
                if (!alert.message) {
                    this.alerts = [];
                    return;
                }
                let timeout = 0;
                let lastExpiration = this.lastExpiration();
                let now = new Date();
                if (lastExpiration !== undefined) {
                    // console.log('lastExpiration', lastExpiration.getTime());
                    lastExpiration.setMilliseconds(lastExpiration.getMilliseconds() + this.timeout);

                    alert.expiraAt = lastExpiration;
                    console.log('alert.expiraAt date', alert.expiraAt.getTime());
                    timeout = (alert.expiraAt.getSeconds() - now.getSeconds()) * 1000;

                } else {
                    alert.expiraAt = now;
                    console.log('alert.expiraAt undefined', alert.expiraAt.getTime());
                }
                console.log('now date', now.getTime());
                console.log('timeout : ', timeout);
                alert.message = `${alert.message} , timeout ${timeout}`

                this.alerts.push(alert);
                //let timeout = alert.expiraAt.getTime() - now.getTime();
                // let timeout = this.alerts.length > 1 ? this.timeout * (this.alerts.length - 1) : 0;
                setTimeout(() => this.removeAlert(alert), timeout);
            });

        // clear alerts on location change
        this.routeSubscription = this.router.events.subscribe(event => {
            if (event instanceof NavigationStart) {
                this.alertService.clear(this.id);
            }
        });
    }

    lastExpiration(): Date | undefined {
        if (this.alerts.length > 0) {
            let alert = this.alerts.reduce((a, b) => {
                return new Date(a.expiraAt) > new Date(b.expiraAt) ? a : b;
            });
            return alert.expiraAt
        }
        return undefined;
    }

    ngOnDestroy() {
        // unsubscribe to avoid memory leaks
        this.alertSubscription.unsubscribe();
        this.routeSubscription.unsubscribe();
    }

    removeAlert(alert: Alert) {
        if (!this.alerts.includes(alert)) return;

        this.alerts = this.alerts.filter(x => x !== alert);
        this.openSnackBar(alert, this.timeout);

    }
 

    openSnackBar(alert: Alert, duration: number | undefined = undefined) {
		switch (alert.alertType) {
			case AlertType.SUCCESS:
				this.openSnackBarSuccess(alert.message, duration);
				break;
			case AlertType.ERROR:
				this.openSnackBarError(alert.message, duration);
				break;
			case AlertType.WARNING:
				this.openSnackBarWarning(alert.message, duration);
				break;
			default:
				this.openSnackBarInfo(alert.message, duration);
				break;
		}
	}

    
	openSnackBarSuccess(message: string, duration: number | undefined = undefined) {
		this.snackBar.openFromComponent(SnackBarComponent, {
			data: message,
			duration: duration,
			verticalPosition: 'bottom',
			horizontalPosition: 'center',
			panelClass: 'success'
		});
	}

	openSnackBarError(message: string, duration: number | undefined = undefined) {
		this.snackBar.openFromComponent(SnackBarComponent, {
			data: message,
			duration: duration,
			verticalPosition: 'bottom',
			horizontalPosition: 'center',
			panelClass: 'error'
		});
	}

	openSnackBarInfo(message: string, duration: number | undefined = undefined) {
		this.snackBar.openFromComponent(SnackBarComponent, {
			data: message,
			duration: duration,
			verticalPosition: 'bottom',
			horizontalPosition: 'center',
			panelClass: 'info'			
		});
	}

	openSnackBarWarning(message: string, duration: number | undefined = undefined) {
		this.snackBar.openFromComponent(SnackBarComponent, {
			data: message,
			duration: duration,
			verticalPosition: 'bottom',
			horizontalPosition: 'center',
			panelClass: 'warning'
		});
	}
}