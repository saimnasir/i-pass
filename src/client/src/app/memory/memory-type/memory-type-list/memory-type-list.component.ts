import { Component, AfterViewInit, ViewChild } from '@angular/core';
import { MediaChange, MediaObserver } from '@angular/flex-layout';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { merge, Observable, of as observableOf } from 'rxjs';
import { catchError, map, startWith, switchMap } from 'rxjs/operators';
import { MemoryTypeModel } from 'src/app/_model/memory-type.model';
import { EnvironmentTypeService } from 'src/app/_service/environment-type.service';
import { MemoryTypeService } from 'src/app/_service/memory-type.service';

@Component({
  selector: 'app-memory-type-list',
  templateUrl: './memory-type-list.component.html',
  styleUrls: ['./memory-type-list.component.scss']
})
export class MemoryTypeListComponent implements AfterViewInit  {
  public data: MemoryTypeModel[] = [];

  dataSource!: MatTableDataSource<MemoryTypeModel>;

  displayedColumns: string[] = ['title', 'action'];
  sortableColumns: string[] = ['title'];

  resultsLength = 0;
  isLoadingResults = true;
  isRateLimitReached = false;
  searchText: string;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  media$: Observable<MediaChange[]>;
  constructor(media: MediaObserver,
    private memoryTypeService: MemoryTypeService
  ) {
    this.media$ = media.asObservable();
  }

  ngAfterViewInit(): void {    
    this.getData();    
  }

  getData() {

    // If the user changes the sort order, reset back to the first page.
    this.sort.sortChange.subscribe(() => (this.paginator.pageIndex = 0));

    merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;
          return this.memoryTypeService.getAllPaginated(
            this.memoryTypeService.route,
            this.sort.active,
            this.sort.direction,
            this.paginator.pageIndex,
            this.paginator.pageSize,
            this.searchText
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
}