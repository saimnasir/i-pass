import { Component, AfterViewInit, ViewChild } from '@angular/core';
import { MediaChange, MediaObserver } from '@angular/flex-layout';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { merge, Observable, of as observableOf } from 'rxjs';
import { catchError, map, startWith, switchMap } from 'rxjs/operators';
import { EnvironmentTypeModel } from 'src/app/_model/environment-type.model';
import { EnvironmentTypeService } from 'src/app/_service/environment-type.service';

@Component({
  selector: 'app-environment-type-list',
  templateUrl: './environment-type-list.component.html',
  styleUrls: ['./environment-type-list.component.scss']
})
export class EnvironmentTypeListComponent implements AfterViewInit {
  public data: EnvironmentTypeModel[] = [];

  dataSource!: MatTableDataSource<EnvironmentTypeModel>;

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
    private environmentTypeService: EnvironmentTypeService
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
          return this.environmentTypeService.getAllPaginated(
            this.environmentTypeService.route,
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