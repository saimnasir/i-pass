import { Inject, Injectable } from '@angular/core';
import { BaseService } from './base-service';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { LoginResult, ProfileModel, RegisterModel, User } from '../_model/user.model';
import { Params, Router } from '@angular/router';

import { map } from 'rxjs/operators';

import { environment } from '../../environments/environment';
import { FinalResponse } from '../_model/final-response';
import { SingleResponse } from '../_model/single-response';
import { DOCUMENT } from '@angular/common';
import { AlertService } from './alert.service';

@Injectable({ providedIn: 'root' })
export class AccountService extends BaseService<User, string> {

    public user: Observable<LoginResult>;
    private userSubject: BehaviorSubject<LoginResult>;

    private isUserLoggedIn: Observable<boolean>;
    private userLoggedSubject: BehaviorSubject<boolean>;
    constructor( public alertService: AlertService,
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
            username: username,
            password: Password,
        };
        return this.post<LoginResult>(route, model)
            .pipe(map(response => {
                if (response.success) {
                    this.addToken(response.data);
                    return response;
                }
                return response;
            }));
    }

    loginExternal(params: Params) {
        let allowToCommunicate = params['allowToCommunicate'];
        let t = params['t'];
        let r = params['r'];
        let e = params['e'];
        let acs = params['acs'];
        let acv = params['acv'];
        let pc = params['pc'];

        let loginResult = new LoginResult();
        loginResult.accessToken = t;
        this.addToken(loginResult);
        let route = `/api/account/ext-callback?allowToCommunicate=${allowToCommunicate}&t=${t}&r=${r}&e=${e}&acs=${acs}&acv=${acv}&pc=${pc}`;
        return this.externalCallBack<LoginResult>(route, t)
            .pipe(map(response => {
                if (response.success) {
                    this.addToken(response.data);
                    return response;
                }
                return response;
            }));
    }

    loginWithGoogle() {
        let callback = this.document.location.origin + '/register/external'
        this.document.location.href = this.combineWithApiUrl(`/api/account/google?callback=${callback}`);
    }

    loginWithFacebook() {
        let callback = this.document.location.origin + '/register/external'
        this.document.location.href = this.combineWithApiUrl(`/api/account/facebook?callback=${callback}`);
    }

    logout() {
        // remove user from local storage and set current user to null
        this.removeToken();
        this.router.navigate(['/account'])
            .then(() => {
                window.location.reload();
            });
    }

    addToken(loginResult: LoginResult) {
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        localStorage.setItem('user', JSON.stringify(loginResult));
        this.userSubject.next(loginResult);
        this.userLoggedSubject.next(true);
    }
    removeToken() {
        localStorage.removeItem('user');
        this.userSubject.next(new LoginResult());
        this.userLoggedSubject.next(false);
    }

    register(user: RegisterModel) {
        let route = `/api/account/register`;
        return this.post<LoginResult>(route, user)
        .pipe(map(response => {
            if (response.success) {
                this.addToken(response.data);
                console.log('response sucess:', response);
                
                return response;
            }
            console.log('response fail:', response);
            return response;
        }));         
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