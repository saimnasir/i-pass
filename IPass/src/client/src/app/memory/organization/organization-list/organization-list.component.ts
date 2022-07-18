import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';  
import { OrganizationModel } from 'src/app/_model/organization.model';
import { OrganizationService } from 'src/app/_service/organization.service';

@Component({
  selector: 'app-organization-list',
  templateUrl: './organization-list.component.html',
  styleUrls: ['./organization-list.component.scss']
})
export class OrganizationListComponent implements OnInit  {
  public organizations?: Array<OrganizationModel>;

  dataSource!: MatTableDataSource<OrganizationModel>;

  displayedColumns: string[] = [ 'title','organizationType','parentOrganization', 'action'];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor(private organizationService: OrganizationService
  ) {

  }
   
  ngOnInit(): void {
    this.organizationService.getAll(this.organizationService.route).subscribe({
      next: (response) => {
        if (response.success) {
          this.organizations = response.data.data;
          console.log("response.data.data",response.data.data);
          this.dataSource = new MatTableDataSource<OrganizationModel>(response.data.data);          
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
        }
      },
      error: (e) => console.error(e),
      complete: () => console.info('complete')
    });
  }
}