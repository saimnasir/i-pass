import { Injectable } from '@angular/core';
import { MatSnackBarComponent } from '../common/snack-bar/mat-snack-bar.component';


@Injectable({ providedIn: 'root' })
export class AlertService {
    constructor(private snackBar: MatSnackBarComponent) {
    }

    // convenience methods
    success(message: string ) {
        this.snackBar.openSnackBar(message,  'success');
    } 
    // convenience methods
    successMulti(messages: Array<string> ) {
        this.snackBar.openSnackBars(messages,  'success');
    }

    error(message: string) {
        this.snackBar.openSnackBar(message, 'error');
    }

    info(message: string) {

        this.snackBar.openSnackBar(message, 'info');
    }

    warn(message: string) {
        this.snackBar.openSnackBar(message,  'warning');
    }
}