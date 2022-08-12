import { Component, Input, OnInit } from '@angular/core';
import { AccountService } from '../../_service/account.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { UsernameValidationsValidationMessages } from 'src/app/_static-data/consts';
import { User } from 'src/app/_model/user.model';


@Component({
    selector: 'app-profile',
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
    form: FormGroup; 
    loading = false;
    submitted = false;
    returnUrl: string;

    errorMessage: string | null;
    showError: boolean = false;
    phonePattern: string | RegExp = new RegExp('^[0-9]{10}$');
    editable = false;

    @Input() user : User;
    constructor(private formBuilder: FormBuilder, 
        private accountService: AccountService, 
    ) { }

    get usernameValidationsValidationMessages() {
        return UsernameValidationsValidationMessages;
    }
    
    ngOnInit() {
        this.initForm();
        this.form.patchValue(this.user); 
    }

    // convenience getter for easy access to form fields
    get f() {
        return this.form.controls;
    }

    private initForm() {
        this.form = this.formBuilder.group
            ({
                id: new FormControl(null),
                userName: new FormControl(null, [
                    Validators.required,
                    Validators.minLength(10),
                    Validators.maxLength(10),
                ]),
                email: new FormControl(null),
                firstName: new FormControl(null, [
                    Validators.required, 
                ]),
                lastName: new FormControl(null, [
                    Validators.required, 
                ]),
            });
    }
    
    saveProfile() {
        this.accountService.updateProfile(this.form.value).subscribe({
            next: (response) => {
                if (response.success) {
                    console.info('updated');
                    
                }
                this.editable = false;
            },
            error: (e) => console.error(e),
            complete: () => console.info('complete')
        });
    } 
}