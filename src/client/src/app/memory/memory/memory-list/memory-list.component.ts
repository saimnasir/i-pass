import { AfterViewInit, Component, HostListener, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MemoryModel } from 'src/app/_model/memory.model';
import { MemoryService } from 'src/app/_service/memory.service';
import { BehaviorSubject, merge, Observable, of as observableOf } from 'rxjs';
import { catchError, map, startWith, switchMap } from 'rxjs/operators';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { UnlockMemoryDialog } from '../unlock-memory-dialog/unlock-memory-dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { MediaQueryStatusComponent } from 'src/app/_components/media-query-status.component';
import { MediaChange, MediaObserver } from '@angular/flex-layout';

@Component({
  selector: 'app-memory-list',
  templateUrl: './memory-list.component.html',
  styleUrls: ['./memory-list.component.scss']
})
export class MemoryListComponent implements AfterViewInit {

  public data: MemoryModel[] = [];
  public groupDataPerCount: MemoryModel[][] = []
  dataSource!: MatTableDataSource<MemoryModel>;

  displayedColumns: string[] = ['title', 'organization', 'memoryType', 'environmentType', 'userName', 'email', 'hostOrIpAddress', 'port', 'password', 'description', 'action'];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  media$: Observable<MediaChange[]>;
  resultsLength = 0;
  isLoadingResults = true;
  isRateLimitReached = false;
  seacrchText: string;
  cardCountSubject: BehaviorSubject<number>;
  constructor(media: MediaObserver,
    private memoryService: MemoryService,
    private router: Router,
    private route: ActivatedRoute,
    public dialog: MatDialog
  ) {

    this.media$ = media.asObservable();
    this.cardCountSubject = new BehaviorSubject<number>(3);
  }
  public innerWidth: any;
  @HostListener('window:resize', ['$event'])
  onResize(event: any) {
    this.media$.subscribe(changes => {
      let change = changes.find(c => c.mqAlias.length === 2); 
      let count = 3;
      if (change?.mqAlias === 'sm') {
        count = 2;
      }
      if (change?.mqAlias === 'xs') {
        count = 1;
      }
      this.cardCountSubject = new BehaviorSubject<number>(count);
    })
  }

  public get cardCount(): number {
    return this.cardCountSubject.value;
  }

  ngAfterViewInit() {

    // If the user changes the sort order, reset back to the first page.
    this.sort.sortChange.subscribe(() => (this.paginator.pageIndex = 0));

    merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;
          return this.memoryService.getAllPaginated(
            this.memoryService.route,
            this.sort.active,
            this.sort.direction,
            this.paginator.pageIndex,
            this.paginator.pageSize,
            this.seacrchText
          ).pipe(catchError(() => observableOf(null)));
        }),
        map(response => {
          // Flip flag to show that loading has finished.
          this.isLoadingResults = false;
          this.isRateLimitReached = response === null;

          if (response === null) {
            return [];
          }
          // Only refresh the result length if there is new data. In case of rate
          // limit errors, we do not want to reset the paginator to zero, as that
          // would prevent users from re-triggering requests.
          this.resultsLength = response.data.totalCount;
          return response.data.data;
        }),
      )
      .subscribe(data => {
        this.data = data;
        this.setGroupDataPerCount;
      });
  }

  get setGroupDataPerCount() {
    let index = 0;
    for (let i = 0; i < this.data.length; i++) {
      if (this.groupDataPerCount[index] && this.groupDataPerCount[index].length === this.cardCount) {
        index++;
      }
      if (!this.groupDataPerCount[index]) {
        this.groupDataPerCount[index] = [];
      }
      this.groupDataPerCount[index].push(this.data[i]);
    }
    return this.groupDataPerCount;
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
}