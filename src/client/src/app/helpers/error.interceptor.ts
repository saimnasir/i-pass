import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AccountService } from '../_service/account.service';


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private accountService: AccountService,
    ) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError((error: HttpErrorResponse) => {

            if ([401, 403].includes(error.status) && this.accountService.userValue) {
                // auto logout if 401 or 403 response returned from api
                this.accountService.logout();
            }
            let errorMessage = '';
            if (error.error instanceof ErrorEvent) {
                // client-side error                
                errorMessage =  `${ error.error.message }`;
            } else {
                // server-side error
                errorMessage = `${ error.status } \nMessage: ${ error.message }`;
            }           
            this.accountService.error(errorMessage);

            return next.handle(request);
        }))
    }
}