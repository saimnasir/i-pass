import { Inject, Injectable } from '@angular/core';
import { BaseService } from './base-service';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { LoginResult, ProfileModel, RegisterModel, User } from '../_model/user.model';
import { Router } from '@angular/router';

import { map } from 'rxjs/operators';

import { environment } from '../../environments/environment';
import { FinalResponse } from '../_model/final-response';
import { SingleResponse } from '../_model/single-response';
import { DOCUMENT } from '@angular/common';

@Injectable({ providedIn: 'root' })
export class AccountService extends BaseService<User, string> {

    public user: Observable<LoginResult>;
    private userSubject: BehaviorSubject<LoginResult>;

    private isUserLoggedIn: Observable<boolean>;
    private userLoggedSubject: BehaviorSubject<boolean>;
    constructor(
        private router: Router,
        @Inject(DOCUMENT) private document: Document,
        protected override http: HttpClient) {
        super(http);
        let userStr = localStorage.getItem('user');
        if (userStr) {
            this.userSubject = new BehaviorSubject<LoginResult>(JSON.parse(userStr));
            this.userLoggedSubject = new BehaviorSubject<boolean>(true);
        } else {
            this.userSubject = new BehaviorSubject<LoginResult>(new LoginResult());
            this.userLoggedSubject = new BehaviorSubject<boolean>(false);
        }
        this.user = this.userSubject.asObservable();
        this.isUserLoggedIn = this.userLoggedSubject.asObservable();
    }
    route = `/api/account`;

    alertError(errors: any) {
        super.alertErrorFor(errors, 'account');
    }

    public get userValue(): LoginResult {
        return this.userSubject.value;
    }
    public get userLoggedIn(): boolean {
        return this.userLoggedSubject.value;
    }

    login(username: any, Password: any) {
        let route = `/api/account/login`;
        let model = {
            phoneNumber: username,
            Password: Password,
        };
        return this.post<LoginResult>(route, model)
            .pipe(map(response => {
                if (response.success) {
                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('user', JSON.stringify(response.data));
                    this.userSubject.next(response.data);
                    this.userLoggedSubject.next(true);
                    return response;
                }
                return response;
            }));
    }
    
    loginExternal() {      
        let route = `/api/account/external-token`; 
        return this.get<LoginResult>(route)
            .pipe(map(response => {
                if (response.success) {
                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('user', JSON.stringify(response.data));
                    this.userSubject.next(response.data);                    
                    this.userLoggedSubject.next(true);
                    return response;
                }
                return response;
            }));
    }

    loginWithGoogle() {
        // let provider = 'provider=Google';
        // let callBack = 'callBack=' + this.document.location.origin + '/register/external';
        // this.document.location.href = this.combineWithApiUrl(`/api/account/google?${callBack}`);
        let route = `/api/account/google`; 
        return this.get<LoginResult>(route)
            .pipe(map(response => {
                if (response.success) {
                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('user', JSON.stringify(response.data));
                    this.userSubject.next(response.data);                    
                    this.userLoggedSubject.next(true);
                    return response;
                }
                return response;
            }));
    }

    logout() {
        // remove user from local storage and set current user to null
        this.removeToken();
        this.router.navigate(['/login'])
            .then(() => {
                window.location.reload();
            });
    }

    removeToken() {
        localStorage.removeItem('user');
        this.userSubject.next(new LoginResult());
        this.userLoggedSubject.next(false);
    }

    register(user: RegisterModel) {
        let route = `/api/account/register`;
        return this.post<FinalResponse<SingleResponse<User>>>(route, user);
    }

    getProfile() {
        let route = `/api/account/profile`;
        return this.get<FinalResponse<ProfileModel>>(route);
    }

    updateProfile(model: User) {
        let route = `/api/account/profile`;
        return this.put<FinalResponse<ProfileModel>>(route, model);
    }
}