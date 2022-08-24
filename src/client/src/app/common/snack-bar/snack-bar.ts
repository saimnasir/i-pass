import { Component, Inject, Input } from "@angular/core";
import { ProgressBarMode } from "@angular/material/progress-bar";
import { MatSnackBarRef, MAT_SNACK_BAR_DATA } from "@angular/material/snack-bar";
import { AlertType } from "src/app/_service/alert.service";


@Component({
	selector: 'app-snack-bar',
	templateUrl: 'snack-bar.html',
	styles: [` 	`,],
})
export class SnackBarComponent {
	snackType: any;

	progress = 100;
	mode: ProgressBarMode;
	private currentIntervalId: any;
	constructor(@Inject(MAT_SNACK_BAR_DATA) public data: any,
		public snackBarRef: MatSnackBarRef<SnackBarComponent>) {
		this.snackBarRef.afterOpened().subscribe(
			() => {
				const duration = this.snackBarRef.containerInstance.snackBarConfig.duration;
				this.snackType = this.snackBarRef.containerInstance.snackBarConfig.panelClass;
				this.mode =  duration ? 'buffer' : 'query';
				this.runProgressBar(duration ?? 2000);
			},
			error => console.error(error)
		);
	}

	dismissWithAction() {
		this.cleanProgressBarInterval();
		this.snackBarRef.dismissWithAction();
	}

	/**
	 * @param duration - in milliseconds
	 */
	runProgressBar(duration: number) {
		this.progress = 100;
		const step = 0.005;
		this.cleanProgressBarInterval();
		this.currentIntervalId = setInterval(() => {
			this.progress -= 100 * step;
			if (this.progress < 0) {
				this.cleanProgressBarInterval();
			}
		}, duration * step);
	}

	cleanProgressBarInterval() {
		clearInterval(this.currentIntervalId);
	}

 
	
	get Title() {
		switch (this.snackType) {
			case AlertType.ERROR: return 'Error';
			case AlertType.WARNING: return 'Warning';
			case AlertType.SUCCESS: return 'Success';
			default: return 'Info';
		}
	}

	get Icon() {
		switch (this.snackType) {
			case AlertType.ERROR: return 'error';
			case AlertType.WARNING: return 'warning';
			case AlertType.SUCCESS: return 'check_circle';
			default: return 'info';
		}
	}
} 