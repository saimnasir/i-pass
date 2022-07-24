import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { first } from 'rxjs/operators';
import { AccountService } from '../../_service/account.service';
import { AlertService } from '../../_service/alert.service';
import { User } from '../../_model/user.model';


@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
    model = new User();
    loading = false;
    submitted = false;
    returnUrl: string;
    errorMessage: string | undefined;
    phonePattern : string | RegExp = new RegExp('^[0-9]{10}$');
    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private accountService: AccountService,
        private alertService: AlertService
    ) { }

    ngOnInit() {
        // get return url from route parameters or default to '/'
        this.accountService.removeToken();
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    }

    login() {
        this.errorMessage = undefined;

        this.submitted = true;
        // reset alerts on submit
        this.alertService.clear();

        this.loading = true;
        this.accountService.login(this.model.phoneNumber, this.model.password)
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
    loginWithGoogle() {      
        this.accountService.loginWithGoogle();
    }
    
    loginWithFacebook() {       
        this.accountService.loginWithFacebook();
    }

    isFormValid(): boolean {
        if (!this.model.phoneNumber) {
            return false;
        }
        if (!this.model.password) {
            return false;
        }
        if(!this.model.phoneNumber.match(this.phonePattern)){
            console.log(this.model.phoneNumber, this.phonePattern); 
            return false;
        }
        return true;
    }

    reset(){
        this.model = new User();
    }
}