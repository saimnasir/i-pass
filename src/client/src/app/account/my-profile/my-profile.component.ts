import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../_service/account.service';
import { ProfileModel } from '../../_model/user.model';
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

    constructor(
        private accountService: AccountService,
    ) { }

    ngOnInit() {
        this.getModel();
    }

    get user() {
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
                } else {
                    this.accountService.error(response.message);
                }
            },
            error: (e) => {
                console.error(e);
                this.accountService.error(e);
            },
            complete: () => {
                this.loading = false;
            }
        });
    }

}