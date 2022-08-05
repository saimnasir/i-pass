import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MediaChange, MediaObserver } from '@angular/flex-layout';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { merge, Observable, of as observableOf } from 'rxjs';
import { catchError, map, startWith, switchMap } from 'rxjs/operators';
import { OrganizationModel } from 'src/app/_model/organization.model';
import { OrganizationService } from 'src/app/_service/organization.service';

@Component({
  selector: 'app-organization-list',
  templateUrl: './organization-list.component.html',
  styleUrls: ['./organization-list.component.scss']
})
export class OrganizationListComponent implements AfterViewInit {

  public data: OrganizationModel[] = [];
  dataSource!: MatTableDataSource<OrganizationModel>;

  displayedColumns: string[] = ['title', 'organizationType', 'parentOrganization', 'action'];
  sortableColumns: string[] = ['title', 'parentOrganization'];

  resultsLength = 0;
  isLoadingResults = true;
  isRateLimitReached = false;
  searchText: string;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  media$: Observable<MediaChange[]>;
  constructor(media: MediaObserver,
    private organizationService: OrganizationService
  ) {

    this.media$ = media.asObservable();
  }

  ngAfterViewInit() { 
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
          return this.organizationService.getAllPaginated(
            this.organizationService.route,
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