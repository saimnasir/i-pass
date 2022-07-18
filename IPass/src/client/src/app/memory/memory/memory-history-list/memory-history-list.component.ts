import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MemoryModel } from 'src/app/_model/memory.model';
import { MemoryService } from 'src/app/_service/memory.service';
import { merge, of as observableOf } from 'rxjs';
import { catchError, map, startWith, switchMap } from 'rxjs/operators';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { UnlockMemoryDialog } from '../unlock-memory-dialog/unlock-memory-dialog';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-memory-history-list',
  templateUrl: './memory-history-list.component.html',
  styleUrls: ['./memory-history-list.component.scss']
})
export class MemoryHistoryListComponent implements OnInit,  AfterViewInit {

  public data: MemoryModel[] = [];

  model = new MemoryModel();
  dataSource!: MatTableDataSource<MemoryModel>;

  displayedColumns: string[] = ['title', 'organization', 'memoryType', 'environmentType', 'userName', 'email', 'hostOrIpAddress', 'port', 'password', 'description'];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  resultsLength = 0;
  isLoadingResults = true;
  isRateLimitReached = false;
  seacrchText: string;
  id: string | null;
  constructor(private memoryService: MemoryService,
    private router: Router,
    private route: ActivatedRoute,
    public dialog: MatDialog
  ) {

  }

  ngOnInit(): void {
  
    this.getModel();
  }


  ngAfterViewInit() {

    // If the user changes the sort order, reset back to the first page.
    this.sort.sortChange.subscribe(() => (this.paginator.pageIndex = 0)); 
    let id = this.route.snapshot.paramMap.get('id'); 
    merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;
          let historyRoute = `/api/memories/history/${id}`;
          return this.memoryService.getHistoryPaginated(
            historyRoute,
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
        //this.dataSource = new MatTableDataSource<MemoryModel>(data);
        // this.dataSource.paginator = this.paginator;
        // this.dataSource.sort = this.sort;
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

}