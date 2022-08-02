import { Component, Input, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { MemoryModel } from 'src/app/_model/memory.model';
import { UnlockMemoryDialog } from '../unlock-memory-dialog/unlock-memory-dialog';

@Component({
  selector: 'app-memory-card',
  templateUrl: './memory-card.component.html',
  styleUrls: ['./memory-card.component.css']
})
export class MemoryCardComponent implements OnInit {
  @Input() panelOpenState = false;
  @Input() memory: MemoryModel;
  @Input() decode = false;
  @Input() index : number;
  constructor(public dialog: MatDialog,    
    private router: Router,
    private route: ActivatedRoute,) { }

  ngOnInit(): void {
  }

  openDialog(id: string, action: string, enterAnimationDuration: string, exitAnimationDuration: string): void {
    let config: any = {
      width: '400px',
      enterAnimationDuration,
      exitAnimationDuration,
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
}
