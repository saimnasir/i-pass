﻿import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
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
    form: FormGroup;

    // model = new RegisterModel();
    loading = false;
    submitted = false;

    errorMessage: string | null;
    showError: boolean = false;
    constructor(private formBuilder: FormBuilder,
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
        this.form = this.formBuilder.group
            ({
                phoneNumber: new FormControl(null, [
                    Validators.required,
                    Validators.minLength(10),
                    Validators.maxLength(10),
                ]),
                password: new FormControl(null, [
                    Validators.required,
                    Validators.minLength(8),
                    Validators.pattern(PasswordRegex),
                ]), 
                confirmPassword: new FormControl(null, [
                    Validators.required,
                ])
            });
        this.form.addValidators(CustomValidators.mustMatch('password', 'confirmPassword'));
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
 

    register() { 
        this.showError = !this.form.valid;
        console.log('form', this.form.value);

        this.errorMessage = null;
        if (!this.form.valid) {
            this.errorMessage = 'Please check invalid fields!'
            return;
        }

        this.loading = true;
        this.accountService.register(this.form.value)
            .pipe(first())
            .subscribe(
                data => {
                    this.alertService.success('Registration successful', { keepAfterRouteChange: true });
                    this.router.navigate(['/profile']);
                },
                error => {
                    this.alertService.error(error);
                    this.loading = false;
                });
    }

    loginWithGoogle() {
        this.accountService.loginWithGoogle();
    }

    loginWithFacebook() {
        this.accountService.loginWithFacebook();
    }

    reset() {
        this.initForm();
    }
}