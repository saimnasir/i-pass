import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AccountService } from '../../_service/account.service';
import { AlertService } from '../../_service/alert.service';
import { ProfileModel } from '../../_model/user.model';
import { PinCodeService } from 'src/app/_service/pin-code.service';
import { PinCodeModel } from 'src/app/_model/pin-code.model';


@Component({
    selector: 'app-my-profile',
    templateUrl: './my-profile.component.html',
    styleUrls: ['./my-profile.component.css']
})
export class MyProfileComponent implements OnInit {
    model = new ProfileModel();   
    loading = false;
    returnUrl: string;
    errorMessage: string | undefined;
    constructor(
        private accountService: AccountService,
        private alertService: AlertService
    ) { }

    ngOnInit() {  
        this.getModel();
    }
  
    get user(){
        return this.model?.user;
    }

    getModel() {
        this.loading = true;
        this.accountService.getProfile().subscribe({
            next: (response) => {
                if (response.success) {
                    this.model = response.data.data;
                    if (!this.model.pinCode) {
                        this.model.pinCode = new PinCodeModel();
                    }
                }
            },
            error: (e) => console.error(e),
            complete: () => {
                this.loading = false;
                console.info('complete');
            }
        });
    }

}