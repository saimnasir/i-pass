import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AccountService } from '../../_service/account.service';
import { AlertService } from '../../_service/alert.service';
import { CustomValidators } from 'src/app/helpers/custom-validators';
import { PasswordValidationMessages, ConfirmPasswordValidationMessages, PasswordRegex,  UsernameValidationsValidationMessages, EmailValidationMessages } from 'src/app/_static-data/consts';


@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
    form: FormGroup;
    loading = false;
    submitted = false;
    showError: boolean = false;
    returnUrl = 'profile';

    constructor(private formBuilder: FormBuilder,
        private router: Router,
        private accountService: AccountService
    ) { }

    ngOnInit() {
        this.initForm();
    }

    // convenience getter for easy access to form fields
    get f() {
        return this.form.controls;
    }

    private initForm() {
        this.form = this.formBuilder.group
            ({
                userName: new FormControl(null, [
                    Validators.required,
                    Validators.minLength(10),
                    Validators.maxLength(100),
                ]),
                email: new FormControl(null, [
                    Validators.required,
                    Validators.minLength(10),
                    Validators.maxLength(100),
                ]),
                password: new FormControl(null, [
                    Validators.required,
                    Validators.minLength(8),
                    Validators.pattern(PasswordRegex),
                ]), 
                confirmPassword: new FormControl(null, [
                    Validators.required,
                ]),
                active:new FormControl(true)
            });
        this.form.addValidators(CustomValidators.mustMatch('password', 'confirmPassword'));
    }

    get usernameValidationsValidationMessages() {
        return UsernameValidationsValidationMessages;
    }  
    get emailValidationMessages() {
        return EmailValidationMessages;
    }
    get passwordValidationMessages() {
        return PasswordValidationMessages;
    }
    get confirmPasswordValidationMessages() {
        return ConfirmPasswordValidationMessages;
    }
 

    register() { 
        this.showError = !this.form.valid;
       
        if (!this.form.valid) {
            this.accountService.warn('Please check invalid fields!'); 
            return;
        }

        this.loading = true;
        this.accountService.register(this.form.value)
            .subscribe(
                response => {
                    if (response?.success) {
                        this.accountService.success('Wellcome!'); 
                        this.router.navigate([this.returnUrl]);

                    } else {
                        this.accountService.success(response?.message); 
                    }
                    this.loading = false;
                },
                error => {
                    this.accountService.error(error);
                    this.loading = false;
                });
    }
 
    reset() {
        this.initForm();
    }
}