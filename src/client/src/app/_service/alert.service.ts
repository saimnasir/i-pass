﻿
import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { filter } from 'rxjs/operators';


@Injectable({ providedIn: 'root' })
export class AlertService {
    private subject: BehaviorSubject<Alert>;
    private defaultId = 'default-alert';

    constructor() {
        this.subject = new BehaviorSubject<Alert>(new Alert({ id: this.defaultId }));
    }

    onAlert(id = this.defaultId): Observable<Alert> {
        return this.subject.asObservable().pipe(filter(x => x && x.id === id));
    }

    success(message: string, options?: any) {
        this.alert(new Alert({ ...options, alertType: AlertType.SUCCESS, message }));
    }

    error(message: string, options?: any) {
        this.alert(new Alert({ ...options, alertType: AlertType.ERROR, message }));
    }

    info(message: string, options?: any) {
        this.alert(new Alert({ ...options, alertType: AlertType.INFO, message }));
    }

    warn(message: string, options?: any) {
        this.alert(new Alert({ ...options, alertType: AlertType.WARNING, message }));
    }

    alert(alert: Alert) { 
        alert.id = alert.id || this.defaultId;
        this.subject.next(alert);
    }

    clear(id = this.defaultId) {
        this.subject.next(new Alert({ id }));
    }
}

export class Alert {
    id: string;
    message: string; 
    fade: boolean = true;
    alertType: string;
    expiraAt: Date;

    constructor(init?: Partial<Alert>) {
        Object.assign(this, init);
    }
}
export class AlertType {
    public static SUCCESS = "success";
    public static ERROR = "error";
    public static INFO = "info";
    public static WARNING = "warning";
}