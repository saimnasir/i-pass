import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AccountService } from '../../_service/account.service';
import { AlertService } from '../../_service/alert.service';
import { FormGroup } from '@angular/forms';


@Component({
    selector: 'app-account',
    templateUrl: './account.component.html',
    styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {
    form: FormGroup;
    registerForm: FormGroup;
    loading = false;
    submitted = false;
    returnUrl: string;

    errorMessage: string | null;
    showError: boolean = false;
    phonePattern: string | RegExp = new RegExp('^[0-9]{10}$');
    constructor(
        private route: ActivatedRoute,
        private accountService: AccountService,
        private alertService: AlertService
    ) { }

    ngOnInit() {
        // get return url from route parameters or default to '/'
        this.accountService.removeToken();
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    }

    // convenience getter for easy access to form fields
    get f() {
        return this.form.controls;
    }


    loginWithGoogle() {
        this.accountService.loginWithGoogle();
    }

    loginWithFacebook() {
        this.accountService.loginWithFacebook();
    } 
}