import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomValidators } from 'src/app/helpers/custom-validators';
import { Option } from 'src/app/_model/option.model';
import { EnvironmentTypeService } from 'src/app/_service/environment-type.service';
import { MemoryTypeService } from 'src/app/_service/memory-type.service';
import { OrganizationService } from 'src/app/_service/organization.service';
import { ConfirmPasswordValidationMessages, EmailValidationMessages, MemoryOrganizationValidationMessages, MemoryTitleValidationMessages, MemoryTypeValidationMessages, PasswordValidationMessages, UsernameValidationsValidationMessages } from 'src/app/_static-data/consts';
import { MemoryService } from '../../../_service/memory.service';

@Component({
  selector: 'app-memory-editor',
  templateUrl: './memory-editor.component.html',
  styleUrls: ['./memory-editor.component.css']
})
export class MemoryEditorComponent implements OnInit {
  form: FormGroup;

  organizations: Array<Option>;
  memoryTypes: Array<Option>;
  environmentTypes: Array<Option>;
  title: string;
  action: string = 'add';
  get isRead() {
    return this.action === 'read'
  }
  errorMessage: string | null;
  showError: boolean = false;
  constructor(private formBuilder: FormBuilder,
    private memoryService: MemoryService,
    private organizationService: OrganizationService,
    private memoryTypeService: MemoryTypeService,
    private environmentTypeService: EnvironmentTypeService,
    private route: ActivatedRoute,
    protected router: Router) { }


  ngOnInit(): void {

    this.loadOrganizations();
    this.loadMemoryTypes();
    this.loadEnvironmentTypes();
    this.initForm();
    this.getModel();

  }
  // convenience getter for easy access to form fields
  get f() {
    return this.form.controls;
  }

  private initForm() {
    this.form = this.formBuilder.group({
      id: new FormControl(null),
      title: new FormControl(null, [
        Validators.required,
        // Validators.minLength(10),
        // Validators.maxLength(100),
      ]),
      email: new FormControl(null, [
        Validators.required,
        // Validators.minLength(10),
        // Validators.maxLength(100),
      ]),
      userName: new FormControl(null, [
        Validators.required,
        // Validators.minLength(10),
        // Validators.maxLength(100),
      ]),
      organizationId: new FormControl(null, [
        Validators.required,
      ]),
      memoryTypeId: new FormControl(null, [
        Validators.required,
      ]),
      environmentTypeId: new FormControl(null),
      hostOrIpAddress: new FormControl(null),
      port: new FormControl(null),
      password: new FormControl(null, [
        Validators.required,
        // Validators.minLength(8),
        // Validators.pattern(PasswordRegex),
      ]),
      confirmPassword: new FormControl(null,
        [Validators.required]
      ),
      description: new FormControl(null,
      ),
      forgotQuestion: new FormControl(null),
      forgotAnswer: new FormControl(null,
      ),
      active: new FormControl(true),
      isUEmailSecure: new FormControl(false),
      isHostOrIpAddressSecure: new FormControl(false),
      isPortSecure: new FormControl(false),
      isUserNameSecure: new FormControl(false),
      // isUserNameSecure: new FormControl(true, [Validators.requiredTrue]),
    },
    );
    this.form.addValidators(CustomValidators.mustMatch('password', 'confirmPassword'));
  }

  get memoryTitleValidationMessages() {
    return MemoryTitleValidationMessages;
  }

  get memoryOrganizationValidationMessages() {
    return MemoryOrganizationValidationMessages;
  }

  get memoryTypeValidationMessages() {
    return MemoryTypeValidationMessages;
  }

  get emailValidationsValidationMessages() {
    return EmailValidationMessages;
  }

  get usernameValidationsValidationMessages() {
    return UsernameValidationsValidationMessages;
  }

  get passwordValidationMessages() {
    return PasswordValidationMessages;
  }

  get confirmPasswordValidationMessages() {
    return ConfirmPasswordValidationMessages;
  }

  loadOrganizations() {
    this.organizations = [];
    this.organizationService.getAll(this.organizationService.route).subscribe({
      next: (response) => {
        if (response.success) {
          this.organizations = response.data.data.map(({ id, title }) => (new Option(id, title)));
        }
      },
      error: (e) => console.error(e),
      //complete: () => console.info('complete')
    });
  }

  loadMemoryTypes() {
    this.memoryTypes = [];
    this.memoryTypeService.getAll(this.memoryTypeService.route).subscribe({
      next: (response) => {
        if (response.success) {
          this.memoryTypes = response.data.data.map(({ id, title }) => (new Option(id, title)));
        }
      },
      error: (e) => console.error(e),
      //complete: () => console.info('complete')
    });
  }

  loadEnvironmentTypes() {
    this.environmentTypeService.getAll(this.environmentTypeService.route).subscribe({
      next: (response) => {
        if (response.success) {
          this.environmentTypes = response.data.data.map(({ id, title }) => (new Option(id, title)));
        }
      },
      error: (e) => console.error(e),
      //complete: () => console.info('complete')
    });
  }

  getModel() {
    let id = this.route.snapshot.paramMap.get('id');
    this.action = this.route.snapshot.url[1].toString();
    if (this.action !== 'add') {
      this.memoryService.readWithDecoding(this.memoryService.route, id).subscribe({
        next: (response) => {
          if (response.success) {
            this.form.patchValue(response.data.data);
            this.form.patchValue({ confirmPassword: this.form.value.password })
            // this.form.patchValue({ title: null })
            this.title = this.form.value.title;
          }
        },
        error: (e) => console.error(e),
        //complete: () => console.info('complete')
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
      this.memoryService.create(this.memoryService.route, this.form.value).subscribe({
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
      this.memoryService.update(this.memoryService.route, this.form.value).subscribe({
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
