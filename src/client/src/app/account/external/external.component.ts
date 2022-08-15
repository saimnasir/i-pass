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
  loading :boolean;
  constructor(
    private accountService: AccountService,
    private alertService: AlertService,
    private route: ActivatedRoute,
    private router: Router) {

  }
  ngOnInit() {
    this.loading = true;
    // get return url from route parameters or default to '/'
    this.accountService.removeToken();
    
    this.accountService.loginExternal(this.route.snapshot.queryParams)
      .pipe(first())
      .subscribe(
        response => {
          if (response?.success) {
            this.router.navigate(['/']);
          } else { 
            this.alertService.warn(response?.message);
          }
          this.loading = false;
        },
        error => {
          this.alertService.error(error);
          this.loading = false;
        })
      ;
  } 
}
