import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { first } from 'rxjs/operators';
import { AccountService } from '../../_service/account.service';
import { AlertService } from '../../_service/alert.service';
import { User } from '../../_model/user.model';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ConfirmPasswordValidationMessages, PasswordRegex, PasswordValidationMessages, PhoneNumberValidationMessages, UsernameValidationsValidationMessages } from 'src/app/_static-data/consts';
import { CustomValidators } from 'src/app/helpers/custom-validators';


@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
    form: FormGroup; 
    loading = false;
    submitted = false;
    returnUrl: string;

    errorMessage: string | null;
    showError: boolean = false;
    phonePattern: string | RegExp = new RegExp('^[0-9]{10}$');
    constructor(private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private accountService: AccountService,
        private alertService: AlertService
    ) { }

    ngOnInit() {
        // get return url from route parameters or default to '/'
        this.accountService.removeToken();
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
        this.initForm();
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
            });
    }

    get phoneNumberValidationMessages() {
        return PhoneNumberValidationMessages;
    }

    get passwordValidationMessages() {
        return PasswordValidationMessages;
    }

    login() {

        this.showError = !this.form.valid;
        console.log('form', this.form.value);

        this.errorMessage = null;
        if (!this.form.valid) {
            this.errorMessage = 'Please check invalid fields!'
            return;
        }

        this.loading = true;
        this.accountService.login(this.form.value.phoneNumber, this.form.value.password)
            .pipe(first())
            .subscribe(
                response => {
                    if (response?.success) {
                        this.router.navigate([this.returnUrl]);
                    } else {
                        this.errorMessage = response?.message;
                    }
                },
                error => {
                    this.alertService.error(error);
                    this.loading = false;
                });
    }

    reset() {
        this.initForm();
    }
}