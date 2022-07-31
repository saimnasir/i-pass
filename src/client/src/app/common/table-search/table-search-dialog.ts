import { Component, Inject, Input } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SortDirection } from "@angular/material/sort";
import { PinCodeModel } from "src/app/_model/pin-code.model";
import { PinCodeService } from "src/app/_service/pin-code.service";

@Component({
    selector: 'table-search-dialog',
    templateUrl: 'table-search-dialog.html',
    styleUrls: ['./table-search-dialog.scss']
})
export class TableSearchDialog {

    @Input() columns: string[]
    @Input() searchText: string;
    @Input() sortActive: string;
    @Input() sortDirection: string;   
    sortDirections :  SortDirection [] = [
        '',
        'asc',
        'desc'
    ];
    error: string = '';

    constructor(
        public dialogRef: MatDialogRef<TableSearchDialog>,
        public pinCodeService: PinCodeService,
    ) { }

    close(): void {
        this.dialogRef.close();
    }

    save(): void {
        let data = { searchText: this.searchText, sortActive: this.sortActive, sortDirection : this.sortDirection };
        this.dialogRef.close(data);
    }
     getSortDirectionName(direction: SortDirection| string) : string{
        if(direction == 'asc'){
            return 'Ascending';
        }
          if(direction == 'desc'){
            return 'Descending';
        }
        return 'None';
    }
    getSortDirectionIcon(direction: SortDirection| string) : string{
        if(direction == 'asc'){
            return 'north';
        }
          if(direction == 'desc'){
            return 'south';
        }
        return 'text_rotation_none';
    }
}

export interface DialogData {
    animal: string; 
    name: string;
}