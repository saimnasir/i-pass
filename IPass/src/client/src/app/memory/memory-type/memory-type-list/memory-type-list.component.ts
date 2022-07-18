import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';  
import { MemoryTypeModel } from 'src/app/_model/memory-type.model';
import { MemoryModel } from 'src/app/_model/memory.model'; 
import { MemoryTypeService } from 'src/app/_service/memory-type.service';
import { MemoryService } from 'src/app/_service/memory.service';

@Component({
  selector: 'app-memory-type-list',
  templateUrl: './memory-type-list.component.html',
  styleUrls: ['./memory-type-list.component.scss']
})
export class MemoryTypeListComponent implements OnInit  {
  public memoryTypes?: Array<MemoryTypeModel>;

  dataSource!: MatTableDataSource<MemoryTypeModel>;

  displayedColumns: string[] = [ 'title','action'];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor(private memoryTypeService: MemoryTypeService
  ) {

  }
   
  ngOnInit(): void {
    this.memoryTypeService.getAll(this.memoryTypeService.route).subscribe({
      next: (response) => {
        if (response.success) {
          this.memoryTypes = response.data.data;
          console.log("response.data.data",response.data.data);
          this.dataSource = new MatTableDataSource<MemoryTypeModel>(response.data.data);          
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
        }
      },
      error: (e) => console.error(e),
      complete: () => console.info('complete')
    });
  }
}