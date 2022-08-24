
import { Injectable, OnDestroy } from '@angular/core';
import { MatSnackBar, MatSnackBarRef } from '@angular/material/snack-bar';
import { Observable, BehaviorSubject, Subject, Subscription } from 'rxjs';
import { filter } from 'rxjs/operators';
import { SnackBarComponent } from '../common/snack-bar/snack-bar';


@Injectable({ providedIn: 'root' })
export class AlertService implements OnDestroy {
    private alerts: Array<Alert> = new Array<Alert>();
    alertSubscription: Subscription;
    private snackBarRef: MatSnackBarRef<SnackBarComponent>;
    private isInstanceVisible = false;

    constructor(public snackBar: MatSnackBar) {
    }

    ngOnDestroy(): void {
        // unsubscribe to avoid memory leaks
        this.alertSubscription.unsubscribe();
    }

    success(message: string, options?: any) {
        this.add(new Alert({ ...options, alertType: AlertType.SUCCESS, message }));
    }

    error(message: string, options?: any) {
        this.add(new Alert({ ...options, alertType: AlertType.ERROR, message }));
    }

    info(message: string, options?: any) {
        this.add(new Alert({ ...options, alertType: AlertType.INFO, message }));
    }

    warn(message: string, options?: any) {
        this.add(new Alert({ ...options, alertType: AlertType.WARNING, message }));
    }

    add(alert: Alert): void {
        this.alerts.push(alert);

        if (!this.isInstanceVisible) {
            this.showNext();
        }
    }

    private showNext() {
        if (this.alerts.length === 0) {
            return;
        }

        const alert = this.alerts.shift();
        this.isInstanceVisible = true;
        if (alert) {
            let duration : number | undefined = alert.fade ? 2000: undefined;
            this.openSnackBar(alert, duration);

            this.snackBarRef.afterDismissed().subscribe(() => {
                this.isInstanceVisible = false;
                this.showNext();
            });
        }
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
        this.snackBarRef = this.snackBar.openFromComponent(SnackBarComponent, {
            data: message,
            duration: duration,
            verticalPosition: 'bottom',
            horizontalPosition: 'center',
            panelClass: 'success'
        });
    }

    openSnackBarError(message: string, duration: number | undefined = undefined) {
        this.snackBarRef = this.snackBar.openFromComponent(SnackBarComponent, {
            data: message,
            duration: duration,
            verticalPosition: 'bottom',
            horizontalPosition: 'center',
            panelClass: 'error'
        });
    }

    openSnackBarInfo(message: string, duration: number | undefined = undefined) {
        this.snackBarRef = this.snackBar.openFromComponent(SnackBarComponent, {
            data: message,
            duration: duration,
            verticalPosition: 'bottom',
            horizontalPosition: 'center',
            panelClass: 'info'
        });
    }

    openSnackBarWarning(message: string, duration: number | undefined = undefined) {
        this.snackBarRef = this.snackBar.openFromComponent(SnackBarComponent, {
            data: message,
            duration: duration,
            verticalPosition: 'bottom',
            horizontalPosition: 'center',
            panelClass: 'warning'
        });
    }
}

export class Alert {
    message: string;
    fade: boolean = true;
    alertType: AlertType;
    constructor(init?: Partial<Alert>) {
        Object.assign(this, init);
    }
}

export class AlertType {
    public static SUCCESS = "success";
    public static ERROR = "error";
    public static INFO = "info";
    public static WARNING = "warning";
}