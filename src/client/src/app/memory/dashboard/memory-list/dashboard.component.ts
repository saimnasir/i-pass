import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';  
import { MemoryModel } from 'src/app/_model/memory.model'; 
import { MemoryService } from 'src/app/_service/memory.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardListComponent implements OnInit  {
  public memories?: Array<MemoryModel>;

  dataSource!: MatTableDataSource<MemoryModel>;

  displayedColumns: string[] = [ 'title','organization','memoryType', 'environmentType',  'userName', 'email','hostOrIpAddress', 'port',  'password', 'description'];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor(public memoryService: MemoryService 
  ) { 
  }
   
  ngOnInit(): void {  
    // this.memoryService.getAll(this.memoryService.route).subscribe({
    //   next: (response) => {
    //     if (response.success) {
    //       this.memories = response.data.data;
    //       console.log("response.data.data",response.data.data);
    //       this.dataSource = new MatTableDataSource<MemoryModel>(response.data.data);          
    //       this.dataSource.paginator = this.paginator;
    //       this.dataSource.sort = this.sort;
    //     }
    //   },
    //   error: (e) => console.error(e),
    //   complete: () => console.info('complete')
    // });
  } 
}