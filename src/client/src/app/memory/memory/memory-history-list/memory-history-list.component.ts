import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MemoryModel } from 'src/app/_model/memory.model';
import { MemoryService } from 'src/app/_service/memory.service';
import { merge, Observable, of as observableOf } from 'rxjs';
import { catchError, map, startWith, switchMap } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';
import { MediaChange, MediaObserver } from '@angular/flex-layout';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS } from '@angular/material/form-field';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { TableSearchDialog } from 'src/app/common/table-search/table-search-dialog';

@Component({
  selector: 'app-memory-history-list',
  templateUrl: './memory-history-list.component.html',
  styleUrls: ['./memory-history-list.component.scss'],
  providers: [
    { provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: { appearance: 'outline', floatLabels: 'always' } }
  ],
})
export class MemoryHistoryListComponent implements OnInit, AfterViewInit {

  public data: MemoryModel[] = [];
  dataSource!: MatTableDataSource<MemoryModel>;
  displayedColumns: string[] = ['title', 'organization', 'memoryType', 'environmentType', 'userName', 'email', 'hostOrIpAddress', 'port', 'password', 'description'];
  sortableColumns: string[] = ['title', 'userName', 'email', 'hostOrIpAddress', 'port', 'created', 'updated'];

  openAllPanels = false;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  decode = true;
  model = new MemoryModel();


  media$: Observable<MediaChange[]>;
  resultsLength = 0;
  isLoadingResults = true;
  isRateLimitReached = false;
  searchText: string;
  id: string | null;
  constructor(media: MediaObserver,
    private memoryService: MemoryService,
    private router: Router,
    private route: ActivatedRoute,
    public dialog: MatDialog
  ) {
    this.media$ = media.asObservable();
  }

  ngOnInit(): void {

    this.getModel();
  }


  ngAfterViewInit() {
    this.getData();
  }

  getData() {
    // If the user changes the sort order, reset back to the first page.
    this.sort.sortChange.subscribe(() => (this.paginator.pageIndex = 0));
    let id = this.route.snapshot.paramMap.get('id');

    merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;
          let historyRoute = `/api/memories/history/${id}`;
          return this.memoryService.getAllPaginated(
            historyRoute,
            this.sort.active,
            this.sort.direction,
            this.paginator.pageIndex,
            this.paginator.pageSize,
            this.searchText,
            this.decode
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

          console.log("response", response);
          console.log("resultsLength", this.resultsLength);
          return response.data.data;
        }),
      )
      .subscribe(data => {
        this.data = data;
      });
  }
  getModel() {
    let id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.memoryService.readWithDecoding(this.memoryService.route, id).subscribe({
        next: (response) => {
          if (response.success) {
            this.model = response.data.data;

          }
        },
        error: (e) => console.error(e),
        complete: () => console.info('complete')
      });
    }
  }


  openSearch(enterAnimationDuration: string, exitAnimationDuration: string): void {
    let config: MatDialogConfig<any> = {
      width: '400px',
      disableClose: false,
      hasBackdrop: true,
    };
    const dialogRef = this.dialog.open(TableSearchDialog, config);
    dialogRef.componentInstance.searchText = this.searchText;
    dialogRef.componentInstance.columns = this.sortableColumns;
    dialogRef.componentInstance.sortActive = this.sort.active;
    dialogRef.componentInstance.sortDirection = this.sort.direction;
    dialogRef.afterClosed().subscribe(
      data => {
        if (data) {
          console.log("TableSearchDialog output:", data);
          this.searchText = data.searchText;
          this.sort.active = data.sortActive;
          this.sort.direction = data.sortDirection;
          this.getData();
        }
      }
    );
  }
}