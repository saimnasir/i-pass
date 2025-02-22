import {  Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MemoryModel } from 'src/app/_model/memory.model'; 
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { UnlockMemoryDialog } from '../unlock-memory-dialog/unlock-memory-dialog';
import { ActivatedRoute, Router } from '@angular/router'; 
import { TableSearchDialog } from 'src/app/common/table-search/table-search-dialog';

@Component({
  selector: 'app-memory-card-list',
  templateUrl: './memory-card-list.component.html',
  styleUrls: ['./memory-card-list.component.scss']
})
export class MemoryCardListComponent implements OnInit {
 
  @Input() memories: MemoryModel[] = [] 
  @Input() decode = false;
  @Input() showAdd = true;
  
  @Input()  openAllPanels = false;
  @Input() displayedColumns: string[]
  @Input() searchText: string;
  @Input() sort: MatSort;

  constructor(  
    private router: Router,
    private route: ActivatedRoute,
    public dialog: MatDialog
  ) {
 
  } 

  ngOnInit(): void {
     
  } 

  openDialog(id: string, action: string, enterAnimationDuration: string, exitAnimationDuration: string): void {
    let config: MatDialogConfig<any> = {
      width: '400px',
      disableClose: true,
      hasBackdrop: true,
    };
    const dialogRef = this.dialog.open(UnlockMemoryDialog, config);
    dialogRef.afterClosed().subscribe(
      data => {
        console.log("Dialog output:", data);
        if (data.active) {
          this.router.navigate([`${action}/${id}`], { relativeTo: this.route });
        }
      }
    );
  }

  
  openSearch( enterAnimationDuration: string, exitAnimationDuration: string): void {
    let config: MatDialogConfig<any> = {
      width: '400px',
      disableClose: false,
      hasBackdrop: true,
    };
    const dialogRef = this.dialog.open(TableSearchDialog, config);
    dialogRef.componentInstance.searchText = this.searchText;
    dialogRef.componentInstance.columns = this.displayedColumns;
    dialogRef.componentInstance.sortActive = this.sort.active;
    dialogRef.afterClosed().subscribe(
      data => {
        console.log("TableSearchDialog output:", data);
        
      }
    );
  }
}