import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AccountService } from '../../_service/account.service';
import { AlertService } from '../../_service/alert.service';
import { CustomValidators } from 'src/app/helpers/custom-validators';




@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
    form = new FormGroup(
        {
            phoneNumber: new FormControl(null, [Validators.required, Validators.pattern("^[0-9]*$"), Validators.minLength(10), Validators.maxLength(10)]),            
            password: new FormControl('', [
                Validators.required,
                Validators.minLength(8)
            ]),
            confirmPassword: new FormControl('', [Validators.required])
          },
      
         
    );

    // model = new RegisterModel();
    loading = false;
    submitted = false;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private accountService: AccountService,
        private alertService: AlertService
    ) { }

    ngOnInit() {
        // this.initForm(); 
        this.form.addValidators( CustomValidators.mustMatch('password', 'confirmPassword'));
    }
    // convenience getter for easy access to form fields
    get f() {
        return this.form.controls;
    }
    // private initForm() {
    //     this.form = new FormGroup(
    //         {
    //             'phoneNumber': new FormControl(null, [Validators.required, Validators.pattern("^[0-9]*$"), Validators.minLength(10), Validators.maxLength(10)]),
    //             'password': new FormControl(null, [Validators.required, Validators.minLength(8)]),
    //             'confirmPassword': new FormControl(null, [Validators.required]),
    //         },

    //         CustomValidators.mustMatch('password', 'confirmPassword') // insert here);
    //     );
    // }

    register() {
        this.submitted = true;

        // reset alerts on submit
        this.alertService.clear();


        this.loading = true;
        this.accountService.register(this.form.value)
            .pipe(first())
            .subscribe(
                data => {
                    this.alertService.success('Registration successful', { keepAfterRouteChange: true });
                    this.router.navigate(['../login'], { relativeTo: this.route });
                },
                error => {
                    this.alertService.error(error);
                    this.loading = false;
                });
    }
}