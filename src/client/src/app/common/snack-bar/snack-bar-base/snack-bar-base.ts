import { Component, Inject, Input } from "@angular/core";
import { MatSnackBarRef, MAT_SNACK_BAR_DATA } from "@angular/material/snack-bar";


@Component({
	selector: 'app-snack-bar-base',
	templateUrl: 'snack-bar-base.html',
	styles: [` 	`,],
})
export class SnackBarBaseComponent { 
	@Input() icon :string;
	@Input() title :string;
	@Input() data : any;
	@Input()  snackBarRef: MatSnackBarRef<any>
	constructor( ) { }
}