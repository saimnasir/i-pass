import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AccountService } from '../_service/account.service';
import { AlertService } from '../_service/alert.service';
 

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private accountService: AccountService,
       ) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(err => {
            console.log('cached err', err);
            
            if ([401, 403].includes(err.status) && this.accountService.userValue) {
                // auto logout if 401 or 403 response returned from api
                this.accountService.logout();
            }

            console.error(err);
            const errorMessage = err.error?.message || err.statusText;
            this.accountService.alertService.error(errorMessage+'test');

            let error = new Error(errorMessage);
            return throwError(() => error);
        }))
    }
}