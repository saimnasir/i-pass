import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { OrganizationTypeModel } from 'src/app/_model/organization-type.model';
import { OrganizationModel } from 'src/app/_model/organization.model';
import { OrganizationTypeService } from 'src/app/_service/organization-type.service';
import { OrganizationService } from 'src/app/_service/organization.service';

@Component({
  selector: 'app-organization-editor',
  templateUrl: './organization-editor.component.html',
  styleUrls: ['./organization-editor.component.css']
})
export class OrganizationEditorComponent implements OnInit {

  model = new OrganizationModel();
  organizationTypes: Array<OrganizationTypeModel>;
  isNew = false;
  title :string;
  constructor(private organizationService: OrganizationService,
    private organizationTypeService: OrganizationTypeService, 
    private route: ActivatedRoute,
    protected router: Router) { }


  ngOnInit(): void {

    this.loadOrganizationTypes();
    this.getModel();
  }

  loadOrganizationTypes() {  
      this.organizationTypeService.getAll(this.organizationTypeService.route).subscribe({
        next: (response) => {
          if (response.success) {
           this.organizationTypes = response.data.data; 
           this.title = this.model.title;
          }
        },
        error: (e) => console.error(e),
        complete: () => console.info('complete')
      }); 
  }

  getModel() {
    let id = this.route.snapshot.paramMap.get('id');
    this.isNew = id == null;
    if (id) {
      this.organizationService.read(this.organizationService.route, id).subscribe({
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
      this.organizationService.create(this.organizationService.route, this.model).subscribe({
        next: (response) => {
          if (response.success) {
            console.info('created');
            this.router.navigate(['organizations']);
          }
        },
        error: (e) => console.error(e),
        complete: () => console.info('complete')
      });
    } else {
      this.organizationService.update(this.organizationService.route, this.model).subscribe({
        next: (response) => {
          if (response.success) {
            console.info('updated');
            this.router.navigate(['organizations']);
          }
        },
        error: (e) => console.error(e),
        complete: () => console.info('complete')
      });
    }
  }

}
