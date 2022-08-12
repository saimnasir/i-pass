import { Component, Input, OnInit } from '@angular/core';
import { AccountService } from '../../_service/account.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { PinCodeValidationMessages } from 'src/app/_static-data/consts';
import { PinCodeService } from 'src/app/_service/pin-code.service';
import { PinCodeModel } from 'src/app/_model/pin-code.model';


@Component({
    selector: 'app-settings',
    templateUrl: './settings.component.html',
    styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {
    form: FormGroup; 
    loading = false;
    submitted = false;
    returnUrl: string;
    errorMessage: string | null;
    showError: boolean = false;
    phonePattern: string | RegExp = new RegExp('^[0-9]{10}$');
    editable = false;
    
    @Input() pinCode : PinCodeModel;
    constructor(private formBuilder: FormBuilder, 
        private accountService: AccountService, 
        private pinCodeService: PinCodeService, 
    ) { }

    get pinCodeValidationMessages() {
        return PinCodeValidationMessages;
    }
    
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
                id: new FormControl(null),
                code: new FormControl(null, [
                    Validators.required,
                    Validators.minLength(4),
                    Validators.maxLength(4),
                ]),
                active : new FormControl(true),
            });
            this.form.patchValue(this.pinCode);        
    }
    
    saveSettings() {
        this.pinCodeService.create(this.pinCodeService.route, this.form.value).subscribe({
            next: (response) => {
                if (response.success) {
                    console.info('created');
                    this.editable = false;
                }
            },
            error: (e) => console.error(e),
            complete: () => console.info('complete')
        });
    }

 
   
}