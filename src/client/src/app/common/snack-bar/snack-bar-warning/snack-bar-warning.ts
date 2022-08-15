import { Component, Inject } from "@angular/core";
import { MatSnackBarRef, MAT_SNACK_BAR_DATA } from "@angular/material/snack-bar";


@Component({
	selector: 'app-snack-bar-warning',
	templateUrl: 'snack-bar-warning.html',
	styles: [` 	`,],
})
export class SnackBarWarningComponent { 
	icon= 'warning';
	title = 'warning';
	constructor(public snackBarRef: MatSnackBarRef<SnackBarWarningComponent>, @Inject(MAT_SNACK_BAR_DATA) public data: string) { }
}