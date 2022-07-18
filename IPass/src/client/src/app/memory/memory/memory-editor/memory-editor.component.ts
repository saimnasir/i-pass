import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { Guid } from 'guid-typescript'; 
import { EnvironmentTypeModel } from 'src/app/_model/environment-type.model';
import { MemoryTypeModel } from 'src/app/_model/memory-type.model';
import { MemoryModel } from 'src/app/_model/memory.model';
import { OrganizationModel } from 'src/app/_model/organization.model';
import { EnvironmentTypeService } from 'src/app/_service/environment-type.service';
import { MemoryTypeService } from 'src/app/_service/memory-type.service';
import { OrganizationService } from 'src/app/_service/organization.service';
import { MemoryService } from '../../../_service/memory.service';

@Component({
  selector: 'app-memory-editor',
  templateUrl: './memory-editor.component.html',
  styleUrls: ['./memory-editor.component.css']
})
export class MemoryEditorComponent implements OnInit {

  model = new MemoryModel();
  organizations: Array<OrganizationModel>;
  memoryTypes: Array<MemoryTypeModel>;
  environmentTypes: Array<EnvironmentTypeModel>;
  title : string;
  isNew = false;
  isRead = false;
  constructor(private memoryService: MemoryService, 
    private organizationService: OrganizationService,
    private memoryTypeService: MemoryTypeService,
    private environmentTypeService: EnvironmentTypeService,   
    private route: ActivatedRoute,
    protected router: Router) { }


  ngOnInit(): void {
 
    this.loadOrganizations();
    this.loadMemoryTypes();
    this.loadEnvironmentTypes();
    this.getModel();
  }

  loadOrganizations() {
    this.organizationService.getAll(this.organizationService.route).subscribe({
      next: (response) => {
        if (response.success) {
          this.organizations = response.data.data;
          this.title = this.model.title;
        }
      },
      error: (e) => console.error(e),
      complete: () => console.info('complete')
    });
  }

  loadMemoryTypes() {
    this.memoryTypeService.getAll(this.memoryTypeService.route).subscribe({
      next: (response) => {
        if (response.success) {
          this.memoryTypes = response.data.data;
        }
      },
      error: (e) => console.error(e),
      complete: () => console.info('complete')
    });
  }

  loadEnvironmentTypes() {
    this.environmentTypeService.getAll(this.environmentTypeService.route).subscribe({
      next: (response) => {
        if (response.success) {
          this.environmentTypes = response.data.data;
        }
      },
      error: (e) => console.error(e),
      complete: () => console.info('complete')
    });
  }

  getModel() {
    let id = this.route.snapshot.paramMap.get('id');
    this.isNew = id == null;
    this.isRead = this.route.snapshot.url[1].toString() ==="read";
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

  save() {
    if (this.isNew) {
      this.memoryService.create(this.memoryService.route, this.model).subscribe({
        next: (response) => {
          if (response.success) {
            console.info('created');
            this.router.navigate(['memories']);
          }
        },
        error: (e) => console.error(e),
        complete: () => console.info('complete')
      });
    } else {
      this.memoryService.update(this.memoryService.route, this.model).subscribe({
        next: (response) => {
          if (response.success) {
            console.info('updated');
            this.router.navigate(['memories']);
          }
        },
        error: (e) => console.error(e),
        complete: () => console.info('complete')
      });
    }
  }

}
