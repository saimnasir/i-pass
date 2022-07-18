import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AccountService } from '../../_service/account.service';
import { AlertService } from '../../_service/alert.service';
import { ProfileModel } from '../../_model/user.model';
import { PinCodeService } from 'src/app/_service/pin-code.service';
import { PinCodeModel } from 'src/app/_model/pin-code.model';


@Component({
    selector: 'app-profile',
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
    model = new ProfileModel();
    prevModel = new ProfileModel();
    loading = false;
    submitted = false;
    returnUrl: string;
    errorMessage: string | undefined;
    constructor(
        private route: ActivatedRoute,
        private router: Router, 
        private accountService: AccountService,
        private pinCodeService: PinCodeService,
        private alertService: AlertService
    ) { }

    ngOnInit() {
        this.getModel();
    }

    saveProfile() {
        this.accountService.updateProfile(this.model.user).subscribe({
            next: (response) => {
                if (response.success) {
                    console.info('created');
                    this.getModel();
                }
            },
            error: (e) => console.error(e),
            complete: () => console.info('complete')
        });
    }


    getModel() {
        this.accountService.getProfile().subscribe({
            next: (response) => {
                if (response.success) {
                    this.model = response.data.data;
                    if(!this.model.pinCode){
                        this.model.pinCode = new PinCodeModel();
                    } 
                    this.prevModel = (JSON.parse(JSON.stringify( this.model)));  
                }
            },
            error: (e) => console.error(e),
            complete: () => console.info('complete')
        });
    }

    isPinCodeChange(){
        let equal =  JSON.stringify(this.prevModel.pinCode) !== JSON.stringify(this.model.pinCode) ;
        return equal;
    }

    isUserChange(){
        let equal =  JSON.stringify(this.prevModel.user) !== JSON.stringify(this.model.user) ;
        return equal;
    }

    savePinCode() {
        this.pinCodeService.create(this.pinCodeService.route, this.model.pinCode).subscribe({
            next: (response) => {
                if (response.success) {
                    console.info('created');
                    this.getModel();
                }
            },
            error: (e) => console.error(e),
            complete: () => console.info('complete')
        });
    }
}