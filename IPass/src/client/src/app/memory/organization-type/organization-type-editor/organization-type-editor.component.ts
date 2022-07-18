import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { Guid } from 'guid-typescript'; 
import { OrganizationTypeModel } from 'src/app/_model/organization-type.model';
import { OrganizationModel } from 'src/app/_model/organization.model';
import { OrganizationTypeService } from 'src/app/_service/organization-type.service'; 

@Component({
  selector: 'app-organization-type-editor',
  templateUrl: './organization-type-editor.component.html',
  styleUrls: ['./organization-type-editor.component.css']
})
export class OrganizationTypeEditorComponent implements OnInit {

  model = new OrganizationTypeModel ();
  title: string;
  isNew = false;
  constructor(private organizationTypeService: OrganizationTypeService, 
    private route: ActivatedRoute,
    protected router: Router) { }
 
  ngOnInit(): void { 
    this.getModel();
  }

  getModel() {
    let id = this.route.snapshot.paramMap.get('id');
    this.isNew = id == null;
    if (!this.isNew) {
      this.organizationTypeService.read(this.organizationTypeService.route, id).subscribe({
        next: (response) => {
          if (response.success) {
            this.model = response.data.data; 
            this.title = this.model.title;
          }
        },
        error: (e) => console.error(e),
        complete: () => console.info('complete')
      });
    } 
  }

  save() {
    if (this.isNew) {
      this.organizationTypeService.create(this.organizationTypeService.route, this.model).subscribe({
        next: (response) => {
          if (response.success) {
            console.info('created');
            this.router.navigate(['organization-types']);
          }
        },
        error: (e) => console.error(e),
        complete: () => console.info('complete')
      });
    } else {
      this.organizationTypeService.update(this.organizationTypeService.route, this.model).subscribe({
        next: (response) => {
          if (response.success) {
            console.info('updated');
            this.router.navigate(['organization-types']);
          }
        },
        error: (e) => console.error(e),
        complete: () => console.info('complete')
      });
    }
  }

}
