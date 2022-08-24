import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { first } from 'rxjs/operators';
import { AccountService } from '../../_service/account.service';
import { AlertService } from '../../_service/alert.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { PasswordRegex, PasswordValidationMessages, UsernameValidationsValidationMessages } from 'src/app/_static-data/consts';
import { CustomValidators } from 'src/app/helpers/custom-validators';


@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
    form: FormGroup;
    loading = false;
    returnUrl: string;

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
                userName: new FormControl(null, [
                    Validators.required,
                    Validators.minLength(10),
                    Validators.maxLength(100),
                ]),
                password: new FormControl(null, [
                    Validators.required,
                    Validators.minLength(8),
                    Validators.pattern(PasswordRegex),
                ]),
            });
    }

    get usernameValidationsValidationMessages() {
        return UsernameValidationsValidationMessages;
    }

    get passwordValidationMessages() {
        return PasswordValidationMessages;
    }

    // private error(message: string) {
    //     this.alertService.error(message,
    //         { fade: false }
    //     );
    // }
    // private success(message: string) {
    //     this.alertService.success(message,
    //         { fade: true }
    //     );
    // }
    // private warn(message: string) {
    //     this.alertService.warn(message,
    //         { autoClose: false }
    //     );
    // }
    // private info(message: string) {
    //     this.alertService.info(message,
    //         { fade: false }
    //     );
    // }

    login() {
        this.alertService.success('Please ...');
        this.alertService.error('Please ...', { fade: false });
        this.alertService.info('Please ...');
        this.alertService.warn('Please ...', { fade: false });
        this.alertService.success('Please check invalid fields 3!');
        this.alertService.info('Please check invalid fields 4!');
        this.alertService.success('Please check invalid fields 5!');

        this.alertService.error('Please check invalid fields 6!');
        this.alertService.warn('Please check invalid fields 7!');
        this.alertService.success('Please check invalid fields 8!');
        this.alertService.info('Please check invalid fields 9!');
        this.alertService.success('Please check invalid fields 10!');

        return;
        this.showError = !this.form.valid;

        if (!this.form.valid) {
            this.alertService.error('Please check invalid fields!', { fade: false });
            return;
        }

        this.loading = true;

        this.accountService.login(this.form.value.userName, this.form.value.password)
            .subscribe({
                next: (response) => {
                    if (response.success) {
                        this.alertService.success('Wellcome back!');
                        this.router.navigate([this.returnUrl]);
                    } else {
                        this.alertService.info(response.message, { fade: false });
                    }
                },
                error: (err) => {
                    this.loading = false;
                },
                complete: () => {
                    this.loading = false;
                }
            });
    }

    reset() {
        this.initForm();
    }
}