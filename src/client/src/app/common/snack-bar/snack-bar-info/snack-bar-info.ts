import { Component, Inject } from "@angular/core";
import { MatSnackBarRef, MAT_SNACK_BAR_DATA } from "@angular/material/snack-bar";


@Component({
	selector: 'app-snack-bar-info',
	templateUrl: 'snack-bar-info.html',
	styles: [` 	`,],
})
export class SnackBarInfoComponent { 
	icon= 'info';
	title = 'Info';
	constructor(public snackBarRef: MatSnackBarRef<SnackBarInfoComponent>, @Inject(MAT_SNACK_BAR_DATA) public data: string) { }
}