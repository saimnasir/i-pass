import { Component, Inject, Input } from '@angular/core';
import { MatSnackBar, MatSnackBarRef, MAT_SNACK_BAR_DATA } from '@angular/material/snack-bar';
import { Alert, AlertSettings } from 'src/app/_service/alert.service';
import { SnackBarErrorComponent } from './snack-bar-error/snack-bar-error';
import { SnackBarInfoComponent } from './snack-bar-info/snack-bar-info';
import { SnackBarSuccessComponent } from './snack-bar-succes/snack-bar-success';
import { SnackBarWarningComponent } from './snack-bar-warning/snack-bar-warning';

@Component({
	selector: 'app-mat-snack-bar',
	template: '',
	styleUrls: ['./mat-snack-bar.component.css']
})
export class MatSnackBarComponent {
	duration = 2000;
	constructor(public snackBar: MatSnackBar) { }

	openSnackBar(alert: Alert, duration: number | undefined = undefined) {
		switch (alert.alertType) {
			case AlertSettings.SUCCESS:
				this.openSnackBarSuccess(alert.message, duration);
				break;
			case AlertSettings.ERROR:
				this.openSnackBarError(alert.message, duration);
				break;
			case AlertSettings.WARNING:
				this.openSnackBarWarning(alert.message, duration);
				break;
			default:
				this.openSnackBarInfo(alert.message, duration);
				break;
		}
	}

	openSnackBarSuccess(message: string, duration: number | undefined = undefined) {
		this.snackBar.openFromComponent(SnackBarSuccessComponent, {
			data: message,
			duration: duration,
			verticalPosition: 'bottom',
			horizontalPosition: 'center',
			panelClass: ['success'],
		});
	}

	openSnackBarError(message: string, duration: number | undefined = undefined) {
		this.snackBar.openFromComponent(SnackBarErrorComponent, {
			data: message,
			duration: duration,
			verticalPosition: 'bottom',
			horizontalPosition: 'center',
			panelClass: ['error'],
		});
	}

	openSnackBarInfo(message: string, duration: number | undefined = undefined) {
		this.snackBar.openFromComponent(SnackBarInfoComponent, {
			data: message,
			duration: duration,
			verticalPosition: 'bottom',
			horizontalPosition: 'center',
			panelClass: ['info'],
		});
	}

	openSnackBarWarning(message: string, duration: number | undefined = undefined) {
		this.snackBar.openFromComponent(SnackBarWarningComponent, {
			data: message,
			duration: duration,
			verticalPosition: 'bottom',
			horizontalPosition: 'center',
			panelClass: ['warning'],
		});
	}
} 