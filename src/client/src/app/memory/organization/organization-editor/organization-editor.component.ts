import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { OrganizationTypeService } from 'src/app/_service/organization-type.service';
import { OrganizationService } from 'src/app/_service/organization.service';
import { Option } from 'src/app/_model/option.model';

@Component({
  selector: 'app-organization-editor',
  templateUrl: './organization-editor.component.html',
  styleUrls: ['./organization-editor.component.css']
})
export class OrganizationEditorComponent implements OnInit {

  form: FormGroup;

  organizationTypes: Array<Option>;
  parentOrganizations: Array<Option>;
  title: string;
  action: string = 'add';
  id: string | null;
  get isRead() {
    return this.action === 'read'
  }
  errorMessage: string | null;
  showError: boolean = false;

  constructor(private formBuilder: FormBuilder,
    private organizationService: OrganizationService,
    private organizationTypeService: OrganizationTypeService,
    private route: ActivatedRoute,
    protected router: Router) { }


  ngOnInit(): void {

    this.id = this.route.snapshot.paramMap.get('id');

    this.getModel();
    this.loadOrganizations();
    this.loadOrganizationTypes();
    this.initForm();
  }

  private initForm() {
    this.form = this.formBuilder.group({
      id: new FormControl(null),
      title: new FormControl(null, [
        Validators.required,
        Validators.minLength(10),
        Validators.maxLength(100),
      ]),

      organizationTypeId: new FormControl(null, [
        Validators.required,
      ]),

      parentOrganizationId: new FormControl(null, [        
      ]),
      active: new FormControl(true)
    },
    );
  }

  // convenience getter for easy access to form fields
  get f() {
    return this.form.controls;
  }

  loadOrganizations() {
    this.parentOrganizations = [];
    this.organizationService.getAll(this.organizationService.route).subscribe({
      next: (response) => {
        if (response.success) {
          let organizationList = response.data.data;
          // prevent self parenting and circular parenting
          if (this.id) {
            let organization = organizationList.find(s => s.id == this.id); 
            organizationList = organizationList.filter(s => s.id != organization?.id && s.id != organization?.parentOrganizationId && s.parentOrganizationId != organization?.id);          
          }
          this.parentOrganizations = organizationList.map(({ id, title }) => (new Option(id, title)));
        }
      },
      error: (e) => console.error(e),
      //complete: () => console.info('complete')
    });
  }


  loadOrganizationTypes() {
    this.organizationTypeService.getAll(this.organizationTypeService.route).subscribe({
      next: (response) => {
        if (response.success) {
          this.organizationTypes = response.data.data.map(({ id, title }) => (new Option(id, title)));
        }
      },
      error: (e) => console.error(e),
      complete: () => console.info('complete')
    });
  }

  getModel() { 
    this.action = this.route.snapshot.url[1].toString();
    if (this.id) {
      this.organizationService.read(this.organizationService.route, this.id).subscribe({
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
      this.organizationService.create(this.organizationService.route, this.form.value).subscribe({
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
      this.organizationService.update(this.organizationService.route, this.form.value).subscribe({
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
