import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';  
import { EnvironmentTypeModel } from 'src/app/_model/environment-type.model'; 
import { EnvironmentTypeService } from 'src/app/_service/environment-type.service'; 

@Component({
  selector: 'app-environment-type-list',
  templateUrl: './environment-type-list.component.html',
  styleUrls: ['./environment-type-list.component.scss']
})
export class EnvironmentTypeListComponent implements OnInit  {
  public environmentTypes?: Array<EnvironmentTypeModel>;

  dataSource!: MatTableDataSource<EnvironmentTypeModel>;

  displayedColumns: string[] = [ 'title','action'];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor(private environmentTypeService: EnvironmentTypeService
  ) {

  }
   
  ngOnInit(): void {
    this.environmentTypeService.getAll(this.environmentTypeService.route).subscribe({
      next: (response) => {
        if (response.success) {
          this.environmentTypes = response.data.data;
          console.log("response.data.data",response.data.data);
          this.dataSource = new MatTableDataSource<EnvironmentTypeModel>(response.data.data);          
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
        }
      },
      error: (e) => console.error(e),
      complete: () => console.info('complete')
    });
  }
 
}