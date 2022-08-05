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

  form: FormGroup;

  title: string;
  action: string = 'add';
  id: string | null;
  get isRead() {
    return this.action === 'read'
  }
  errorMessage: string | null;
  showError: boolean = false;

  constructor(private organizationTypeService: OrganizationTypeService, 
     private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    protected router: Router) { }
 

    ngOnInit(): void {
      this.id = this.route.snapshot.paramMap.get('id');
      this.action = this.route.snapshot.url[1].toString();
      this.getModel();
      this.initForm();
    }
  
    private initForm() {
      this.form = this.formBuilder.group({
        id: new FormControl(null),
        title: new FormControl(null, [
          Validators.required,
          Validators.minLength(2),
          Validators.maxLength(100),
        ]),
      },
      );
    }
  

  // convenience getter for easy access to form fields
  get f() {
    return this.form.controls;
  }

  getModel() {

    if (this.id) {
      this.organizationTypeService.read(this.organizationTypeService.route, this.id).subscribe({
        next: (response) => {
          if (response.success) {
            this.form.patchValue(response.data.data);
            this.title = this.form.value.title;
          }
        },
        error: (e) => console.error(e),
        complete: () => console.info('complete')
      });
    }
  }

  
  save() {
    this.showError = !this.form.valid;
    console.log('form', this.form.value);

    this.errorMessage = null;
    if (!this.form.valid) {
      this.errorMessage = 'Please check invalid fields!'
      return;
    }
    if (this.action === 'add') {
      this.organizationTypeService.create(this.organizationTypeService.route, this.form.value).subscribe({
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
      this.organizationTypeService.update(this.organizationTypeService.route, this.form.value).subscribe({
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
