import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs';
import { AccountService } from 'src/app/_service/account.service';
import { AlertService } from 'src/app/_service/alert.service';

@Component({
  selector: 'app-external',
  templateUrl: './external.component.html',
  styleUrls: ['./external.component.css']
})
export class ExternalComponent implements OnInit {
  returnUrl: string;
  errorMessage: string | undefined;

  constructor(
    private accountService: AccountService,
    private route: ActivatedRoute,
    private router: Router,
    private alertService: AlertService) {

  }
  ngOnInit() {
    // get return url from route parameters or default to '/'
    this.accountService.removeToken();
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  loginExternal() {
    this.errorMessage = undefined;

    this.alertService.clear();

    this.accountService.loginExternal()
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
        })
      ;
  }
}
