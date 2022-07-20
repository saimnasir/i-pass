import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AccountService } from '../../_service/account.service';
import { AlertService } from '../../_service/alert.service';
import { CustomValidators } from 'src/app/helpers/custom-validators';
import { PhoneNumberValidationMessages, PasswordValidationMessages, ConfirmPasswordValidationMessages, PasswordRegex, PhoneNumberRegex } from 'src/app/_static-data/consts';

 
@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
    form = new FormGroup(
        {
            phoneNumber: new FormControl(),
            password: new FormControl(),
            confirmPassword: new FormControl()
        },
    );

    // model = new RegisterModel();
    loading = false;
    submitted = false;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private accountService: AccountService,
        private alertService: AlertService
    ) { }

    ngOnInit() {
        this.initForm();
        // this.form.addValidators( CustomValidators.mustMatch('password', 'confirmPassword'));
    }

    // convenience getter for easy access to form fields
    get f() {
        return this.form.controls;
    }

    private initForm() {
        this.form = new FormGroup(
            {
                'phoneNumber': new FormControl(null, [
                    Validators.required,
                    Validators.pattern(PhoneNumberRegex),
                    Validators.minLength(10),
                    Validators.maxLength(10)
                ]),
                'password': new FormControl(null, [
                    Validators.required,
                    Validators.minLength(8),
                    Validators.pattern(PasswordRegex),
                ]),
                'confirmPassword': new FormControl(null,
                    [Validators.required]
                ),
            },
            CustomValidators.mustMatch('password', 'confirmPassword') // insert here);
        );
    }

    get phoneNumberValidationMessages() {
        return PhoneNumberValidationMessages;
    }
    get passwordValidationMessages() {
        return PasswordValidationMessages;
    }
    get confirmPasswordValidationMessages() {
        return ConfirmPasswordValidationMessages;
    }

    public findInvalidControls() {
        const invalid = [];
        const controls = this.form.controls;
        let x = this.f[''];
        for (const name in controls) {
            if (controls[name].invalid) {
                invalid.push(name);
            }
        }
        console.log('invalid controls', invalid);
        return invalid;
    }
    public findFirstInvalidControls() {
        let invalid = this.findInvalidControls();
        if (invalid.length > 0) {
            return invalid[0];
        }
        return invalid;
    }

    register() {
        let invalid = this.findFirstInvalidControls();
        console.log('invalid controls[0]', invalid);
        console.log('this.form.value', this.form.value);
        console.log('validation', this.form.valid);
        console.log('form', this.form);
        console.log('phoneNumber errors', this.form.controls['phoneNumber'].errors);
        return;
        this.submitted = true;

        // reset alerts on submit
        this.alertService.clear();


        this.loading = true;
        this.accountService.register(this.form.value)
            .pipe(first())
            .subscribe(
                data => {
                    this.alertService.success('Registration successful', { keepAfterRouteChange: true });
                    this.router.navigate(['../login'], { relativeTo: this.route });
                },
                error => {
                    this.alertService.error(error);
                    this.loading = false;
                });
    }
}