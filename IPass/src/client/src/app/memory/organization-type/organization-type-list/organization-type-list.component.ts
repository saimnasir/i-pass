import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';   
import { OrganizationTypeModel } from 'src/app/_model/organization-type.model'; 
import { OrganizationTypeService } from 'src/app/_service/organization-type.service';

@Component({
  selector: 'app-organization-type-list',
  templateUrl: './organization-type-list.component.html',
  styleUrls: ['./organization-type-list.component.scss']
})
export class OrganizationTypeListComponent implements OnInit  {
  public organizationTypes?: Array<OrganizationTypeModel>;

  dataSource!: MatTableDataSource<OrganizationTypeModel>;

  displayedColumns: string[] = [ 'title', 'action'];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor(private organizationTypeService: OrganizationTypeService
  ) {

  }
   
  ngOnInit(): void {
    this.organizationTypeService.getAll(this.organizationTypeService.route).subscribe({
      next: (response) => {
        if (response.success) {
          this.organizationTypes = response.data.data;
          console.log("response.data.data",response.data.data);
          this.dataSource = new MatTableDataSource<OrganizationTypeModel>(response.data.data);          
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
        }
      },
      error: (e) => console.error(e),
      complete: () => console.info('complete')
    });
  } 
}