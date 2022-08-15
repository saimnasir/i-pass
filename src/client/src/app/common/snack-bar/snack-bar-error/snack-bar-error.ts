import { Component, Inject } from "@angular/core";
import { MatSnackBarRef, MAT_SNACK_BAR_DATA } from "@angular/material/snack-bar";


@Component({
	selector: 'app-snack-bar-error',
	templateUrl: 'snack-bar-error.html',
	styles: [` 	`,],
})
export class SnackBarErrorComponent { 
	icon= 'error';
	title = 'Error';
	constructor(public snackBarRef: MatSnackBarRef<SnackBarErrorComponent>, @Inject(MAT_SNACK_BAR_DATA) public data: string) { }
}