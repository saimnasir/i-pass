import { Component, Inject } from '@angular/core';
import { MatSnackBar, MatSnackBarRef, MAT_SNACK_BAR_DATA } from '@angular/material/snack-bar';

@Component({
	selector: 'app-mat-snack-bar',
	template: '',
	styleUrls: ['./mat-snack-bar.component.css']
})
export class MatSnackBarComponent {
	duration = 2000;
	constructor(public snackBar: MatSnackBar) { }

	openSnackBar(message: string, className: string, duration: number | undefined = undefined) {
		this.snackBar.openFromComponent(SnackerComponent, {
			data: message,
			duration: duration,
			verticalPosition: 'bottom',
			horizontalPosition: 'center',
			panelClass: [className],
		});
	}

	openSnackBars(messages: string[], className: string) {
		messages.forEach((message, index) => {
			setTimeout(() => {
				this.openSnackBar(message, className, this.duration);
			}, index * (this.duration + 500)); // 500 - timeout between two messages
		});
	}
}

@Component({
	selector: 'app-snack-bar',
	templateUrl: 'snack-bar-component-example-snack.html',
	styles: [` 	`,],
})
export class SnackerComponent {
	constructor(public snackBarRef: MatSnackBarRef<SnackerComponent>, @Inject(MAT_SNACK_BAR_DATA) public data: string) { }
}