import { Component, Inject } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PinCodeModel } from "src/app/_model/pin-code.model";
import { PinCodeService } from "src/app/_service/pin-code.service";

@Component({
    selector: 'unlock-memory-dialog',
    templateUrl: 'unlock-memory-dialog.html',
    styleUrls: ['./unlock-memory-dialog.scss']
})
export class UnlockMemoryDialog {

    model = new PinCodeModel();
    error: string = '';

    constructor(
        public dialogRef: MatDialogRef<UnlockMemoryDialog>,
        public pinCodeService: PinCodeService,
    ) { }

    close(): void {
        this.dialogRef.close();
    }

    save(): void {
        this.error = '';
        this.pinCodeService.getPinCode(this.model.code).subscribe({
            next: (response) => {
                if (response.success) {
                    this.model = response.data.data;
                    this.dialogRef.close(this.model);
                }
                else {
                    this.error = response.message;
                }
            },
            error: (e) => {
                console.error(e);
                this.error = e;
            },
            complete: () => console.info('complete')
        });

    }
}

export interface DialogData {
    animal: string;
    name: string;
}