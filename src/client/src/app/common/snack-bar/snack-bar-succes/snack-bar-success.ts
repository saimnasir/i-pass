import { Component, Inject } from "@angular/core";
import { MatSnackBarRef, MAT_SNACK_BAR_DATA } from "@angular/material/snack-bar";


@Component({
	selector: 'app-snack-bar-success',
	templateUrl: 'snack-bar-success.html',
	styles: [` 	`,],
})
export class SnackBarSuccessComponent { 
	icon= 'check_circle';
	title = 'Success';
	constructor(public snackBarRef: MatSnackBarRef<SnackBarSuccessComponent>, @Inject(MAT_SNACK_BAR_DATA) public data: string) { }
}